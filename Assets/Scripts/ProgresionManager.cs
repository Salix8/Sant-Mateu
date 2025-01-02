using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalManager;
using UnityEngine.SceneManagement;
using static ProgresionManager;

public class ProgresionManager : MonoBehaviour
{
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

    [SerializeField] public bool puzleQRCompletado = true;
    [SerializeField] public bool sello1Villores    = false;
    [SerializeField] public bool sello2Villores    = false;
    [SerializeField] public bool sello3PlazaMayor  = false;
    [SerializeField] public bool sello4Arciprestal = false;
    [SerializeField] public bool sello5Muralla     = false;
    [SerializeField] public bool sello6Borrull     = false;
    [SerializeField] public bool sello7Judios      = false;
    [SerializeField] public bool sello8Horno       = false;
    [SerializeField] public bool sello9Fuente      = false;
    [SerializeField] public bool sello10SantPere   = false;
    [SerializeField] public bool sello11Convento   = false;
    [SerializeField] public bool sello12Reloj      = false;

    [SerializeField] private TextAsset[] dialogosAlCompletarPuzles;

    string currentZone = "";


    private static ProgresionManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un ProgresionManager en la escena");
            gameObject.SetActive(false);
            // Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static ProgresionManager GetInstance()
    {
        return instance;
    }

    public void SetComplete(int id)
    {
        switch (id)
        {
            case 0:
                puzleQRCompletado = true;
                Debug.Log($"Puzle QR completado id: {id}.");
                break;
            case 1:
                sello1Villores = true;
                Debug.Log($"Sello {id} (Villores) completado.");
                break;
            case 2:
                sello2Villores = true;
                Debug.Log($"Sello {id} (Villores) completado.");
                break;
            case 3:
                sello3PlazaMayor = true;
                Debug.Log($"Sello {id} (Plaza Mayor) completado.");
                break;
            case 4:
                sello4Arciprestal = true;
                Debug.Log($"Sello {id} (Arciprestal) completado.");
                break;
            case 5:
                sello5Muralla = true;
                Debug.Log($"Sello {id} (Muralla) completado.");
                break;
            case 6:
                sello6Borrull = true;
                Debug.Log($"Sello {id} (Borrull) completado.");
                break;
            case 7:
                sello7Judios = true;
                Debug.Log($"Sello {id} (Judíos) completado.");
                break;
            case 8:
                sello8Horno = true;
                Debug.Log($"Sello {id} (Horno) completado.");
                break;
            case 9:
                sello9Fuente = true;
                Debug.Log($"Sello {id} (Fuente) completado.");
                break;
            case 10:
                sello10SantPere = true;
                Debug.Log($"Sello {id} (Sant Pere) completado.");
                break;
            case 11:
                sello11Convento = true;
                Debug.Log($"Sello {id} (Convento) completado.");
                break;
            case 12:
                sello12Reloj = true;
                Debug.Log($"Sello {id} (Reloj) completado.");
                break;
            default:
                Debug.LogWarning($"ID no reconocido: {id}");
                break;
        }

        RestoreZoneObjectState();

        DialogueManager.GetInstance().EnterDialogueMode(dialogosAlCompletarPuzles[id]);
    }

    void ChangeZone(string nextZone)
    {
        Debug.Log($"Escena a la que tendremos que cambiar = {currentZone}.");
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            GlobalManager.GetInstance().SetActiveZone(nextZone);
            GlobalManager.GetInstance().HideMainMenu();
            AudioPasos.GetInstance().PlayOneShot(); //Por defecto suenan los pasos
        }
    }


    void SaveZoneState()
    {
        GameObject[] zonesObjects = GlobalManager.GetInstance().GetZonesObjects();
        foreach (GameObject zone in zonesObjects)
        {
            if (zone.activeSelf)
            {
                currentZone = zone.name;
            }
        }
        Debug.Log($"La zona actual es: {currentZone}; SaveZoneState()");
    }

    // Restaurar el estado de los objetos de la zona
    void RestoreZoneObjectState()
    {
        if (currentZone != null)
            ChangeZone(currentZone);
        else
            Debug.LogWarning($"No se ha encontrado una currentZone: {currentZone}. Script progresionManager.");
    }

    public void LoadSceneMinijuego(int num)
    {
        SaveZoneState();
        Debug.Log($"Se cambia a la escena {(SceneType)num}; LoadSceneMinijuego(int)");
        SceneManager.LoadScene(num);
        GlobalManager.GetInstance().RefreshObjects();
    }

    public void LoadMainScene()
    {
        SceneManager.sceneLoaded += FinishMainSceneLoad;
        SceneManager.LoadScene(0);

    }

    void FinishMainSceneLoad(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= FinishMainSceneLoad;

        GlobalManager.GetInstance().RefreshObjects();
        RestoreZoneObjectState();
    }

    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }


}
