using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{

    //[Header("Signo exclamacion")]
    [SerializeField] private GameObject exclamationMark;

    //[Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool isDebug = false;

    private void Awake()
    {
        if(isDebug) Debug.Log("Activando Dialogue trigger");
        exclamationMark.SetActive(false);
    }

    private void Update()
    {
        if (isDebug && InputManager.GetInstance().GetInteractPressed()) //Tambien sirve para controles tactiles
        {
            Debug.Log("Se ha clicado en " + this.gameObject.name);
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            //exclamationMark.SetActive(true); //Esto sera si se pasa el cursor por encima
            if (InputManager.GetInstance().GetInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            exclamationMark.SetActive(false);
        }
    }

}
