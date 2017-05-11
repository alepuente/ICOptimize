using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Game,
    Pause
}

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public Vector3 inicialPos;

    public GameObject menuHUD;
    public GameObject gameHUD;
    public GameObject pauseHUD;

    public GameState gameState;


	// Use this for initialization
	void Start () {
        instance = this;
        StartMenu();
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
}
