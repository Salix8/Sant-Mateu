using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;

    private GameObject[] pathObjects;
    private GameObject[] scenesObjects;

    //private bool isDebug = false;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un GlobalManager en la escena");
        }
        instance = this;

        pathObjects = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(false);
            Debug.Log(obj);
        }
        scenesObjects = GameObject.FindGameObjectsWithTag("Escena");
        foreach (GameObject obj in scenesObjects)
        {
            obj.SetActive(false);
            Debug.Log(obj);
        }
    }

    public static GlobalManager GetInstance()
    {
        return instance;
    }
    public GameObject[] GetPathObjects()
    {
        return pathObjects;
    }

    public GameObject[] GetScenesObjects()
    {
        return scenesObjects;
    }




}
