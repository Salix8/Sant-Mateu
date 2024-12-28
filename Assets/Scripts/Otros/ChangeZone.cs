using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    [SerializeField] private GameObject nextZone;
    [SerializeField] private AudioClip interactionSound; // Sonido al interactuar
    [SerializeField] private AjusteSonido ajusteSonido; // Referencia al script de ajuste de sonido

    

    public void OnMouseUp()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            Debug.Log("Se ha cambiado a la escena: " + nextZone);
            transform.parent.gameObject.SetActive(false);
            nextZone.gameObject.SetActive(true);
            ControlCursor.GetInstance().changeCursor("normal");
            GlobalManager.GetInstance().SetPathObject(false);

            // Aquí obtenemos el volumen ajustado y lo aplicamos al sonido
            if (ajusteSonido != null)
            {
                float volumen = ajusteSonido.GetVolume(); 
                AudioPasos.GetInstance().PlayOneShot(interactionSound, volumen); 
            }
            
        }
    }
}
