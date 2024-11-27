using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject nextScene;

    private void OnMouseUp()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            transform.parent.gameObject.SetActive(false);
            nextScene.gameObject.SetActive(true);
            Debug.Log(nextScene);
            Debug.Log(transform.parent.gameObject);
            Debug.Log(nextScene.gameObject);
        }
    }
}

// puedo utilizar onmouse... se llamacn a los collider y el raton interactua nada que ver con la UI
// Arreglar el event system utilizar el input manager (utilizar los eventos para que vayan los btn)
// Utilizar sistema de eventos por defecto