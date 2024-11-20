using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ID del botón correcto
    public int correctButtonID = 1;

    // Método que se llamará al presionar cualquier botón
    public void OnButtonPressed(int buttonID)
    {
        if (buttonID == correctButtonID)
        {
            Debug.Log($"¡Correcto!");
        }
        else
        {
            Debug.Log($"Incorrecto");
        }
    }
}
