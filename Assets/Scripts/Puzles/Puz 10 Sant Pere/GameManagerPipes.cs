using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importar el namespace de TextMeshPro

public class GameManagerPipes : MonoBehaviour
{
    public ProgressionManagerProxy progressionmanagerproxy;
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField] private int totalPipes = 0; // Total de tubos
    [SerializeField] private int correctedPipes = 0; // Tubos correctamente colocados

    public TextMeshProUGUI pipesText; 

    
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }

        UpdateUIText(); // Actualizar el texto al inicio
    }

    public void CorrectMove()
    {
        correctedPipes += 1;

        UpdateUIText(); // Actualizar el texto

        if (correctedPipes == totalPipes) // Todos los ladrillos est�n bien colocados
        {
            progressionmanagerproxy.SetComplete(9);
            Debug.Log("�Completado!");
        }
    }

    public void WrongMove()
    {
        correctedPipes -= 1;

        UpdateUIText(); // Actualizar el texto
    }

    private void UpdateUIText()
    {
        // Actualizar el texto con el n�mero de tubos correctos
        pipesText.text = "Ladrillos Correctos: " + correctedPipes + "/" + totalPipes;
    }
}