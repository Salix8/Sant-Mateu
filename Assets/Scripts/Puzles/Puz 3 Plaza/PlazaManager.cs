using System.Collections.Generic;
using UnityEngine;

public class PlazaManager : MonoBehaviour
{
    public static PlazaManager Instance; 
    public List<ObjetoArrastrable> draggableObjects; // Lista de objetos arrastrables

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckCompletion()
    {
        // Verificar si todos los objetos están correctamente colocados
        foreach (var draggableObject in draggableObjects)
        {
            if (!draggableObject.isCorrectlyPlaced)
            {
                return; // Si hay al menos uno mal colocado, no está completo
            }
        }

        
        Debug.Log("¡Minijuego completado!");
    }
}
