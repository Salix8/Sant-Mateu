using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muralla : MonoBehaviour
{
    [SerializeField] private int vida = 3;

    private void hasPerdido()
    {
        if (GlobalManager.GetInstance() != null)
            GlobalManager.GetInstance().LoadMainScene();
        else
            Debug.LogWarning($"No se ha creado ninguna instancia del GlobalManger. Puz 5 muralla");

        if (ProgresionManager.GetInstance() != null)
            ProgresionManager.GetInstance().SetComplete(5);
        else
            Debug.LogWarning($"No se ha creado ninguna instancia del ProgresionManger. Puz 5 muralla");
    }

    public int GetVidaMuralla()
    {
        return vida;
    }

    public void SetVidaMuralla(int valor)
    {
        if(vida > 0)
            vida += valor;
        else
            hasPerdido();
        Debug.Log($"Tienes {vida} vidas de 3");
    }
}
