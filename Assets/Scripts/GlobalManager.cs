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
    private GameObject[] allObjects;
    private List<GameObject> activeObjectsList;

    public enum SceneType
    {
        MainScene,          // 0
        Puz_1_Villores,     // 1
        Puz_2_Villores,     // 2
        Puz_3_Plaza,        // 3
        Puz_4_Arciprestal,  // 4
        Puz_5_Muralla,      // 5
        Puz_6_Borrull,      // 6
        Puz_7_Callejon,     // 7
        Puz_8_Horno,        // 8
        Puz_9_Fuente,       // 9
        Puz_10_Pere,        // 10
        Puz_11_Convento     // 11
    }


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

        allObjects = FindObjectsOfType<GameObject>();
        activeObjectsList = new List<GameObject>();

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

    public GameObject[] SetActiveObjects()
    {
        foreach (GameObject obj in allObjects)
        {
            if (obj.activeInHierarchy) // Verifica si está activo en la jerarquía
            {
                Debug.Log(obj);
                activeObjectsList.Add(obj);
            }
        }
        return activeObjectsList.ToArray();
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

    public GameObject[] GetAllObjects()
    {
        return allObjects;
    }

    public GameObject[] GetActiveObjects()
    {
        return activeObjectsList.ToArray();
    }

    public GameObject[] GetPathObjects()
    {
        return pathObjects;
    }

    public GameObject[] GetZonesObjects()
    {
        return zonesObjects;
    }




    public void LoadScene(SceneType sceneType)      // Convierte el enum a string y carga la escena
    {
        Debug.Log($"Se cambia a la escena {sceneType}");
        SceneManager.LoadScene(sceneType.ToString());
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(SceneType.MainScene.ToString());
    }





}
