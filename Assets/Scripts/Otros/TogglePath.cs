using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePath : MonoBehaviour
{
    private bool isEnable = false;

    public void ToggleObjects()
    {
        // Alterna el estado activo de cada objeto
        GlobalManager.GetInstance().SetPathObject(isEnable);

        isEnable = !isEnable; // Cambia el estado para la pr�xima vez que se haga clic
    }
}
