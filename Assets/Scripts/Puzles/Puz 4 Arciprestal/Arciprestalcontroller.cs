using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArciprestalController : MonoBehaviour
{
    public GameObject lupa;  // Referencia a la imagen de la lupa
    public Canvas canvas;    // Referencia al Canvas
    public GameObject botonInstrucciones; // Botón de instrucciones

    private int restantes = 5; // Número de objetos restantes
    private bool isLupaActive = false; // Estado de la lupa
    private bool isInstruccionesActive = false; // Estado del botón de instrucciones

    // Dimensiones del área límite
    private float areaWidth = 6000f;
    private float areaHeight = 5000f;

    void Update()
    {
        // Detecta cuando el usuario hace clic, solo si las instrucciones no están activas
        if (!isInstruccionesActive)
        {
            if (Input.GetMouseButtonDown(0)) // Cuando se presiona el botón del ratón
            {
                isLupaActive = true;
            }
            else if (Input.GetMouseButtonUp(0)) // Cuando se suelta el botón
            {
                isLupaActive = false;
            }
        }

        // Si la lupa está activa y el botón de instrucciones no está activado, actualiza su posición
        if (isLupaActive && !isInstruccionesActive)
        {
            // Obtener la posición del ratón en la pantalla
            Vector3 mousePos = Input.mousePosition;

            // Convertir la posición del ratón de coordenadas de pantalla a coordenadas locales del Canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePos, canvas.worldCamera, out localPoint);

            // Calcular los límites reales considerando el tamaño de la lupa
            RectTransform lupaRect = lupa.GetComponent<RectTransform>();
            float lupaHalfWidth = lupaRect.rect.width / 2;
            float lupaHalfHeight = lupaRect.rect.height / 2;

            float minX = -areaWidth / 2 + lupaHalfWidth;
            float maxX = areaWidth / 2 - lupaHalfWidth;
            float minY = -areaHeight / 2 + lupaHalfHeight;
            float maxY = areaHeight / 2 - lupaHalfHeight;

            // Limitar la posición de la lupa dentro de los límites calculados
            float clampedX = Mathf.Clamp(localPoint.x, minX, maxX);
            float clampedY = Mathf.Clamp(localPoint.y, minY, maxY);

            // Actualizar la posición local de la lupa en el Canvas
            lupa.GetComponent<RectTransform>().localPosition = new Vector3(clampedX, clampedY, 0);
        }
    }

    // Método para desactivar botones u objetos al interactuar con ellos
    public void añadir(GameObject bt)
    {
        bt.SetActive(false);
        restantes--;

        if (restantes == 0)
        {
            Debug.Log("Has encontrado todos");
        }
    }

    // Método para alternar la visibilidad de las instrucciones y el estado de la lupa
    public void AlternarInstrucciones()
    {
        isInstruccionesActive = !isInstruccionesActive;

        // Aquí podrías añadir la lógica para mostrar o esconder el panel de instrucciones
        // Ejemplo: instruccionesPanel.SetActive(isInstruccionesActive);

        // Si las instrucciones están activas, desactivar la lupa
        if (isInstruccionesActive)
        {
            isLupaActive = false;
        }
    }
}
