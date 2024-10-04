using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] private List<GameObject> objetosRecogidos = new List<GameObject> ();

    public void AgregarObjeto(GameObject objeto)
    {
        objetosRecogidos.Add(objeto);
    }

    public void EliminarObjeto(GameObject objeto)
    {
        objetosRecogidos.Remove(objeto);
    }

    public bool TengoObjeto(GameObject objeto)
    {
        return objetosRecogidos.Contains(objeto);
    }
}
