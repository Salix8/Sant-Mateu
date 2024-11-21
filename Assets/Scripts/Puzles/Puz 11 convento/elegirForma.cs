using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elegirForma : MonoBehaviour
{
    [Header("Forma Base")]
    [SerializeField] private GameObject formaBase;
    [SerializeField] private Sprite[] pilaBase;

    [Header("Forma1")]
    [SerializeField] private GameObject forma1;
    [SerializeField] private Sprite[] pila1;

    [Header("Forma2")]
    [SerializeField] private GameObject forma2;
    [SerializeField] private Sprite[] pila2;

    [Header("Forma3")]
    [SerializeField] private GameObject forma3;
    [SerializeField] private Sprite[] pila3;

    private int fase = 0; //Esto es el iterador (i), indica que imagen toca ahora

    private void Start()
    {
        // formaBase.imagen = pilaBase[fase];
        // forma1.imagen = pila1[fase];
        // forma2.imagen = pila2[fase];
        // forma3.imagen = pila3[fase];
    }
    public void formaElegida(int forma){
        // if(fase >= pilaBase.size() && fase >= pila1.size())
        //     Debug.LogWarning("En formaElegida la fase (" + fase + ") es mayor que la cantidad de imagenes proporcionadas" );

        if (forma == 1)
        {
            //Aqui tienes que cambiar tanto la imagen como la posicion de los botones
            /*
            forma1.transform = new vector2();
            forma1.imagen
            */
        }
    }
}
