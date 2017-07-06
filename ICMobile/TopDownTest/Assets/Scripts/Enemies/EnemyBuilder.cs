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
        if (m.GetComponent<EnemyController>() == null)
        {            
        m.layer = 8;
        gameObject.name = enemyType;
        m.transform.rotation = Quaternion.identity;
        m.AddComponent<EnemyController>().name = enemyType;
        m.AddComponent<Health>();
        m.AddComponent<HealthBarController>().healthPanel = (GameObject)Instantiate(Resources.Load("HealthBar"));
        m.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        }
        return m;
    }
}