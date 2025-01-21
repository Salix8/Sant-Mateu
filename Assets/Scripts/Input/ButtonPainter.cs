using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonPainter : MonoBehaviour
{
    private List<Vector2> points;
    private LineRenderer line;
    public GameObject lienzo;
    private Vector3 previousPosition;
    public float minDistance = 0.1f;
    private bool switcher = true;
    public GameObject botonInstrucciones; // Botón de instrucciones
    public GameObject botonBorrar; // Botón de borrar
    public GameObject botonlisto; // Botón de borrar
    public GameObject imagen;
    public GameObject linePrefab;
    private bool isInstruccionesActive = false; // Estado del botón de instrucciones
    Line activeLine;

    void Start()
    {
        // Asegurarnos de que el botón de borrar esté desactivado al iniciar
        if (botonBorrar != null)
        {
            botonBorrar.SetActive(false);
        }
    }

    void Update()
    {
        if (!switcher)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newLine = Instantiate(linePrefab);
                newLine.transform.SetParent(imagen.transform);
                activeLine = newLine.GetComponent<Line>();
            }
            if (Input.GetMouseButtonUp(0))
            {
                activeLine = null;
            }

            if (activeLine != null)
            {
                Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                activeLine.UpdateLine(mousepos);
            }
        }
    }

    public void change()
    {
        if (switcher)
        {
            // Activar el lienzo
            imagen.SetActive(true);
            switcher = false;

            // Mostrar el botón de borrar
            if (botonBorrar != null)
            {
                botonBorrar.SetActive(true);
            }

            // Desactivar el botón de instrucciones
            botonInstrucciones.GetComponent<Button>().interactable = false;
            botonlisto.GetComponent<Button>().interactable = false;
        }
        else
        {
            // Desactivar el lienzo
            imagen.SetActive(false);
            switcher = true;

            // Ocultar el botón de borrar
            if (botonBorrar != null)
            {
                botonBorrar.SetActive(false);
            }

            // Reactivar el botón de instrucciones
            botonInstrucciones.GetComponent<Button>().interactable = true;
            botonlisto.GetComponent<Button>().interactable = true;
        }
    }

    // Método para borrar el lienzo
    public void ClearCanvas()
    {
        // Elimina todos los hijos del objeto 'imagen'
        foreach (Transform child in imagen.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
