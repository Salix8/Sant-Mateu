using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    [SerializeField] private GameObject nextZone;
    [SerializeField] private AudioClip interactionSound; // Sonido al interactuar
    [SerializeField] private bool playSound = true; // Controla si se debe reproducir el sonido

    public void OnMouseUp()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log("Se ha cambiado a la escena: " + nextZone);
            transform.parent.gameObject.SetActive(false);
            nextZone.gameObject.SetActive(true);
            GlobalManager.GetInstance().SetPathObject(false);
            AudioPasos.GetInstance().PlayOneShot(interactionSound, 0.5f);
        }
    }


    //public IEnumerator DestroyAfterSound(AudioSource audioSource)
    //{
    //    // Esperar a que el audio termine
    //    yield return new WaitForSeconds(audioSource.clip.length);

    //    // Destruir el GameObject
    //    Destroy(audioSource.gameObject);
    //}
}

// puedo utilizar onmouse... se llamacn a los collider y el raton interactua nada que ver con la UI
// Arreglar el event system utilizar el input manager (utilizar los eventos para que vayan los btn)
// Utilizar sistema de eventos por defecto