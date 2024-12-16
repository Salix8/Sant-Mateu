using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetoArrastrable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition; // Posición inicial del objeto
    private Transform parentTransform;

    public Transform correctPoint; // El punto correcto donde debe estar el objeto
    public bool isCorrectlyPlaced = false;

    private GameObject lastObjectOver; // Último objeto con el que se colisionó

    private CanvasGroup canvasGroup; // Para manejar el raycast en UI

    private void Start()
    {
        parentTransform = transform.parent; // Guardar el padre del objeto
        canvasGroup = GetComponent<CanvasGroup>(); // Obtener el CanvasGroup
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isCorrectlyPlaced)
        {
            startPosition = transform.position; // Guardar la posición inicial
            lastObjectOver = null; // Reiniciar el último objeto sobre el que se pasó

            // Desactivar el raycast para evitar conflictos mientras se arrastra
            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isCorrectlyPlaced)
        {
            // Mover el objeto con el puntero (para UI usa RectTransform)
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                parentTransform.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out Vector3 globalMousePos))
            {
                transform.position = globalMousePos;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isCorrectlyPlaced)
        {
            // Verificar si el último objeto sobre el que se soltó es el punto correcto
            if (lastObjectOver != null && lastObjectOver.transform == correctPoint)
            {
                transform.position = correctPoint.position; // Ajustar al punto correcto
                isCorrectlyPlaced = true; // Marcar como correctamente colocado
                Debug.Log($"{gameObject.name} colocado correctamente.");
                PlazaManager.Instance.CheckCompletion(); // Comprobar si el minijuego está completo
            }
            else
            {
                // Si no es el lugar correcto, volver a la posición inicial
                transform.position = startPosition;
                Debug.Log($"{gameObject.name} no está en el lugar correcto.");
            }

            // Reactivar el raycast
            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = true;
        }
    }

    // Detectar cuando entra en contacto con otro collider (2D o 3D según tu setup)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        lastObjectOver = collision.gameObject; // Guardar el último objeto sobre el que se pasó
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lastObjectOver == collision.gameObject)
        {
            lastObjectOver = null; // Limpiar el último objeto si se sale del área
        }
    }
}
