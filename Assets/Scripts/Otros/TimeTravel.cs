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
    [SerializeField] private SceneTransitionManager sceneTransitionManager;
    private bool isEnable = false; // Comienza desactivado hasta que el QR esté completado.
    [SerializeField] private GameObject[] sceneObjects;
    

    void Start()
    {
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
        if (sceneTransitionManager == null)
        {
            Debug.LogError("No se encontró un objeto con SceneTransitionManager en la escena.");
        }
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
        Debug.Log(GameObject.FindGameObjectsWithTag("Escena"));
        // Solo permite cambiar de escena si `isEnable` está en true.
        if (isEnable)
        {
            string currentScene = "";
            foreach (GameObject obj in sceneObjects)
            {
                if (obj.activeSelf) {
                    currentScene = obj.name;
                }
            }
            //currentScene = SceneManager.GetActiveScene().name;
            string otherScene = sceneTransitionManager.GetOtherScene(currentScene);
            Debug.Log(otherScene);
            Debug.Log(!string.IsNullOrEmpty(otherScene));

            if (!string.IsNullOrEmpty(otherScene))
            {
                Debug.Log($"Cambiando a la otra escena: {otherScene}");
                GameObject current = GameObject.Find($"/Canvas/Escenas/{currentScene}");
                current.SetActive(false);
                GameObject other = GameObject.Find($"/Canvas/Escenas/{otherScene}");
                other.SetActive(true);
            }
            else
            {
                Debug.Log("No hay una escena definida para cambiar desde la escena actual.");
            }
        }
        else
        {
            Debug.Log("No se puede cambiar de escena todavía. Completa el puzzle QR.");
        }
    }
}
