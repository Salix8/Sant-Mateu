using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProgresionManager : MonoBehaviour
{
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




    private static ProgresionManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Se ha encontrado mas de un ProgresionManager en la escena");
        }
        instance = this;
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
                Debug.Log("Puzle QR completado.");
                break;
            case 1:
                sello1Villores = true;
                Debug.Log("Sello 1 (Villores) completado.");
                break;
            case 2:
                sello2Villores = true;
                Debug.Log("Sello 2 (Villores) completado.");
                break;
            case 3:
                sello3PlazaMayor = true;
                Debug.Log("Sello 3 (Plaza Mayor) completado.");
                break;
            case 4:
                sello4Arciprestal = true;
                Debug.Log("Sello 4 (Arciprestal) completado.");
                break;
            case 5:
                sello5Muralla = true;
                Debug.Log("Sello 5 (Muralla) completado.");
                break;
            case 6:
                sello6Borrull = true;
                Debug.Log("Sello 6 (Borrull) completado.");
                break;
            case 7:
                sello7Judios = true;
                Debug.Log("Sello 7 (Judíos) completado.");
                break;
            case 8:
                sello8Horno = true;
                Debug.Log("Sello 8 (Horno) completado.");
                break;
            case 9:
                sello9Fuente = true;
                Debug.Log("Sello 9 (Fuente) completado.");
                break;
            case 10:
                sello10SantPere = true;
                Debug.Log("Sello 10 (Sant Pere) completado.");
                break;
            case 11:
                sello11Convento = true;
                Debug.Log("Sello 11 (Convento) completado.");
                break;
            case 12:
                sello12Reloj = true;
                Debug.Log("Sello 12 (Reloj) completado.");
                break;
            default:
                Debug.LogWarning("ID no reconocido: " + id);
                break;
        }
    }



}
