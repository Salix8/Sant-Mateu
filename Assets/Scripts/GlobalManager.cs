using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;

    private GameObject[] pathObjects;
    private GameObject[] zonesObjects;

    private bool isDebug = false;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un GlobalManager en la escena");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        pathObjects = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Path: " + obj);
        }
        zonesObjects = GameObject.FindGameObjectsWithTag("Escena");
        foreach (GameObject obj in zonesObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Escena: " + obj);
        }
    }

    public void SetPathObject(bool isEnable)
    {
        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(isEnable);
            if (isDebug) Debug.Log("Path: " + obj);
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

    public GameObject[] GetZonesObjects()
    {
        return zonesObjects;
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        Debug.Log($"Se cambia a la escena numero {sceneIndex}");
        SceneManager.LoadScene(sceneIndex);
    }




}
