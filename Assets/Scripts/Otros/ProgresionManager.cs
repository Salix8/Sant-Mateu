using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProgresionManager : MonoBehaviour
{
    [SerializeField] public bool puzleQRCompletado = false;
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

}
