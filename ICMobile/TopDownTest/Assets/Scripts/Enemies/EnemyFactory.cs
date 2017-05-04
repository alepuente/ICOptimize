using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{

    public static EnemyFactory instance;

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

    public GameObject create(string enemyType)
    {
        return (GameObject)Instantiate(Resources.Load(enemyType, typeof(GameObject))) as GameObject; ;
    }
}