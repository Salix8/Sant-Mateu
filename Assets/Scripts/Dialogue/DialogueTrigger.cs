using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{

    //[Header("Signo exclamacion")]
    //[SerializeField] private GameObject exclamationMark;

    //[Header("Ink JSON")]
    //[SerializeField] private TextAsset inkJSON;

    private InputAction clickAction;

    private bool isDebug = true;

    private void Awake()
    {
        if(isDebug) Debug.Log("Activando Dialogue trigger");
        clickAction = InputSystem.actions.FindAction("Click");
        //exclamationMark.SetActive(false);
    }

    public void StartDialogue(TextAsset dialogo)
    {
        Debug.Log("Se ha clicado en " + this.gameObject.name);

        if (isDebug && clickAction.IsPressed()) //Tambien sirve para controles tactiles
        {
            Debug.Log("Se ha clicado en " + this.gameObject.name);
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(dialogo);
        }
    }

}
