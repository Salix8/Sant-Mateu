using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private float initialX; // Almacena la posición X inicial para mantenerla constante.
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        initialX = transform.position.x; // Guarda la posición X inicial.
    }

    private void OnMouseDrag()
    {
        // Obtiene la posición del mouse en el mundo.
        Vector3 mouseWorldPosition = GetMouseWorldPosition();

        // Actualiza solo el eje Y, manteniendo el eje X constante.
        transform.position = new Vector3(initialX, mouseWorldPosition.y, transform.position.z);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Distancia desde la cámara al objeto.
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
