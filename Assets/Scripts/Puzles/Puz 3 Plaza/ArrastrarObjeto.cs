using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetoArrastrable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition; // Posici�n inicial del objeto
    private Transform parentTransform;

    public Transform correctPoint; // El punto correcto donde debe estar el objeto
    public bool isCorrectlyPlaced = false;

    private GameObject lastObjectOver; // �ltimo objeto con el que se colision�

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
            startPosition = transform.position; // Guardar la posici�n inicial
            lastObjectOver = null; // Reiniciar el �ltimo objeto sobre el que se pas�

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
            // Verificar si el �ltimo objeto sobre el que se solt� es el punto correcto
            if (lastObjectOver != null && lastObjectOver.transform == correctPoint)
            {
                transform.position = correctPoint.position; // Ajustar al punto correcto
                isCorrectlyPlaced = true; // Marcar como correctamente colocado
                Debug.Log($"{gameObject.name} colocado correctamente.");
                PlazaManager.Instance.CheckCompletion(); // Comprobar si el minijuego est� completo
            }
            else
            {
                // Si no es el lugar correcto, volver a la posici�n inicial
                transform.position = startPosition;
                Debug.Log($"{gameObject.name} no est� en el lugar correcto.");
            }

            // Reactivar el raycast
            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = true;
        }
    }

    // Detectar cuando entra en contacto con otro collider (2D o 3D seg�n tu setup)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        lastObjectOver = collision.gameObject; // Guardar el �ltimo objeto sobre el que se pas�
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lastObjectOver == collision.gameObject)
        {
            lastObjectOver = null; // Limpiar el �ltimo objeto si se sale del �rea
        }
    }
}
