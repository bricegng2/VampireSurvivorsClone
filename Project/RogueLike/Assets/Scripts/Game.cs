using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game sInstance;

    public Player player;

    public GameObject enemyPrefab;
    public int poolSize = 10;
    private List<Enemy> enemyPool;

    private float enemySpawnTimer = 2.0f;

    //private int round = 1;

    public static Game Instance
    {
        get { return sInstance; }
    }

    public Player getPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this);
        }
        else
        {
            sInstance = this;
        }

        InitializeEnemyPool();
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer < 0)
        {
            enemySpawnTimer = Constants.enemySpawnTimer;
            SpawnEnemy();
        }
    }

    private void InitializeEnemyPool()
    {
        enemyPool = new List<Enemy>();
        EnemySpawnPoint[] spawnPoints = FindObjectsByType<EnemySpawnPoint>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemyObject = Instantiate(enemyPrefab);
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            enemyObject.SetActive(false);

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            enemy.setPosition = spawnPoints[spawnIndex].position;

            enemyPool.Add(enemy);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPool.Count > 0)
        {
            Enemy enemy = enemyPool[0];
            enemy.gameObject.SetActive(true);
            enemyPool.RemoveAt(0);
        }
    }

    /*
    public Enemy GetEnemyFromPool()
    {
        if (enemyPool.Count > 0)
        {
            Enemy enemy = enemyPool.Dequeue();
            enemy.gameObject.SetActive(true);
            return enemy;
        }
        else
        {
            GameObject enemyObject = Instantiate(enemyPrefab);
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            return enemy;
        }
    }

    public void ReturnEnemyToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
    */
}
