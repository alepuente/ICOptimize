using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{

    public static EnemyFactory instance;
    private List<GameObject> enemies;
    public int enemiesAlive;

    private void Awake()
    {
        instance = this;
        enemies = new List<GameObject>();
    }
    
  
    public void clearEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Health>().reset();
            enemies[i].SetActive(false);
        }
        enemiesAlive = 0;
    }

    public GameObject create(string enemyType)
    {
        if (enemies.Count>0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    enemies[i].SetActive(true);
                    enemiesAlive++;
                    return enemies[i];
                }            
            }
        }       
        GameObject enemie;
        enemie = (GameObject)Instantiate(Resources.Load(enemyType, typeof(GameObject))) as GameObject;
        enemies.Add(enemie);
        enemiesAlive++;
        return enemie;        
    }
}