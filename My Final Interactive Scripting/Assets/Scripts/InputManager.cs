using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public Gun gun;

    public bool debug = false;

    void Start()
    {
        if (debug) Debug.Log("Input Manager is starting up");
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        if (mouse.leftButton.isPressed)
        {
            if(debug) Debug.Log("Left Mouse Button was pressed this frame.");
            if(gun != null)
            {
                gun.Fire();
            }
        }

        var keyboard = Keyboard.current;
        if(keyboard == null) return;



    }
    private InputAction leftMouseClick;

    private void Awake()
    {
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => LeftMouseClicked();
        leftMouseClick.Enable();
    }

    private void LeftMouseClicked()
    {
        print("LeftMouseClicked");
    }
}
