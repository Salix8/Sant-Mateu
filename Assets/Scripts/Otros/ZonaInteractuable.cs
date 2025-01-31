using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ZonaInteractuable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private string textoInteraccion;

    private void OnMouseEnter()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            ControlCursor.GetInstance().changeCursor("mano");
            textInfo.text = textoInteraccion;
        }
    }

    private void OnMouseExit()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            ControlCursor.GetInstance().changeCursor("normal"); 
            textInfo.text = "";
        }
    }
}
