using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelector : MonoBehaviour
{
    public LineDrawer lineDrawer; // Referencia al script LineDrawer
    private bool isFirstButtonSelected = false;

    public void OnButtonClick(GameObject button)
    {
        if (!isFirstButtonSelected)
        {
            lineDrawer.startButton = button;
            isFirstButtonSelected = true;
        }
        else
        {
            // Asegurarse de que el segundo botón esté en el otro lado
            if ((lineDrawer.startButton.tag == "TopButton" && button.tag == "BottomButton") ||
                (lineDrawer.startButton.tag == "BottomButton" && button.tag == "TopButton"))
            {
                lineDrawer.endButton = button;
                lineDrawer.DrawLine();
                isFirstButtonSelected = false; // Reiniciar para otra línea
            }
            else
            {
                Debug.Log("No puedes seleccionar dos botones del mismo lado.");
            }
        }
    }

}


