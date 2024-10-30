using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ZonaInteractuable : MonoBehaviour
{
    [SerializeField] private ControlCursor controlCursor;
    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private string textoInteraccion;

    private void OnMouseEnter()
    {
        controlCursor.changeCursor("mano");
        textInfo.text = textoInteraccion;
    }

    private void OnMouseExit()
    {
        controlCursor.changeCursor("normal");
        textInfo.text = "";
    }
}
