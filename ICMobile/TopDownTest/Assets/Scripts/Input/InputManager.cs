using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;
    IInput _input;

    void Awake()
    {
        if (!instance){
            instance = this;
        }

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        _input = new InputKeyboard();
#elif UNITY_ANDROID
        _input = new InputMobile();
#endif
    }

    public Vector3 getSelection()
    {
        return _input.getSelection();
    }
}
