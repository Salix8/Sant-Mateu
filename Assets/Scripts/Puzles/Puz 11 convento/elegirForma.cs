using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class elegirForma : MonoBehaviour
{
    
    [SerializeField] private GameObject formaBase; // Objeto de la galleta base

    [Header("Sprites de las Galletas")]
    [SerializeField] private Sprite[] galletasSprites; // Sprites precargados

    [Header("Temporizador")]
    [SerializeField] private float tiempoMaximo = 120f;
    [SerializeField] private TextMeshProUGUI tiempoTexto;

    [Header("Panel de Resultados")]
    [SerializeField] private GameObject panelResultados; // Panel que muestra los resultados
    [SerializeField] private Text textoResultados; // Texto dentro del panel

    private int fase; // Iterador que indica la fase actual
    private List<int> fasesRestantes; // Lista para almacenar las fases restantes aleatoriamente
    private float tiempoRestante;
    private bool juegoActivo;
    private int puntuacion; // Cantidad de galletas acertadas

    private void Start()
    {
        if (galletasSprites == null || galletasSprites.Length == 0)
        {
            Debug.LogError("No se han asignado sprites de las galletas en el Inspector.");
            return;
        }

        // Inicializa el juego
        tiempoRestante = tiempoMaximo;
        juegoActivo = true;
        puntuacion = 0; // Resetea puntuaci�n
        InicializarFases();
        SeleccionarFaseAleatoria();

        // Aseg�rate de que el panel de resultados est� desactivado
        if (panelResultados != null)
        {
            panelResultados.SetActive(false);
        }
    }

    private void Update()
    {
        if (juegoActivo)
        {
            Debug.Log("Tiempo restante: " + tiempoRestante);
            // Actualiza el temporizador
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                ActualizarTiempoTexto();
            }
            else
            {
                tiempoRestante = 0;
                juegoActivo = false; // Finaliza el juego
                FinalizarJuego();
            }
        }
    }

    public void CompruebaModelo(int forma)
    {
        if (juegoActivo) // Solo permite interacciones si el juego est� activo
        {
            if (forma == fase) // Verifica si la forma seleccionada es correcta
            {
                Debug.Log("Forma correcta.");
                puntuacion++; // Incrementa la puntuaci�n

                if (fasesRestantes.Count > 0)
                {
                    SeleccionarFaseAleatoria();
                }
                else
                {
                    // Si no quedan fases, reinicia la lista
                    InicializarFases();
                    SeleccionarFaseAleatoria();
                }
            }
            else
            {
                Debug.Log("Forma incorrecta. Intenta de nuevo.");
            }
        }
    }

    private void SeleccionarFaseAleatoria()
    {
        // Seleccionar un �ndice aleatorio de la lista de fases restantes
        int indiceAleatorio = Random.Range(0, fasesRestantes.Count);

        // Obtener la fase correspondiente
        fase = fasesRestantes[indiceAleatorio];

        // Eliminar la fase seleccionada de la lista para evitar repetir
        fasesRestantes.RemoveAt(indiceAleatorio);

        // Cambiar la galleta correspondiente
        CambiarGalleta(fase);
    }

    private void CambiarGalleta(int fase)
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

    private void ActualizarTiempoTexto()
    {
        if (tiempoTexto != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            tiempoTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
        else
        {
            Debug.LogError("El campo Tiempo Texto no esta asignado!");
        }
    }

    private void InicializarFases()
    {
        fasesRestantes = new List<int>();
        for (int i = 1; i <= galletasSprites.Length; i++)
        {
            fasesRestantes.Add(i);
        }
    }

    private void FinalizarJuego()
    {
        Debug.Log("Tiempo agotado! Fin del juego.");
        // Muestra el panel de resultados
        if (panelResultados != null && textoResultados != null)
        {
            
            panelResultados.SetActive(true); // Activa el panel
            textoResultados.text = "Número de Galletas : " + puntuacion;
        }
        else
        {
            Debug.LogError("Panel o texto de resultados no asignados en el Inspector!");
        }
    }
}
