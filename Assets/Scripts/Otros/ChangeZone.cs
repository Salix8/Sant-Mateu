using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    [SerializeField] private GameObject nextZone;

    public void OnMouseUp()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log("Se ha cambiado a la escena: " + nextZone);
            transform.parent.gameObject.SetActive(false);
            nextZone.gameObject.SetActive(true);
            GlobalManager.GetInstance().SetPathObject(false);
        }
    }
}

// puedo utilizar onmouse... se llamacn a los collider y el raton interactua nada que ver con la UI
// Arreglar el event system utilizar el input manager (utilizar los eventos para que vayan los btn)
// Utilizar sistema de eventos por defecto