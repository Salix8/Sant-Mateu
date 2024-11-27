using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elegirForma : MonoBehaviour
{
    [Header("Forma Base")]
    [SerializeField] private GameObject formaBase; // Objeto de la galleta base

    [Header("Sprites de las Galletas")]
    [SerializeField] private Sprite[] galletasSprites; // Sprites precargados

    private int fase = 1; // Iterador que indica la fase actual
    private List<int> fasesRestantes; // Lista para almacenar las fases restantes aleatoriamente

    private void Start()
    {
        if (galletasSprites == null || galletasSprites.Length == 0)
        {
            Debug.LogError("No se han asignado sprites de las galletas en el Inspector.");
            return;
        }

        // Generar una lista con todas las fases posibles
        fasesRestantes = new List<int>();
        for (int i = 1; i <= galletasSprites.Length; i++)
        {
            fasesRestantes.Add(i);
        }

        // Inicializa la primera fase aleatoria
        SeleccionarFaseAleatoria();
    }

    public void CompruebaModelo(int forma)
    {
        if (forma == fase) // Verifica si la forma seleccionada es la correcta
        {
            if (fasesRestantes.Count > 0) // Si quedan fases por jugar
            {
                SeleccionarFaseAleatoria();
            }
            else
            {
                Debug.Log("¡Juego terminado! No hay más fases.");
            }
        }
        else
        {
            Debug.Log("Forma incorrecta. Intenta de nuevo.");
        }
    }

    private void SeleccionarFaseAleatoria()
    {
        // Seleccionar un índice aleatorio de la lista de fases restantes
        int indiceAleatorio = Random.Range(0, fasesRestantes.Count);

        // Obtener la fase correspondiente
        fase = fasesRestantes[indiceAleatorio];

        // Eliminar la fase seleccionada de la lista para evitar repetir
        fasesRestantes.RemoveAt(indiceAleatorio);

        // Cambiar la galleta correspondiente
        CambiarGalleta(fase);
    }

    public void CambiarGalleta(int fase)
    {

        if (fase - 1 >= 0 && fase - 1 < galletasSprites.Length)
        {
            formaBase.GetComponent<Image>().sprite = galletasSprites[fase - 1];
        }
        else
        {
            Debug.LogError("Fase fuera de rango o sprites no asignados.");
        }
    }
}
