using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
//using UnityEngine.Assertions;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] Dictionary<Elements, List<GameObject>> enemies = new Dictionary<Elements, List<GameObject>>()
    {
        { Elements.Feu, null },
        { Elements.Glace, null },
        { Elements.Nature, null },
        { Elements.Maudit, null },
    };
    [SerializeField] private GameObject enemiesContainer;
    private Cooldown timer;
    private CinemachineCamera currentCamera;
    
    void Start()
    {
        timer = GetComponent<Cooldown>();
        currentCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineCamera>();
        //Assert.IsTrue(enemies.Count>0, "EnemySpawning.cs : There are no enemies to spawn");
        //Assert.IsNotNull(enemiesContainer, "EnemySpawning.cs : the enemiesContainer wasn't set properly");
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
    void ChangeEnemy(Dictionary<Elements, List<GameObject>> newEnemies)
    {
        Elements[] keys = enemies.Keys.ToArray();

        foreach (Elements key in keys)
        {
            enemies[key] = newEnemies[key];
        }
    }
    private GameObject ChooseEnemy()
    {
        Elements element = Elements.None;
        int elementIndex = Random.Range(1, ElementManager.instance.spawnRatio[Elements.Glace]+1);
        if (enemies.Count == 0)
        {
            return null;
        }

        if (elementIndex > 0 && elementIndex < ElementManager.instance.spawnRatio[Elements.Feu])
        {
            element = Elements.Feu;
        }
        else if (elementIndex > ElementManager.instance.spawnRatio[Elements.Feu] && elementIndex < ElementManager.instance.spawnRatio[Elements.Nature])
        {
            element = Elements.Nature;
        }
        else if (elementIndex > ElementManager.instance.spawnRatio[Elements.Nature] && elementIndex < ElementManager.instance.spawnRatio[Elements.Maudit])
        {
            element = Elements.Maudit;
        }
        else if (elementIndex > ElementManager.instance.spawnRatio[Elements.Maudit] && elementIndex < ElementManager.instance.spawnRatio[Elements.Glace])
        {
            element = Elements.Glace;
        }

        if (enemies[element].Count == 0)
        {
            return null;
        }
        else
        {
            int i = Random.Range(0, enemies[element].Count-1);
            return enemies[element][i];
        }
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
