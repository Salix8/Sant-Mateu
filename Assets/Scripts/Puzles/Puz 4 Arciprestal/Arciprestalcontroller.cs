using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ArciprestalController : MonoBehaviour
{
    public GameObject lupa;
    public Canvas canvas;
    public GameObject botonInstrucciones;

    // Nuevas referencias
    public TextMeshProUGUI contadorTexto; // Texto del contador
    public GameObject contadorShakeImage; // Imagen que hará el shake

    private int contador = 0; // Contador actual
    private int maxContador = 5; // Número total de botones a encontrar
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

        // Actualizar el contador y animar el shake y el zoom
        ActualizarContador();

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

    private void ActualizarContador()
    {
        contador++; // Incrementar el contador
        contadorTexto.text = $"{contador}/{maxContador}"; // Actualizar el texto del contador

        

        // Lanzar la animación de escalado (zoom)
        StartCoroutine(ScaleImage());
    }



    private IEnumerator ScaleImage()
    {
        float duration = 0.3f; // Duración de la animación
        float scaleFactor = 1.2f; // Factor de escala (10% más grande)

        Vector3 originalScale = contadorShakeImage.transform.localScale; // Guardamos el tamaño original
       

        // Fase de agrandamiento
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float scaleLerp = Mathf.Lerp(1f, scaleFactor, elapsed / duration); // Interpolamos entre el tamaño original y el aumentado
            contadorShakeImage.transform.localScale = originalScale * scaleLerp; // Aplicamos el cambio de escala
            elapsed += Time.deltaTime;
            yield return null;
        }

        
        elapsed = 0f;
        while (elapsed < duration)
        {
            float scaleLerp = Mathf.Lerp(scaleFactor, 1f, elapsed / duration); 
            contadorShakeImage.transform.localScale = originalScale * scaleLerp;
            elapsed += Time.deltaTime;
            yield return null;
        }

      
        contadorShakeImage.transform.localScale = originalScale;
    }

}
