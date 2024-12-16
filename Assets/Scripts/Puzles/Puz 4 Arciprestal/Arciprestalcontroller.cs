using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arciprestalcontroller : MonoBehaviour
{
    public GameObject lupa;  // Referencia a la imagen de la lupa
    public Canvas canvas;    // Referencia al Canvas (si es necesario para la conversión)

    private int restantes = 5;

    private bool isLupaActive = false;

    void Update()
    {
        // Detecta cuando el usuario hace clic
        if (Input.GetMouseButtonDown(0)) // Cuando se presiona el botón del ratón
        {
            isLupaActive = true;
        }
        else if (Input.GetMouseButtonUp(0)) // Cuando se suelta el botón
        {
            isLupaActive = false;
            
        }

        // Si la lupa está activa, actualiza su posición
        if (isLupaActive)
        {
            // Obtener la posición del ratón en la pantalla
            Vector3 mousePos = Input.mousePosition;

            // Convertir la posición del ratón de coordenadas de pantalla a coordenadas locales del Canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePos, canvas.worldCamera, out localPoint);

            // Actualizar la posición local de la lupa en el Canvas
            lupa.GetComponent<RectTransform>().localPosition = localPoint;
        }
    }

    public void añadir(GameObject bt){
        bt.SetActive(false);
        restantes--;
        if (restantes == 0){
            Debug.Log("Has encontrado todos");
        }
    }
}
