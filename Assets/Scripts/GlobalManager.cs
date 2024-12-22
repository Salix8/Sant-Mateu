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
                //Debug.Log(obj);
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

    public void SetZonesFalse()
    {
        zonesObjects = GameObject.FindGameObjectsWithTag("Escena");
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




    
    public void Reestablecer2()
    {
        allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(true);
        }
        pathObjects = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject obj in pathObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Path: " + obj);
        }
        //GameObject escena = GameObject.Find("Villores1"); //Esto no funciona
        //escena.SetActive(true);
    }
    public void Reestablecer3()
    {
        zonesObjects = GameObject.FindGameObjectsWithTag("Escena");
        foreach (GameObject obj in zonesObjects)
        {
            obj.SetActive(false);
            if (isDebug) Debug.Log("Escena: " + obj);
        }
        GameObject escena = GameObject.FindGameObjectWithTag("Zonas");
        Transform[] children = escena.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            //Debug.Log("Hijo encontrado: " + child.name);
            if (child.name == "Villores1")
            {
                child.gameObject.SetActive(true);
                if (isDebug) Debug.Log("Villores1 activado");
            }
        }
        GameObject menuInicial = GameObject.FindGameObjectWithTag("MenuInicial");
        menuInicial.SetActive(false);

    }












    //private List<ObjectState> sceneObjectStates = new List<ObjectState>();

    //public void SaveSceneState(GameObject[] objects)
    //{
    //    sceneObjectStates.Clear(); // Limpiar la lista anterior
    //    foreach (GameObject obj in objects)
    //    {
    //        if (obj != null)
    //        {
    //            ObjectState state = new ObjectState
    //            {
    //                name = obj.name,
    //                isActive = obj.activeSelf
    //            };
    //            sceneObjectStates.Add(state);
    //        }
    //    }
    //    Debug.Log("Estado de la escena guardado.");
    //}

    //public void RestoreSceneState()
    //{
    //    foreach (ObjectState state in sceneObjectStates)
    //    {
    //        GameObject obj = GameObject.Find(state.name);
    //        if (obj != null)
    //        {
    //            obj.SetActive(state.isActive);
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"El objeto {state.name} no se encontró en la escena actual.");
    //        }
    //    }
    //    Debug.Log("Estado de la escena restaurado.");
    //}

    //public void LoadScene2(int num)
    //{
    //    GameObject[] activeObjects = SetActiveObjects(); // Guardar los objetos activos
    //    SaveSceneState(activeObjects); // Guardar el estado antes de cambiar de escena
    //    SceneManager.LoadScene(num);
    //}

    //public void LoadMainScene2()
    //{
    //    SceneManager.LoadScene((int)SceneType.MainScene);
    //    RestoreSceneState(); // Restaurar el estado después de cargar la escena
    //}




}
