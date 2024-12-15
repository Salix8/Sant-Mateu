using System.Collections;
using FifteenPuzzle; 
using System.Collections.Generic;
using UnityEngine;

public class ButtonInstructionsCallejon : MonoBehaviour
{
    private bool switcher = true;

    public GameObject imagen;

    public List<GameObject> objetosaocultar = new List<GameObject>();

    void OnEnable()
    {
        GameManager.OnTileCreated += AddTileToList;
    }

    void OnDisable()
    {
        GameManager.OnTileCreated -= AddTileToList;
    }

    void AddTileToList(GameObject tile)
    {
        if (!objetosaocultar.Contains(tile))
        {
            objetosaocultar.Add(tile);
            Debug.Log($"Tile añadido a la lista: {tile.name}");
        }
    }

    public void change()
    {
        if (switcher)
        {
            imagen.SetActive(true);
            foreach (var v in objetosaocultar)
            {
                v.SetActive(false);
            }
            switcher = false;
            Time.timeScale = 0;
        }
        else
        {
            imagen.SetActive(false);
            foreach (var v in objetosaocultar)
            {
                v.SetActive(true);
            }
            switcher = true;
            Time.timeScale = 1;
        }
    }
}
