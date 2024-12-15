using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPipes : MonoBehaviour
{

    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [SerializeField]
    int totalPipes = 0;

    [SerializeField]
    int correctedPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;

        Pipes = new GameObject[totalPipes];

        for (int i = 0; i< Pipes.Length;  i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        correctedPipes +=1;

        if (correctedPipes == 15) //La cantidad de ladrillos puestos bien
        {
            Debug.Log("Completado");
        }
    }
    public void WrongMove()
    {
        correctedPipes -= 1;
    }
}
