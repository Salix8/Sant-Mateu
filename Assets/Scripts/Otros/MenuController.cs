using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private bool isDebug = true;

    public void MenuAManejar()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
            if (isDebug) Debug.Log($"Se ha cerrado el menu: {menu}. MenuController.MenuAManejar()");
        }
        else
        {
            menu.SetActive(true);
            if (isDebug) Debug.Log($"Se ha abierto el menu: {menu}. MenuController.MenuAManejar()");
        }
    }
}
