using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private bool isEnabled = false;
    private GameObject[] menuObjects;

    void Start()
    {
        // Encuentra todos los objetos con el tag "Menu"
        menuObjects = GameObject.FindGameObjectsWithTag("Menu");
        ToggleMenu(false);
    }

    public void OnButtonClick()
    {
        isEnabled = !isEnabled;
        ToggleMenu(isEnabled);
    }

    private void ToggleMenu(bool enabled)        // Los canvia
    {
        foreach (GameObject obj in menuObjects)
        {
            obj.SetActive(enabled);
        }
    }
}
