using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MoverPiezas : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector2 targetPosition;     // Posición objetivo de cada pieza

    // Lista para almacenar las referencias de todas las piezas
    private static List<MoverPiezas> piezas = new List<MoverPiezas>();
    private static bool puzzleCompletado = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        // Guardamos la posición objetivo como la posición inicial de cada pieza
        targetPosition = rectTransform.anchoredPosition;

        // Agregar la pieza a la lista de todas las piezas
        piezas.Add(this);
    }

    private void Start()
    {
        // Mueve cada pieza a una posición aleatoria al inicio
        float randomX = Random.Range(-400, 390);
        float randomY = Random.Range(-140, 140);
        rectTransform.anchoredPosition = new Vector2(randomX, randomY);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Clic en la pieza: {gameObject.name}");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;  // Hace la pieza un poco transparente durante el arrastre
        canvasGroup.blocksRaycasts = false;
        Debug.Log($"Iniciando arrastre de la pieza: {gameObject.name}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Actualiza la posición de la pieza mientras se arrastra
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        Debug.Log($"Finalizando arrastre de la pieza: {gameObject.name} en la posición {rectTransform.anchoredPosition}");

        // Verificar si la pieza está cerca de su posición objetivo
        float snapDistance = 10f;
        if (Vector2.Distance(rectTransform.anchoredPosition, targetPosition) < snapDistance)
        {
            // Colocar la pieza en su posición correcta
            rectTransform.anchoredPosition = targetPosition;
            enabled = false;  // Desactivar el script para evitar más movimientos de esta pieza

            // Comprobar si el puzzle está completo
            if (!puzzleCompletado)
            {
                CheckPuzzleCompletion();
            }
        }
    }

    private void CheckPuzzleCompletion()
    {
        Debug.Log("Comprobando si el puzzle está completo...");

        // Verifica cada pieza y sus vecinos
        foreach (MoverPiezas pieza in piezas)
        {
            // Verificar que esta pieza está en su posición objetivo
            if (Vector2.Distance(pieza.rectTransform.anchoredPosition, pieza.targetPosition) >= 10f)
            {
                Debug.Log($"Pieza {pieza.gameObject.name} no está en su posición correcta.");
                return; // Si una pieza no está en su posición, el puzzle no está completo
            }

            // Verificar que los vecinos de esta pieza están en su posición correcta
            if (!VerificarVecinos(pieza))
            {
                Debug.Log($"Los vecinos de la pieza {pieza.gameObject.name} no están en las posiciones correctas.");
                return; // Si los vecinos no están en sus posiciones correctas, el puzzle no está completo
            }
        }

        // Si todas las piezas están en posición correcta con sus vecinos
        puzzleCompletado = true;
        Debug.Log("¡Puzzle completado correctamente!");
    }

    private bool VerificarVecinos(MoverPiezas pieza)
    {
        // Definimos las posiciones relativas de los vecinos (arriba, abajo, izquierda, derecha)
        Dictionary<string, Vector2> posicionesRelativas = new Dictionary<string, Vector2>
        {
            { "Arriba", new Vector2(0, 100) },
            { "Abajo", new Vector2(0, -100) },
            { "Izquierda", new Vector2(-100, 0) },
            { "Derecha", new Vector2(100, 0) }
        };

        // Buscar si hay vecinos en las posiciones relativas
        foreach (var vecino in posicionesRelativas)
        {
            Vector2 posicionEsperada = pieza.targetPosition + vecino.Value;
            bool vecinoCorrecto = false;

            // Verifica si alguna otra pieza está en esta posición esperada
            foreach (MoverPiezas otraPieza in piezas)
            {
                if (otraPieza != pieza && Vector2.Distance(otraPieza.targetPosition, posicionEsperada) < 10f)
                {
                    vecinoCorrecto = true;
                    Debug.Log($"La pieza {pieza.gameObject.name} tiene a su vecino {otraPieza.gameObject.name} en la posición {vecino.Key}.");
                    break;
                }
            }

            // Si no hay vecino en la posición correcta
            if (!vecinoCorrecto)
            {
                Debug.Log($"La pieza {pieza.gameObject.name} no tiene un vecino en la posición {vecino.Key}.");
                return false;
            }
        }

        // Si todos los vecinos están en posición correcta
        return true;
    }
}
