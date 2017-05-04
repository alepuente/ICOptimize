using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<SpawnerEnemy> spawnerPoints;
    private float spawnRateTimer;
    public float spawnRate;
	// Use this for initialization
	void Start () {
        spawnRateTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        spawnRateTimer += Time.deltaTime;
        if (spawnRateTimer > spawnRate)
        {
            spawnEnemy();
            spawnRateTimer = 0;
        }
    }

    void spawnEnemy()
    {
        spawnerPoints[Random.Range(0, spawnerPoints.Count)].Spawn();
    }
}
