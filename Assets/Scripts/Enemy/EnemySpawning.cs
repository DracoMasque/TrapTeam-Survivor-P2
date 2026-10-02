using System.Collections.Generic;

using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private GameObject enemiesContainer;
    private Cooldown timer;
    private CinemachineCamera currentCamera;
    void Start()
    {
        timer = GetComponent<Cooldown>();
        currentCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineCamera>();
        Assert.IsTrue(enemies.Count>0, "EnemySpawning.cs : There are no enemies to spawn");
        Assert.IsNotNull(enemiesContainer, "EnemySpawning.cs : the enemiesContainer wasn't set properly");
    }

    void Update()
    {
        if (timer.finished)
        {
            SpawnEnemy(ChooseEnemy());
            timer.Play();
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        if (enemy)
        {
            GameObject newEnemy = Instantiate(enemy, enemiesContainer.transform);
            newEnemy.transform.position = RandomSpawnPoint();
        }
    }
    
    void ChangeTimer()
    {
        //as the game progresses have enemies spawn more often
    }
    void ChangeEnemy(Dictionary<GameObject, int> newEnemies)
    {
        List<GameObject> newEnemiesList = new List<GameObject>();
        foreach (var item in newEnemies)
        {
            if (item.Value > 0)
            {
                for (int i = 0; i <= item.Value; i++)
                {
                    newEnemiesList.Add(item.Key);
                }
            }
        }
        enemies = newEnemiesList;
    }
    private GameObject ChooseEnemy()
    {
        if (enemies.Count == 0)
        {
            return null;
        }
        int i = Random.Range(0, enemies.Count-1);
        return enemies[i];
    }
    //=================SPAWN POINT===================//
    
    //chooses a random spawn point on a circle bigger than the camera
    private Vector2 RandomSpawnPoint()
    {
        float radianPoint = RandomRadianPoint();
        Vector2 result = RadianToCoords(radianPoint);
        return result;
    }
    //longueure de la camera + un % en plus comme radius
    private float CircleRadiusX()
    {
        if (!currentCamera)
        {
            return 1.0f;
        }
        float currentCameraWidth = currentCamera.Lens.OrthographicSize*2f;
        float padding = currentCameraWidth * 0.2f;
        
        float circleRadius = currentCameraWidth + padding;
        
        return circleRadius;
    }
    private float CircleRadiusY()
    {
        if (!currentCamera)
        {
            return 1.0f;
        }
        float currentCameraWidth = currentCamera.Lens.OrthographicSize;
        float padding = currentCameraWidth * 0.2f;
        
        float circleRadius = currentCameraWidth + padding;
        
        return circleRadius;
    }
    private float RandomRadianPoint()
    {
        float radian = Random.Range(0, 2 * Mathf.PI);
        return radian;
    }
    private Vector2 RadianToCoords(float radians)
    {
        float x = Mathf.Cos(radians) * CircleRadiusX();
        float y = Mathf.Sin(radians) * CircleRadiusY();
        Vector2 cameraPos = currentCamera.gameObject.transform.position;
        return new Vector2(x + cameraPos.x, y + cameraPos.y) ;
    }
}
