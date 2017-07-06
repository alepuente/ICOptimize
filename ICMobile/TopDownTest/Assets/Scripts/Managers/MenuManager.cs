using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Menu,
    Game,
    Pause
}

public class MenuManager : MonoBehaviour
{

    public static MenuManager instance;

    public Vector3 inicialPos;
    public GameObject menuHUD;
    public GameObject gameHUD;
    public GameObject pauseHUD;

    public Text moneyText;

    public Text damageAmountText;
    public Button damageButton;
    public int damagePrice;
    public float damageAmount;

    public Button frontCannonButton;
    public int frontCannonPrice;

    public Text speedAmountText;
    public Button speedButton;
    public int speedPrice;
    public float speedAmount;

    public Text turnAmountText;
    public Button turnButton;
    public int turnPrice;
    public float turnAmount;

    public Text healthAmountText;
    public Button healthButton;
    public int healthPrice;
    public float healthAmount;

    public Text enemiesLeft;

    public GameState gameState;

    // Use this for initialization
    void Start()
    {
        instance = this;
        StartMenu();

        moneyText.text = GameManager.instance.money.ToString();
        damageButton.GetComponentInChildren<Text>().text = damagePrice.ToString();
        damageAmountText.text = "+" + damageAmount + " Cannons Damage";

        speedButton.GetComponentInChildren<Text>().text = speedPrice.ToString();
        speedAmountText.text = "+" + speedAmount + " Speed";

        turnButton.GetComponentInChildren<Text>().text = turnPrice.ToString();
        turnAmountText.text = "+" + turnAmount + " Turn Speed";

        healthButton.GetComponentInChildren<Text>().text = healthPrice.ToString();
        healthAmountText.text = "+" + healthAmount + " Max Health";

        frontCannonButton.GetComponentInChildren<Text>().text = frontCannonPrice.ToString();
    }

    private void Update()
    {
        if (GameManager.instance.money >= damagePrice) { damageButton.interactable = true; }
        else { damageButton.interactable = false; }

        if (GameManager.instance.money >= frontCannonPrice) { frontCannonButton.interactable = true; }
        else { frontCannonButton.interactable = false; }

        if (GameManager.instance.money >= speedPrice) { speedButton.interactable = true; }
        else { speedButton.interactable = false; }

        if (GameManager.instance.money >= turnPrice) { turnButton.interactable = true; }
        else { turnButton.interactable = false; }

        if (GameManager.instance.money >= healthPrice) { healthButton.interactable = true; }
        else { healthButton.interactable = false; }

        enemiesLeft.text = "Enemies Left: " + EnemyFactory.instance.enemiesAlive;
        moneyText.text = GameManager.instance.money.ToString();
    }
    #region MainMenu


    public void StartMenu()
    {
        gameState = GameState.Menu;
        gameHUD.SetActive(false);
        menuHUD.SetActive(true);
        pauseHUD.SetActive(false);
    }
    public void StartGame()
    {
        gameState = GameState.Game;
        CameraController.instance.changeToControl();
        menuHUD.SetActive(false);
        gameHUD.SetActive(true);
        pauseHUD.SetActive(false);
        SpawnManager.instance.canSpawn = true;
    }
    public void Pause()
    {
        pauseHUD.SetActive(true);
        gameHUD.SetActive(false);
        menuHUD.SetActive(false);
        gameState = GameState.Pause;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pauseHUD.SetActive(false);
        gameHUD.SetActive(true);
        menuHUD.SetActive(false);
        gameState = GameState.Game;
        Time.timeScale = 1;
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        GameManager.instance.resetGame();
    }
    public void Exit()
    {
        Application.Quit();
    }
    #endregion
    #region Upgrades
    public void upgradeCannonDamage()
    {
        GameManager.instance.money -= damagePrice;
        GameManager.instance.updateDamage("Player", damageAmount);
        damageAmount += 1;
        damagePrice += 200;
        damageButton.GetComponentInChildren<Text>().text = damagePrice.ToString();
        damageAmountText.text = "+" + damageAmount + " Cannons Damage";
    }
    public void upgradeSpeed()
    {
        GameManager.instance.money -= speedPrice;
        GameManager.instance.updateSpeed("Player", speedAmount);
        speedAmount += 1;
        speedPrice += 150;
        speedButton.GetComponentInChildren<Text>().text = speedPrice.ToString();
        speedAmountText.text = "+" + speedAmount + " Speed";
    }
    public void upgradeSteering()
    {
        GameManager.instance.money -= turnPrice;
        GameManager.instance.updateSteering("Player", turnAmount);
        turnAmount += 0.1f;
        turnPrice += 100;
        turnButton.GetComponentInChildren<Text>().text = turnPrice.ToString();
        turnAmountText.text = "+" + turnAmount + " Turn Speed";
    }
    public void upgradeHealth()
    {
        GameManager.instance.money -= healthPrice;
        GameManager.instance.updateSteering("Player", healthAmount);
        healthPrice += 100;
        healthButton.GetComponentInChildren<Text>().text = healthPrice.ToString();
        healthAmountText.text = "+" + healthAmount + " MaxHealth";
    }
    public void upgradeFrontCannon()
    {
            GameManager.instance.money -= frontCannonPrice;
            GameManager.instance.enableFrontCannon();
            frontCannonButton.GetComponentInChildren<Text>().text = "MAX";
            frontCannonPrice = 99999999;
    }
    #endregion
}
