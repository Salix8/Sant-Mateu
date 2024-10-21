using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecogerObjeto : MonoBehaviour
{
    Inventario inventario;

    void Awake()
    {
        inventario = GameObject.Find("Scripts").GetComponent (typeof(Inventario)) as Inventario;
    }

    void OnMouseDown()
    {
        inventario.AgregarObjeto(transform.parent.gameObject);
    }

}
