using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Unico punto de acceso para obtener cualquier Input


[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private bool interactPressed = false;
    private bool submitPressed = false;
    private bool isPause = false;

    private bool isDebug = false;

    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un Input Manager en la escena.");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    //_________
    /* 
     * Notas
     * var buttonAction = new InputAction(type: InputActionType.Button, binding: "<Gamepad>/*Trigger");
     * buttonAction.performed += c => Debug.Log("${c.control.name} pressed (Button)");
     */
    /* 
        using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

        public void Update()
        {
            foreach (var touch in Touch.activeTouches)
                Debug.Log($"{touch.touchId}: {touch.screenPosition},{touch.phase}");
        }
    */

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (isDebug) Debug.Log(context);
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPause = !isPause;
    }


    //__________________________________


    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        submitPressed = false;
    }

}
