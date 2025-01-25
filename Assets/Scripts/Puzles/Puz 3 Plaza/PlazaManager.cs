using System.Collections.Generic;
using UnityEngine;

public class PlazaManager : MonoBehaviour
{
    public static PlazaManager Instance;
    public List<ObjetoArrastrable> draggableObjects; // Lista de objetos arrastrables
    public ProgressionManagerProxy progressionmanagerproxy;

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
        // Verificar si todos los objetos est�n correctamente colocados
        foreach (var draggableObject in draggableObjects)
        {
            if (!draggableObject.isCorrectlyPlaced)
            {
                return; // Si hay al menos uno mal colocado, ya no est� completo
            }
        }
        progressionmanagerproxy.SetComplete(2);
        Debug.Log("�Minijuego completado!");
    }
}
