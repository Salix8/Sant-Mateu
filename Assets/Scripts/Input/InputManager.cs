using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Unico punto de acceso para obtener cualquier Input


[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private bool submitPressed = false;
    private Vector2 direction = Vector2.zero;
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

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        Debug.Log($"Fase del clic: {context.phase}");
        if (context.performed)
        {
            Debug.Log("Clic detectado correctamente (fase: performed)");
            submitPressed = true;
        }
        else if (context.canceled)
        {
            Debug.Log("Clic detectado correctamente (fase: canceled)");
            submitPressed = false;
        }
    }

    public void SelectChoice(InputAction.CallbackContext context)
    {
        // ReadValue tiene que leer un Vector2 si no, no funcionan las opciones de dialogo ni el segundo sistema de movimiento (si empiezas con wasd luego las flechas no van)
        // Esto se determina en Controls que es una accion que establecemos en Unity y que hemos cambiado en el EventSystem
        if (isDebug) Debug.Log(context);
        if (context.performed)
        {
            direction = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            direction = context.ReadValue<Vector2>();
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPause = !isPause;
    }


    //__________________________________

    public Vector2 GetDirection() //En principio esto solo sirve para las choices
    {
        return direction;
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
