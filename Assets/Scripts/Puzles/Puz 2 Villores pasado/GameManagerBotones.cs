using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ID del bot�n correcto
    public int correctButtonID = 1;

    // M�todo que se llamar� al presionar cualquier bot�n
    public void OnButtonPressed(int buttonID)
    {
        if (buttonID == correctButtonID)
        {
            Debug.Log($"�Correcto!");
        }
        else
        {
            Debug.Log($"Incorrecto");
        }
    }
}
