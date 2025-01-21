using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject startButton; // Botón superior
    public GameObject endButton;   // Botón inferior
    public LineRenderer lineRenderer;

    public void DrawLine()
    {
        Vector3 start = startButton.transform.position;
        Vector3 end = endButton.transform.position;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        lineRenderer.sortingOrder = 1;
    }

    public void ClearLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;

            // Forzar una actualización del renderizador
            lineRenderer.enabled = false;
            lineRenderer.enabled = true;
        }
    }

}
