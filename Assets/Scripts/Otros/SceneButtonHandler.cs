using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalManager;
using UnityEngine.SceneManagement;

public class SceneButtonHandler : MonoBehaviour
{
    public void LoadScene(SceneType sceneType)
    {
        Debug.Log($"Se cambia a la escena {sceneType}");
        SceneManager.LoadScene(sceneType.ToString());
    }
}
