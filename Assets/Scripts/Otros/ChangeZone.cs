using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{
    [SerializeField] private GameObject nextZone;
    [SerializeField] private AudioClip interactionSound; // Sonido al interactuar
    [SerializeField] private AjusteSonido ajusteSonido; // Referencia al script de ajuste de sonido
    [SerializeField] private GameObject transicionnormal;
    public int niveldesbloqueo;
    public int mapcode;

    public ProgresionManager progresionmanager;


    private Animator transition;


    void Start(){
        transition = transicionnormal.GetComponent<Animator>();

    }


    public void OnMouseUp()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            
            transition.SetTrigger("Ejecuta");

           
            if (ajusteSonido != null)
            {
                float volumen = ajusteSonido.GetVolume(); 
                AudioPasos.GetInstance().PlayOneShot(interactionSound, volumen); 
            }
            // Inicia un Coroutine para ejecutar el cambio después de la animación
            StartCoroutine(WaitForAnimation(transition, "FadeOut")); 
        }
    }

    private IEnumerator WaitForAnimation(Animator animator, string stateName)
    {
        // Espera hasta que la animación esté en el estado deseado
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null; // Espera un frame
        }

        // Espera hasta que la animación termine
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null; // Espera un frame
        }

        progresionmanager.zonactual = mapcode;
        transition.SetTrigger("AcabaMedio");
        Debug.Log("Se ha cambiado a la escena: " + nextZone);
        transform.parent.gameObject.SetActive(false);
        nextZone.gameObject.SetActive(true);
        ControlCursor.GetInstance().changeCursor("normal");
        
    }
}

