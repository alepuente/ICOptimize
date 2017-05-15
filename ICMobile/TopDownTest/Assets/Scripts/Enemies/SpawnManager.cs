using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;

    public List<SpawnerEnemy> spawnerPoints;
    private float spawnRateTimer;
    public float spawnRate;
    public bool canSpawn = false;
    public bool isSpawning = false;
    public int maxEnemies;

	// Use this for initialization
	void Start () {
        instance = this;
        spawnRateTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (MenuManager.instance.gameState == GameState.Game)
        {            
        spawnRateTimer += Time.deltaTime;
        isSpawning = true;
        if (canSpawn && EnemyFactory.instance.enemiesAlive < maxEnemies)
        {
        if (spawnRateTimer > spawnRate)
        {
            spawnEnemy();
            spawnRateTimer = 0;
        }
        }
        else
        {
            canSpawn = false;
            isSpawning = false;
        }
        if (EnemyFactory.instance.enemiesAlive <= 0 && !isSpawning)
        {
            canSpawn = true;
            isSpawning = true;
            maxEnemies += Random.Range(1, 3);
        }
        }     
    }

    void spawnEnemy()
    {
        spawnerPoints[Random.Range(0, spawnerPoints.Count)].Spawn();
    }
}
