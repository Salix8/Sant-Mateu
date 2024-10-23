using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaInteractuable : MonoBehaviour
{
    [SerializeField] private ControlCursor controlCursor;

    private void OnMouseEnter()
    {
        controlCursor.changeCursor("mano");
    }

    private void OnMouseExit()
    {
        controlCursor.changeCursor("normal");
    }
}
