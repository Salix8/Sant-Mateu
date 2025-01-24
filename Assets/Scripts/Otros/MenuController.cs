using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private bool isDebug = true;
    [SerializeField] private GameObject transicionmenu;

    private bool switcher = false;

    private Animator transition;

    [SerializeField] private bool playTransition = false;

    void Start()
    {
        transition = transicionmenu.GetComponent<Animator>();

    }

    public void MenuAManejar()
    {
        //Time.timeScale = 0;
        if (playTransition) {
            transition.SetTrigger("Ejecuta");

            StartCoroutine(WaitForAnimation(transition, "MenuOut"));
            StartCoroutine(WaitForBlock(transition, "MenuIn"));
        }
        else {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                //Debug.Log($"{switcher}");
                if (isDebug) Debug.Log($"Se ha cerrado el menu: {menu}. MenuController.MenuAManejar()");
                //Time.timeScale = 0;
            }
            else
            {
                menu.SetActive(true);
                if (isDebug) Debug.Log($"Se ha abierto el menu: {menu}. MenuController.MenuAManejar()");
            }
            Time.timeScale = 1;
        }
    }

    private IEnumerator WaitForAnimation(Animator animator, string stateName)
    {
        // Espera hasta que la animaci�n est� en el estado deseado
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null; // Espera un frame
        }

        // Espera hasta que la animaci�n termine
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null; // Espera un frame
        }


        transition.SetTrigger("AcabaMedio");

        if (menu.activeSelf)
        {
            menu.SetActive(false);
            //Debug.Log($"{switcher}");
            if (isDebug) Debug.Log($"Se ha cerrado el menu: {menu}. MenuController.MenuAManejar()");
            //Time.timeScale = 0;
        }
        else
        {
            menu.SetActive(true);
            if (isDebug) Debug.Log($"Se ha abierto el menu: {menu}. MenuController.MenuAManejar()");
        }
        Time.timeScale = 1;

    }

    private IEnumerator WaitForBlock(Animator animator, string stateName)
    {
        // Espera hasta que la animaci�n est� en el estado deseado
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null; // Espera un frame
        }

        // Espera hasta que la animaci�n termine
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null; // Espera un frame
        }

        if (menu.activeSelf) {
            Time.timeScale = 0;
        }

    }

    public void cambiarTiempo() {
        Time.timeScale = 1;
    }
}
