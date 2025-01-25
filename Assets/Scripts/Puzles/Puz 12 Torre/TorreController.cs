using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreController : MonoBehaviour
{
    public ProgressionManagerProxy progressionmanagerproxy;
    public List<GameObject> lista; // Lista de engranajes
    private int seleccionado;
    private bool juegoFinalizado = false; // Estado del juego

    // Start is called before the first frame update
    void Start()
    {
        // Inicializaci�n si es necesario
    }

    // Update is called once per frame
    void Update()
    {
        // L�gica adicional si es necesario
    }

    public void pulsarEngranaje(int engranaje)
    {
        // Verificar si el juego ya finaliz�
        if (juegoFinalizado)
        {
            Debug.Log("El juego ya termin�, no puedes cambiar los engranajes.");
            return;
        }

        // Seleccionar el engranaje
        seleccionado = engranaje;
        foreach (var imagen in lista)
        {
            imagen.SetActive(false);
        }
        lista[engranaje].SetActive(true);
    }

    public void acabar()
    {
        // Verificar si el resultado es correcto
        if (seleccionado == 1)
        {
            Debug.Log("Correcto");
            juegoFinalizado = true; // Marcar el juego como terminado
            progressionmanagerproxy.SetComplete(11);
        }
        else
        {
            Debug.Log("Incorrecto");
        }
    }
}
