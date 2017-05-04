using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public List<string> enemyTypes;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        GameObject aux = EnemyBuilder.instance.Build(enemyTypes[Random.Range(0, enemyTypes.Count)]);
        aux.transform.LookAt(PlayerController.instance.transform);
        aux.gameObject.transform.position = gameObject.transform.position;
    }
}