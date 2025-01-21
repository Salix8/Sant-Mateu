using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    private static GlobalManager instance;

    private GameObject[] pathObjects;
    private GameObject[] zonesObjects;
    private GameObject[] allObjects;
    private List<GameObject> activeObjectsList;
    [SerializeField] private GameObject[] activeObjectsDefault;

    [System.Serializable]
    public class ObjectState
    {
        public string name; // Nombre del objeto
        public bool isActive; // Estado del objeto
    }


    private bool isDebug = false;


    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un GlobalManager en la escena");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeObjects();
    }

    void InitializeObjects()
    {
        Debug.Log("InitializeObjects");
        allObjects = FindObjectsOfType<GameObject>();
        activeObjectsList = new List<GameObject>();

        pathObjects = GameObject.FindGameObjectsWithTag("Path");
        zonesObjects = GameObject.FindGameObjectsWithTag("Escena");

        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Path: " + obj);
        }
        foreach (GameObject obj in zonesObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Escena: " + obj);
        }

    }
    public void RefreshObjects()
    {
        InitializeObjects();
    }
    public void SetActiveZone(string zoneName)
    {
        foreach (GameObject obj in zonesObjects)
        {
            if (obj.name == zoneName)
            {
                obj.SetActive(true);
                if (isDebug) Debug.Log("Escena: " + obj);
            }
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

    public void SetZonesFalse()
    {
        foreach (GameObject obj in zonesObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Escena: " + obj);
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

    public GameObject[] GetActiveObjectsDefault()
    {
        return activeObjectsDefault;
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





    public void HideMainMenu()
    {
        GameObject menuInicial = GameObject.FindGameObjectWithTag("MenuInicial");
        menuInicial.SetActive(false);
        GameObject[] startObjects = GameObject.FindGameObjectWithTag("ObjetosInicio").GetComponent<ObjectCollection>().Objects;
        foreach (GameObject obj in startObjects)
        {
            obj.SetActive(true);
        }
    }


}
