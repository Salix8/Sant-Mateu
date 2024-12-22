using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlCursor : MonoBehaviour
{
    [SerializeField] private int cursorSize = 32;
    [SerializeField] private Texture2D cursorNormal, cursorMano;
    private Texture2D cursorActual;

    private static ControlCursor instance;

    // Start is called before the first frame update
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un DialogueManager en la escena");
            Destroy(gameObject);
            return;
        }
        instance = this;
        Cursor.visible = false;

        SetupButtonEventTriggers();

        changeCursor("normal");
        DontDestroyOnLoad(gameObject);
    }

    public static ControlCursor GetInstance()
    {
        return instance;
    }

    private void SetupButtonEventTriggers()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            // Añadir EventTrigger si no existe
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = button.gameObject.AddComponent<EventTrigger>();

            // Entrada del ratón
            EventTrigger.Entry enterEntry = new EventTrigger.Entry();
            enterEntry.eventID = EventTriggerType.PointerEnter;
            enterEntry.callback.AddListener((data) => { OnPointerEnterButton(); });
            trigger.triggers.Add(enterEntry);

            // Salida del ratón
            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((data) => { OnPointerExitButton(); });
            trigger.triggers.Add(exitEntry);
        }
    }
    private void OnPointerEnterButton()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            changeCursor("mano");
        }
    }

    private void OnPointerExitButton()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            changeCursor("normal");
        }
    }


    public void changeCursor(string tipoCursor)
    {
        if (tipoCursor == "normal")
            cursorActual = cursorNormal;
        else if (tipoCursor == "mano")
            cursorActual = cursorMano;
        else
            Debug.Log("No encuentro ningun cursor con este nombre: " + tipoCursor);
    }

    public void ResetCursor()
    {
        changeCursor("normal");
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height-Input.mousePosition.y, cursorSize, cursorSize), cursorActual);
    }

}
