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

    // private bool isDebug = false;

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

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
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
