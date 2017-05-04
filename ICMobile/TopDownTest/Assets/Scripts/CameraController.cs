using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController instance;

    public GameObject controlPos;
    public GameObject closePos;
    public GameObject farPos;

    private bool control;
    private bool close;
    private bool far;

    public GameObject flagObjects;

    private GameObject player;
    private CameraPositions actualPos;

    public enum CameraPositions
    {
        Control,
        Close,
        Far
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        changeCameraPos(CameraPositions.Control);
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

    public void changeCameraPos(CameraPositions pos)
    {
        if (pos == CameraPositions.Control)
        {
            transform.position = controlPos.transform.position;
            close = false;
            far = false;
            flagObjects.SetActive(false);
            actualPos = pos;
        }
        else if (pos == CameraPositions.Close)
        {
            transform.position = closePos.transform.position;
            control = false;
            far = false;
            flagObjects.SetActive(true);
            actualPos = pos;
        }
        else if (pos == CameraPositions.Far)
        {
            transform.position = farPos.transform.position;
            close = false;
            control = false;
            flagObjects.SetActive(true);
            actualPos = pos;
        }
    }

    private void Update()
    {       
        if (actualPos != CameraPositions.Control)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
        }
    }
}
