using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCursor : MonoBehaviour
{
    [SerializeField] private int cursorSize = 32;
    [SerializeField] private Texture2D cursorNormal, cursorMano;
    private Texture2D cursorActual;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        changeCursor("normal");
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
