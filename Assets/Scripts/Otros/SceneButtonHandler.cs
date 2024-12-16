using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalManager;
using UnityEngine.SceneManagement;

public class SceneButtonHandler : MonoBehaviour
{

    [SerializeField] private SceneType sceneType;

   

    public void LoadSceneToMini()
    {

        GameObject[] activeObjects = GlobalManager.GetInstance().SetActiveObjects();

        Debug.Log($"Se cambia de la zona principal al Puzle {sceneType}");
        //Debug.Log(activeObjects);
        ProgresionManager.GetInstance().SaveZoneObjectState(activeObjects);
        SceneManager.LoadScene((int)sceneType);
    }

    public void LoadMiniToScene()
    {
        GlobalManager.GetInstance().LoadMainScene();
        Debug.Log($"Se cambia del Puzle {SceneManager.GetActiveScene().name} a la zona principal");
        ProgresionManager.GetInstance().RestoreZoneObjectState();
        //Debug.LogWarning(activeObjects);
        //foreach ( GameObject obj in activeObjects )
        //{
        //    Debug.Log(obj);
        //}
    }
}
