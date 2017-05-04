using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBuilder : MonoBehaviour
{

    public static EnemyBuilder instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Build(string enemyType)
    {
        GameObject m = EnemyFactory.instance.create(enemyType);
        m.AddComponent<EnemyController>();
        m.AddComponent<NavMeshAgent>();
        return m;
    }
}