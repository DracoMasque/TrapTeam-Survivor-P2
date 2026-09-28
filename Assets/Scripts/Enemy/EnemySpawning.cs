using System.Collections.Generic;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private GameObject enemiesContainer;
    private Cooldown timer;
    private CinemachineCamera currentCamera;
    void Start()
    {
        Assert.IsNotEmpty(enemies, "EnemySpawning.cs : There are no enemies to spawn");
        Assert.IsNotNull(enemiesContainer, "EnemySpawning.cs : the enemiesContainer wasn't set properly");
        timer = GetComponent<Cooldown>();
        currentCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        if (timer.finished)
        {
            SpawnEnemy(ChangeEnemy());
            timer.Play();
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, enemiesContainer.transform);
        newEnemy.transform.position = RandomSpawnPoint();
    }
    
    void ChangeTimer()
    {
        //as the game progresses have enemies spawn more often
    }
    private GameObject ChangeEnemy()
    {
        //as the game progresses choose harder and harder
        //I have no idea how to do that yet
        return enemies[0];
    }
    //=================SPAWN POINT===================//
    //chooses a random spawn point on a circle bigger than the camera
    private Vector2 RandomSpawnPoint()
    {
        float circleRadius = CircleRadius();
        float radianPoint = RandomRadianPoint();
        Vector2 result = RadianToCoords(radianPoint, circleRadius);
        return result;
    }
    //longueure de la camera + un % en plus comme radius
    private float CircleRadius()
    {
        if (!currentCamera)
        {
            return 1.0f;
        }
        float currentCameraWidth = currentCamera.Lens.OrthographicSize/0.5f;
        float padding = currentCameraWidth * 0.2f;
        
        float circleRadius = currentCameraWidth + padding;
        
        return circleRadius;
    }
    private float RandomRadianPoint()
    {
        float radian = Random.Range(0, 2 * Mathf.PI);
        return radian;
    }
    private Vector2 RadianToCoords(float radians, float radius)
    {
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        Vector2 cameraPos = currentCamera.gameObject.transform.position;
        return new Vector2(x + cameraPos.x, y + cameraPos.y) ;
    }
}
