using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        instance = this;
        Cursor.visible = false;
        changeCursor("normal");
    }

    public static ControlCursor GetInstance()
    {
        return instance;
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

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height-Input.mousePosition.y, cursorSize, cursorSize), cursorActual);
    }

}
