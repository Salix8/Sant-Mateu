using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float initialX; // Almacena la posición X inicial para mantenerla constante.
    private Camera mainCamera;
    private RectTransform canvasRectTransform;

    private void Start()
    {
        mainCamera = Camera.main;
        initialX = transform.position.x; // Guarda la posición X inicial.

        // Obtén el RectTransform del Canvas principal si el objeto está dentro de un Canvas.
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }
    }

    private void OnMouseDrag()
    {
        // Obtiene la posición del mouse en el mundo.
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Ajusta la posición en Y dentro de los límites del Canvas.
        float clampedY = ClampPositionY(mouseWorldPosition.y);

        // Actualiza la posición del objeto, manteniendo X constante.
        transform.position = new Vector3(initialX, clampedY, transform.position.z);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Distancia desde la cámara al objeto.
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private float ClampPositionY(float y)
    {
        if (canvasRectTransform != null)
        {
            // Convierte los límites del Canvas al espacio del mundo.
            Vector3[] canvasCorners = new Vector3[4];
            canvasRectTransform.GetWorldCorners(canvasCorners);
            float minY = canvasCorners[0].y; // Límite inferior.
            float maxY = canvasCorners[1].y; // Límite superior.

            // Limita la posición Y dentro de los valores mínimos y máximos.
            return Mathf.Clamp(y, minY, maxY);
        }

        // Si no hay Canvas, no limites la posición.
        return y;
    }
}
