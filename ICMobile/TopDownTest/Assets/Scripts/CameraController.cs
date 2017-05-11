using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    public GameObject controlPos;
    public GameObject closePos;
    public GameObject farPos;
    public GameObject menuPos;

    public Camera followCamera;
    public Camera onBoardCamera;
    public Camera menuCamera;

    private GameObject player;
    private CameraPositions actualPos;

    public bool nest;

    public Button nestButton;

    public enum CameraPositions
    {
        Control,
        Close,
        Far,
        Menu
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        changeCameraPos(CameraPositions.Menu);
        player = PlayerController.instance.gameObject;
	}

    public void changeToControl()
    {
        changeCameraPos(CameraPositions.Control);
    }
    public void changeToClose()
    {
        changeCameraPos(CameraPositions.Close);
    }
    public void changeToFar()
    {
        changeCameraPos(CameraPositions.Far);
    }
    public void changeToMenu()
    {
        changeCameraPos(CameraPositions.Menu);
    }

    public void changeCameraPos(CameraPositions pos)
    {
        if (pos == CameraPositions.Menu)
        {
            followCamera.gameObject.SetActive(false);
            onBoardCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(true);

            menuCamera.gameObject.transform.position = menuPos.transform.position;
            actualPos = pos;
        }
        else if (pos == CameraPositions.Control)
        {
            onBoardCamera.gameObject.SetActive(true);
            followCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(false);

            PlayerController.instance.flagObjects.SetActive(false);
            actualPos = pos;
        }
        else if (pos == CameraPositions.Close)
        {
            followCamera.gameObject.SetActive(true);
            onBoardCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(false);

            followCamera.gameObject.transform.position = closePos.transform.position;
            PlayerController.instance.flagObjects.SetActive(true);
            actualPos = pos;
        }
        else if (pos == CameraPositions.Far)
        {
            followCamera.gameObject.SetActive(true);
            onBoardCamera.gameObject.SetActive(false);
            menuCamera.gameObject.SetActive(false);

            followCamera.gameObject.transform.position = farPos.transform.position;
            PlayerController.instance.flagObjects.SetActive(true);
            actualPos = pos;
        }
    }

    private void Update()
    {
        if (nest)
        {
            nestButton.interactable = true;
        }
        else
        {
            nestButton.interactable = false;
        }

        if (actualPos == CameraPositions.Close || actualPos == CameraPositions.Far)
        {
            followCamera.gameObject.transform.position = new Vector3(player.transform.position.x, followCamera.transform.position.y, player.transform.position.z);
        }
    }
}
