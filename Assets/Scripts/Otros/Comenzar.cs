using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comenzar : MonoBehaviour
{
    public GameObject[] objetosMostar;
    public GameObject objetoOcultar;

    public void CambiarMenuInicio()
    {
        foreach (GameObject obj in objetosMostar)
        {
            obj.SetActive(true);
        }

        objetoOcultar.SetActive(false);
    }
        
}

