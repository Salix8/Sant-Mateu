using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorreController : MonoBehaviour
{

    public List<GameObject> lista;
    private int seleccionado;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pulsarEngranaje(int engranaje){
        seleccionado = engranaje;
        foreach(var imagen in lista){
            imagen.SetActive(false);
        }
        lista[engranaje].SetActive(true);

    }

    public void acabar(){
        if (seleccionado == 1){
            Debug.Log("Correcto");
        }
        else{
            Debug.Log("Incorrecto");
        }
    }
}
