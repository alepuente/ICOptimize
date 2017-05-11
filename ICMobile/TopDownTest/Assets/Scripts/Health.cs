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
        reset();
        if(gameObject.name != "Player")
        {
            healthBar = GetComponentInChildren<Image>();
        }
    }

    public void reset()
    {
        health = GameManager.instance.HealthDic[gameObject.name];
        maxHealth = GameManager.instance.HealthDic[gameObject.name];
    }

    private void Update()
    {
        if (MenuManager.instance.gameState == GameState.Game)
        {
            healthBar.fillAmount = health / maxHealth;
            if (health <= 0)
            {
                if (tag != "Player")
                {
                    reset();
                    gameObject.SetActive(false);
                    GameManager.instance.addMoney(name);
                }
                else
                {
                    reset();
                    GameManager.instance.resetGame();
                }
            }
        }
        else if (MenuManager.instance.gameState == GameState.Menu)
        {
            reset();
        }

    }
}
