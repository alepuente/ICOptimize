using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float maxHealth;

    private void Start()
    {
        health = GameManager.instance.HealthDic[gameObject.tag];
        maxHealth = GameManager.instance.HealthDic[gameObject.tag];
        if (tag!="Player")
        {
            healthBar = GetComponentInChildren<Image>();
        }
    }

    private void Update()
    {
        if (MenuManager.instance.gameState == GameState.Game)
        {            
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        }
    }
}
