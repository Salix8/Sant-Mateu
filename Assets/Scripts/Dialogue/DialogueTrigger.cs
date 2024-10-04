using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    //[Header("Signo exclamacion")]
    [SerializeField] private GameObject exclamationMark;

    //[Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool isPressed = false;

    private bool isDebug = true;

    private void Awake()
    {
        if(isDebug) Debug.Log("Activando Dialogue trigger");
        isPressed = false;
        exclamationMark.SetActive(false);
    }

    private void Update()
    {
        if (isPressed && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            exclamationMark.SetActive(true);
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

    private void OnMouseDown()
    {
        Debug.Log("Funciona");
        isPressed = true;
    }
}
