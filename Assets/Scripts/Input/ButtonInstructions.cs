using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstructions : MonoBehaviour
{
    private bool switcher = false;

    public GameObject imagen; // Imagen del panel de instrucciones
    private bool areTilesVisible = false; // Estado de visibilidad de las piezas

    // Start is called before the first frame update
    void Start()
    {
        // Esperamos a que se inicialicen los tiles
        Time.timeScale = 0;
        foreach (var tile in FifteenPuzzle.GameManager.Instance.tiles)
        {
            tile.gameObject.SetActive(areTilesVisible);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleInstructionsAndTiles()
    {
        // Cambiar el estado de la imagen (como lo hacía change())
        if (switcher)
        {
            imagen.SetActive(true);
            switcher = false;
            Time.timeScale = 0;
        }
        else
        {
            imagen.SetActive(false);
            switcher = true;
            Time.timeScale = 1;
        }

        // Alternar la visibilidad de los tiles (como lo hacía ToggleTilesVisibility())
        areTilesVisible = !areTilesVisible;

        // Alternar la visibilidad de todos los tiles en el GameManager
        foreach (var tile in FifteenPuzzle.GameManager.Instance.tiles)
        {
            tile.gameObject.SetActive(areTilesVisible);
        }
    }

}
