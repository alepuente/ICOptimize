using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public static SpawnManager instance;

    public List<SpawnerEnemy> spawnerPoints;
    private float spawnRateTimer;
    public float spawnRate;
    public bool canSpawn = false;
	// Use this for initialization
	void Start () {
        instance = this;
        spawnRateTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {            
        spawnRateTimer += Time.deltaTime;
        if (spawnRateTimer > spawnRate)
        {
            spawnEnemy();
            spawnRateTimer = 0;
        }
        }
    }

    void spawnEnemy()
    {
        spawnerPoints[Random.Range(0, spawnerPoints.Count)].Spawn();
    }
}
