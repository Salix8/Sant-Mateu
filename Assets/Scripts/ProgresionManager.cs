using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalManager;
using UnityEngine.SceneManagement;
using static ProgresionManager;
using System.Threading;

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
        Puz_11_Convento,    // 11
        Puz_12_Convento     // 12
    }

    [SerializeField] public bool puzleQRCompletado = false;
    [SerializeField] public bool sello1Villores    = false;
    [SerializeField] public bool sello2PlazaMayor  = false;
    [SerializeField] public bool sello3Arciprestal = false;
    [SerializeField] public bool sello4Muralla     = false;
    [SerializeField] public bool sello5Borrull     = false;
    [SerializeField] public bool sello6Judios      = false;
    [SerializeField] public bool sello7Horno       = false;
    [SerializeField] public bool sello8Fuente      = false;
    [SerializeField] public bool sello9SantPere   = false;
    [SerializeField] public bool sello10Convento   = false;
    [SerializeField] public bool sello11Reloj      = false;

    private bool dialogoVillores2 = false;
    private bool dialogoVillores3Pres = false;
    private bool dialogoVillores3Pas = false;
    private bool dialogoPlazaPres = false;
    private bool dialogoPlazaPas = false;
    private bool dialogoArciPres = false;
    private bool dialogoArciPas = false;
    private bool dialogoMurallaPres = false;
    private bool dialogoMurallaPas = false;
    private bool dialogoBorrullPres = false;
    private bool dialogoBorrullPas = false;
    private bool dialogoJudiosPres = false;
    private bool dialogoJudiosPas = false;
    private bool dialogoHornoPres = false;
    private bool dialogoHornoPas = false;
    private bool dialogoFuentePres = false;
    private bool dialogoFuentePas = false;
    private bool dialogoPerePres = false;
    private bool dialogoPerePas = false;
    private bool dialogoConvPres = false;
    private bool dialogoConvPas = false;
    private bool dialogoConvGal = false;

    [SerializeField] public int nivelprogreso      = 0;
    [SerializeField] public int zonactual      = 0;

    [SerializeField] private TextAsset[] dialogosAlCompletarPuzles;

    private int completed;


    public string currentZone = "";


    private static ProgresionManager instance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadMainEmergency();
        }
    }

    

    public void LoadMainEmergency(){
        if (SceneManager.GetActiveScene().buildIndex == 0) SaveZoneState();
        LoadMainScene();

    }

    public void setzona(int n){
        zonactual = n;
    }

    public int getzona(){
        return zonactual;
    }
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
        int nivelsiguiente = 0;
        completed = id;
        switch (id)
        {
            case 0:
                puzleQRCompletado = true;
                Debug.Log($"Puzle QR completado id: {id}.");
                break;
            case 1:
                sello1Villores = true;
                Debug.Log($"Sello {id} (Villores) completado.");
                nivelsiguiente = 1;
                if(nivelsiguiente>nivelprogreso) nivelprogreso = 1;
                break;
            case 2:
                sello2PlazaMayor = true;
                Debug.Log($"Sello {id} (Plaza Mayor) completado.");
                nivelsiguiente = 2;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 2;
                break;
            case 3:
                sello3Arciprestal = true;
                Debug.Log($"Sello {id} (Arciprestal) completado.");
                nivelsiguiente = 3;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 3;
                break;
            case 4:
                sello4Muralla = true;
                Debug.Log($"Sello {id} (Muralla) completado.");
                nivelsiguiente = 4;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 4;
                break;
            case 5:
                sello5Borrull = true;
                Debug.Log($"Sello {id} (Borrull) completado.");
                nivelsiguiente = 5;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 5;
                break;
            case 6:
                sello6Judios = true;
                Debug.Log($"Sello {id} (Judíos) completado.");
                nivelsiguiente = 6;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 6;
                break;
            case 7:
                sello7Horno = true;
                Debug.Log($"Sello {id} (Horno) completado.");
                nivelsiguiente = 7;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 7;
                break;
            case 8:
                sello8Fuente = true;
                Debug.Log($"Sello {id} (Fuente) completado.");
                nivelsiguiente = 8;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 8;
                break;
            case 9:
                sello9SantPere = true;
                Debug.Log($"Sello {id} (Sant Pere) completado.");
                nivelsiguiente = 9;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 9;
                break;
            case 10:
                sello10Convento = true;
                Debug.Log($"Sello {id} (Convento) completado.");
                nivelsiguiente = 10;
                if (nivelsiguiente>nivelprogreso) nivelprogreso = 10;
                break;
            case 11:
                sello11Reloj = true;
                Debug.Log($"Sello {id} (Reloj) completado.");
                break;
            default:
                Debug.LogWarning($"ID no reconocido: {id}");
                break;
        }
        LoadMainScene();
        //RestoreZoneObjectState();

        StartCoroutine(rundialog());

        Debug.Log("Se ha hecho bien");
    }

    IEnumerator rundialog(){

        yield return new WaitForSeconds(2f);
        if (DialogueManager.GetInstance() == null){
            DialogueManager[] dialoguemanager = FindObjectsOfType<DialogueManager>();
            dialoguemanager[0].EnterDialogueMode(dialogosAlCompletarPuzles[completed]);
            Debug.Log("Cosa");
        }else{
            DialogueManager.GetInstance().EnterDialogueMode(dialogosAlCompletarPuzles[completed]);
            Debug.Log("Intento de playear dialogo");
        }
        
    }

    public bool GetComplete(int id)
    {
        switch (id)
        {
            case 0:
                Debug.Log($"puzleQRCompletado: {puzleQRCompletado}. ProgresionManager.GetComplete(id)");
                 return puzleQRCompletado;
            case 1:
                Debug.Log($"Sello {id} (Villores): {sello1Villores}. ProgresionManager.GetComplete(id)");
                 return sello1Villores;
            case 2:
                Debug.Log($"Sello {id} (Plaza Mayor): {sello2PlazaMayor}. ProgresionManager.GetComplete(id)");
                 return sello2PlazaMayor;
            case 3:
                Debug.Log($"Sello {id} (Arciprestal): {sello3Arciprestal}. ProgresionManager.GetComplete(id)");
                 return sello3Arciprestal;
            case 4:
                Debug.Log($"Sello {id} (Muralla): {sello4Muralla}. ProgresionManager.GetComplete(id)");
                 return sello4Muralla;
            case 5:
                Debug.Log($"Sello {id} (Borrull): {sello5Borrull}. ProgresionManager.GetComplete(id)");
                 return sello5Borrull;
            case 6:
                Debug.Log($"Sello {id} (Judíos): {sello6Judios}. ProgresionManager.GetComplete(id)");
                 return sello6Judios;
            case 7:
                Debug.Log($"Sello {id} (Horno): {sello7Horno}. ProgresionManager.GetComplete(id)");
                 return sello7Horno;
            case 8:
                Debug.Log($"Sello {id} (Fuente): {sello7Horno}. ProgresionManager.GetComplete(id)");
                 return sello8Fuente;
            case 9:
                Debug.Log($"Sello {id} (Sant Pere): {sello9SantPere}. ProgresionManager.GetComplete(id)");
                 return sello9SantPere;
            case 10:
                Debug.Log($"Sello {id} (Convento): {sello10Convento}. ProgresionManager.GetComplete(id)");
                 return sello10Convento;
            case 11:
                Debug.Log($"Sello {id} (Reloj): {sello11Reloj}. ProgresionManager.GetComplete(id)");
                return sello11Reloj;
            default:
                Debug.LogWarning($"ID no reconocido: {id} ProgresionManager.GetComplete(id)");
                return false;
        }
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

        StartCoroutine(TransicionarMinijuegoOut(num));

    }

    private IEnumerator TransicionarMinijuegoOut(int num)
    {
        // Encuentra el GameObject de la transición
        GameObject transicionObjectOut = GameObject.Find("TransiciónMinijuegos");
        if (transicionObjectOut != null)
        {
            Animator animator = transicionObjectOut.GetComponent<Animator>();

            if (animator != null)
            {
                // Llamar a la animación de cierre antes de cargar la escena.
                animator.SetTrigger("Finalizar");
            }



            yield return new WaitForSeconds(1f);


            SceneManager.LoadScene(num);
            GlobalManager.GetInstance().RefreshObjects();
        }
        GameObject transicionObjectIn = GameObject.Find("TransiciónMinijuegos");
        if (transicionObjectIn != null)
        {
            Animator animator = transicionObjectIn.GetComponent<Animator>();


            if (animator != null)
            {
                // Llamar a la animación de cierre antes de cargar la escena.
                animator.SetTrigger("Iniciar");

                Debug.Log("Ha iniciado la transicion");
            }

            yield return new WaitForSeconds(1f);

            TransicionMinijuegos transicion = FindObjectOfType<TransicionMinijuegos>();
            if (transicion != null && transicion.instructions != null)
            {
                transicion.instructions.SetActive(true);
            }



        }


    }


    public void LoadMainScene()
    {
        StartCoroutine(TransicionarMainScene());
    }

    private IEnumerator TransicionarMainScene()
    {
        // Encuentra el GameObject de la transición
        GameObject transicionObject = GameObject.Find("TransiciónMinijuegos");
        if (transicionObject != null)
        {
            Animator animator = transicionObject.GetComponent<Animator>();

            if (animator != null)
            {

                animator.SetTrigger("Finalizar");


                yield return new WaitForSeconds(1f);
            }
        }

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

    public bool IsPuzzleCompleted(int id)
    {
        switch (id)
        {
            case 0: return puzleQRCompletado;
            case 1: return sello1Villores;
            case 2: return sello2PlazaMayor;
            case 3: return sello3Arciprestal;
            case 4: return sello4Muralla;
            case 5: return sello5Borrull;
            case 6: return sello6Judios;
            case 7: return sello7Horno;
            case 8: return sello8Fuente;
            case 9: return sello9SantPere;
            case 10: return sello10Convento;
            default: return false;
        }
    }

    public bool GetBoolDialogo(int id)
    {
        switch (id)
        {
            case 0: return dialogoVillores2;
            case 1: return dialogoVillores3Pres;
            case 2: return dialogoVillores3Pas;
            case 3: return dialogoPlazaPres;
            case 4: return dialogoPlazaPas;
            case 5: return dialogoArciPres;
            case 6: return dialogoArciPas;
            case 7: return dialogoMurallaPres;
            case 8: return dialogoMurallaPas;
            case 9: return dialogoBorrullPres;
            case 10: return dialogoBorrullPas;
            case 11: return dialogoJudiosPres;
            case 12: return dialogoJudiosPas;
            case 13: return dialogoHornoPres;
            case 14: return dialogoHornoPas;
            case 15: return dialogoFuentePres;
            case 16: return dialogoFuentePas;
            case 17: return dialogoPerePres;
            case 18: return dialogoPerePas;
            case 19: return dialogoConvPres;
            case 20: return dialogoConvPas;
            case 21: return dialogoConvGal;
            default: return false;
        }
    }

    public void SetBoolDialogo(int id)
    {
        switch (id)
        {
            case 0: 
                dialogoVillores2 = true;
                break;
            case 1: 
                dialogoVillores3Pres = true;
                break;
            case 2: 
                dialogoVillores3Pas = true;
                break;
            case 3: 
                dialogoPlazaPres = true;
                break;
            case 4: 
                dialogoPlazaPas = true;
                break;
            case 5: 
                dialogoArciPres = true;
                break;
            case 6: 
                dialogoArciPas = true;
                break;
            case 7: 
                dialogoMurallaPres = true;
                break;
            case 8: 
                dialogoMurallaPas = true;
                break;
            case 9: 
                dialogoBorrullPres = true;
                break;
            case 10:
                    dialogoBorrullPas = true;
                break;
            case 11:
                    dialogoJudiosPres = true;
                break;
            case 12:
                    dialogoJudiosPas = true;
                break;
            case 13:
                    dialogoHornoPres = true;
                break;
            case 14:
                    dialogoHornoPas = true;
                break;
            case 15:
                    dialogoFuentePres = true;
                break;
            case 16:
                    dialogoFuentePas = true;
                break;
            case 17:
                    dialogoPerePres = true;
                break;
            case 18:
                    dialogoPerePas = true;
                break;
            case 19:
                    dialogoConvPres = true;
                break;
            case 20:
                    dialogoConvPas = true;
                break;
            case 21:
                    dialogoConvGal = true;
                break;
            default: break;
        }
    }


}
