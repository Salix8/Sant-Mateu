using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePath : MonoBehaviour
{
    private bool isEnable = false;
    private GameObject[] pathObjects;
    void Start()
    {
        // Encuentra todos los objetos con el tag "Path"
        pathObjects = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ToggleObjects()
    {
        // Alterna el estado activo de cada objeto
        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(isEnable);
        }

        isEnable = !isEnable; // Cambia el estado para la pr�xima vez que se haga clic
    }
}
