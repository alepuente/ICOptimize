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
        //aux.gameObject.transform.position = gameObject.transform.position;
        Vector2 randomPos = Random.insideUnitCircle.normalized * 100;
        aux.gameObject.transform.position = new Vector3(randomPos.x, 0, randomPos.y);
        aux.transform.LookAt(PlayerController.instance.transform.position);
    }
}