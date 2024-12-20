/* using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTravel : MonoBehaviour
{
    private SceneTransitionManager sceneTransitionManager;
    private bool isEnable = true;

    void Start()
    {
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
        //transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Si hay dialogo se desactiva
        if (isEnable && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            transform.gameObject.SetActive(true);
        }
        else
        {
            transform.gameObject.SetActive(false);
        }

        // Cuando se complete el QR se activara
        if (ProgresionManager.GetInstance().puzleQRCompletado)
        {
            isEnable = true;
            transform.gameObject.SetActive(isEnable);
        }*/
/*    }

    public void OnButtonClick()
    {
        if (isEnable)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string otherScene = sceneTransitionManager.GetOtherScene(currentScene);

            if (!string.IsNullOrEmpty(otherScene))
            {
                Debug.Log($"Cambiando a la otra escena: {otherScene}");
                SceneManager.LoadScene(otherScene);
            }
            else
            {
                Debug.Log("No hay una escena definida para cambiar desde la escena actual.");
            }
        }
        else
        {
            // Dialogo de Silvia
            Debug.Log("No se puede cambiar a la escena del pasado todav�a.");
        }
    }
} */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeTravel : MonoBehaviour
{
    private SceneTransitionManager sceneTransitionManager;
    private bool isEnable = false; // Comienza desactivado hasta que el QR esté completado.
    private GameObject[] sceneObjects;

    void Start()
    {
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
        sceneObjects = GameObject.FindGameObjectsWithTag("Escena");
        //Debug.Log(sceneObjects);
    }

    void Update()
    {
        // Verifica si el puzzle del QR está completado para habilitar la transición.
        if (ProgresionManager.GetInstance().puzleQRCompletado && !isEnable)
        {
            isEnable = true;
            Debug.Log("Puzzle QR completado. Transición habilitada.");
        }
    }

    public void OnButtonClick()
    {
        //string currentScene = "";
        //// Solo permite cambiar de escena si `isEnable` está en true.
        //if (isEnable)
        //{
        //    foreach (GameObject obj in sceneObjects)
        //    {
        //        Debug.Log(obj.activeSelf);
        //        if (obj.activeSelf) {
        //            Debug.Log(obj.name);
        //            currentScene = obj.name;
        //        }
        //    }
        //    //string currentScene = SceneManager.GetActiveScene().name;
        //    string otherScene = sceneTransitionManager.GetOtherScene(currentScene);

        //    if (!string.IsNullOrEmpty(otherScene))
        //    {
        //        Debug.Log($"Cambiando a la otra escena: {otherScene}");
        //        SceneManager.LoadScene(otherScene); // Carga la escena siguiente.
        //    }
        //    else
        //    {
        //        Debug.Log("No hay una escena definida para cambiar desde la escena actual.");
        //    }
        //}
        //else
        //{
        //    Debug.Log("No se puede cambiar de escena todavía. Completa el puzzle QR.");
        //}
    }
}
