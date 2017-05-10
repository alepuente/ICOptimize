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

    public GameState gameState;


	// Use this for initialization
	void Start () {
        instance = this;
        gameState = GameState.Menu;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        gameState = GameState.Game;
        CameraController.instance.changeToControl();
        menuHUD.SetActive(false);
        gameHUD.SetActive(true);
        SpawnManager.instance.canSpawn = true;
    }
}
