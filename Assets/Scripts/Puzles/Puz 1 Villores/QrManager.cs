using System.Collections.Generic;
using UnityEngine;

public class QrManager : MonoBehaviour
{
    public static QrManager Instance;
    public List<MoverPiezas> draggableObjects; // Lista de objetos arrastrables

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
        progressionmanagerproxy.SetComplete(0);
        Debug.Log("�Minijuego completado!");
    }

}
