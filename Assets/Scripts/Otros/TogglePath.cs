using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePath : MonoBehaviour
{

    public void ToggleObjects()
    {
        GameObject[] objs = GlobalManager.GetInstance().GetPathObjects(); // Cambia el estado para la pr�xima vez que se haga clic
        if(objs.Length == 0)
        {
            Debug.LogWarning("No se han encontrado objetos con la etiqueta 'Path'");
            return;
        }
        bool toggleVal = !objs[0].activeSelf;

        // Alterna el estado activo de cada objeto
        GlobalManager.GetInstance().SetPathObject(true);

    }
}
