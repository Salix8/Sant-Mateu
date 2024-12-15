using UnityEngine;
using UnityEngine.EventSystems;

public class ObjetoArrastrable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentTransform;

    public Transform correctPoint; // El punto correcto donde debe estar el objeto
    public bool isCorrectlyPlaced = false;

    private void Start()
    {
        parentTransform = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isCorrectlyPlaced) 
        {
            startPosition = transform.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isCorrectlyPlaced)
        {
            
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isCorrectlyPlaced)
        {
            
            if (Vector3.Distance(transform.position, correctPoint.position) < 50f) // Ajusta la tolerancia
            {
                transform.position = correctPoint.position; // Colocar en el punto correcto
                isCorrectlyPlaced = true;
                Debug.Log($"{gameObject.name} colocado correctamente.");
                PlazaManager.Instance.CheckCompletion(); // Revisar si el minijuego está completo
            }
            else
            {
                // Volver a la posición inicial si es incorrecto
                transform.position = startPosition;
                Debug.Log($"{gameObject.name} no está en el lugar correcto.");
            }
        }
    }
}
