using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArciprestalController : MonoBehaviour
{
    public GameObject lupa;
    public Canvas canvas;
    public GameObject botonInstrucciones;

    private int restantes = 5;
    private bool isLupaActive = false;
    private bool isInstruccionesActive = true;

    private float areaWidth = 6000f;
    private float areaHeight = 5000f;

    void Update()
    {
        if (!isInstruccionesActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isLupaActive = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isLupaActive = false;
            }
        }

        if (isLupaActive && !isInstruccionesActive)
        {
            Vector3 mousePos = Input.mousePosition;

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePos, canvas.worldCamera, out localPoint);

            RectTransform lupaRect = lupa.GetComponent<RectTransform>();
            float lupaHalfWidth = lupaRect.rect.width / 2;
            float lupaHalfHeight = lupaRect.rect.height / 2;

            float minX = -areaWidth / 2 + lupaHalfWidth;
            float maxX = areaWidth / 2 - lupaHalfWidth;
            float minY = -areaHeight / 2 + lupaHalfHeight;
            float maxY = areaHeight / 2 - lupaHalfHeight;

            float clampedX = Mathf.Clamp(localPoint.x, minX, maxX);
            float clampedY = Mathf.Clamp(localPoint.y, minY, maxY);

            lupa.GetComponent<RectTransform>().localPosition = new Vector3(clampedX, clampedY, 0);
        }
    }

    public void añadir(GameObject bt)
    {
        StartCoroutine(AnimateButton(bt));
        restantes--;

        if (restantes == 0)
        {
            Debug.Log("Has encontrado todos");
        }
    }

    public void AlternarInstrucciones()
    {
        isInstruccionesActive = !isInstruccionesActive;

        if (isInstruccionesActive)
        {
            isLupaActive = false;
        }
    }

    private IEnumerator AnimateButton(GameObject button)
    {
        // Añadir un borde amarillo al botón
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = Color.yellow;
        }

        // Guardar la escala inicial del botón
        Vector3 originalScale = button.transform.localScale;

        // Fase de agrandamiento
        float duration = 0.2f; // Duración de la animación
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 1.5f, elapsedTime / duration);
            button.transform.localScale = originalScale * scale;
            yield return null;
        }

        // Fase de encogimiento y desvanecimiento
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float scale = Mathf.Lerp(1.5f, 0f, elapsedTime / duration);
            button.transform.localScale = originalScale * scale;

            // Reducir la opacidad gradualmente
            if (buttonImage != null)
            {
                Color color = buttonImage.color;
                color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                buttonImage.color = color;
            }

            yield return null;
        }

        // Desactivar el botón después de la animación
        button.SetActive(false);
    }
}
