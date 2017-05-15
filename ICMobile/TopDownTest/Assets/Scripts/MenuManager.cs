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
    public int crewPrice;

    public Text damageAmountText;
    public Button damageButton;
    public int damagePrice;
    public float damageAmount;

    public Button frontCannonButton;
    public int frontCannonPrice;

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
        frontCannonButton.GetComponentInChildren<Text>().text = frontCannonPrice.ToString();
    }

    private void Update()
    {
        if (GameManager.instance.money >= damagePrice) { damageButton.interactable = true; }
        else { damageButton.interactable = false; }
        if (GameManager.instance.money >= frontCannonPrice) { frontCannonButton.interactable = true; }
        else { frontCannonButton.interactable = false; }
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
    public void upgradeFrontCannon()
    {
            GameManager.instance.money -= frontCannonPrice;
            GameManager.instance.enableFrontCannon();
            frontCannonButton.GetComponentInChildren<Text>().text = "MAX";
            frontCannonPrice = 99999999;
    }
    #endregion
}
