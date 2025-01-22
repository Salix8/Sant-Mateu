using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private bool isDebug = true;
    [SerializeField] private GameObject transicionmenu;

    private Animator transition;

    void Start()
    {
        transition = transicionmenu.GetComponent<Animator>();

    }

    public void MenuAManejar()
    {
        transition.SetTrigger("Ejecuta");

        
        
        StartCoroutine(WaitForAnimation(transition, "MenuOut"));
    }

    private IEnumerator WaitForAnimation(Animator animator, string stateName)
    {
        // Espera hasta que la animación esté en el estado deseado
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null; // Espera un frame
        }

        // Espera hasta que la animación termine
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null; // Espera un frame
        }


        transition.SetTrigger("AcabaMedio");

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
