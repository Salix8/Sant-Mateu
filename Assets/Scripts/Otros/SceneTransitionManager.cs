using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.GlobalManager;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allGameObjects;
    [SerializeField] private GameObject menuTelefono;       
    [SerializeField] private GameObject mapa;
    [SerializeField] private TextAsset dialogo;

    public ProgresionManager progresionmanager;

    private Dictionary<string, string> zoneMapping = new Dictionary<string, string>
    {
        { "Villores3Presente", "Villores3Pasado"    },
        { "Villores3Pasado", "Villores3Presente"        },
        { "PlazaPresente", "PlazaPasado"            },
        { "PlazaPasado", "PlazaPresente"            },
        { "ArciprestalPresente", "ArciprestalPasado"},
        { "ArciprestalPasado", "ArciprestalPresente"},
        { "MurallaPresente", "MurallaPasado"        },
        { "MurallaPasado", "MurallaPresente"        },
        { "BorrullPresente", "BorrullPasado"        },
        { "BorrullPasado", "BorrullPresente"        },
        { "CallejonPresente", "CallejonPasado"      },
        { "CallejonPasado", "CallejonPresente"      },
        { "HornoPresente", "HornoPasado"            },
        { "HornoPasado", "HornoPresente"            },
        { "FuentePasado", "FuentePresente"          },
        { "FuentePresente", "FuentePasado"          },
        { "PerePresente", "PerePasado"              },
        { "PerePasado", "PerePresente"              },
        { "ConventoPresente", "ConventoPasado"      },
        { "ConventoPasado", "ConventoPresente"      }
    };

    void Start()
    {
        menuTelefono.SetActive(false);
        // mapa.SetActive(false);
    }

    void Awake(){
        if (progresionmanager.puzleQRCompletado) menuTelefono.SetActive(true);
    }

    public void AbrirMenuTelefono() // no se abre cuando hay dialogo, desactiva cambio de Zona
    {

        Debug.Log($"Hay dialogo? {DialogueManager.GetInstance().dialogueIsPlaying}");
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Debug.Log($"1 No hay dialogo");
            // foreach (GameObject obj in allGameObjects)
            // {
            //     Debug.Log($"{obj}");
            //     obj.SetActive(true);
            //
            // }
            GameObject[] objs = GlobalManager.GetInstance().GetPathObjects();
            GlobalManager.GetInstance().SetPathObject(false);
            menuTelefono.SetActive(true);
            Debug.Log($"menuTelefono {menuTelefono}");
        }
    }

    public void MostrarMapa()
    {
        mapa.SetActive(true);
        menuTelefono.SetActive(false);
    }

    
    public void Volver()
    {
        menuTelefono.SetActive(false);
        mapa.SetActive(false);

        GameObject[] objs = GlobalManager.GetInstance().GetPathObjects();
        GlobalManager.GetInstance().SetPathObject(true);
        foreach (GameObject obj in allGameObjects)
        {
            obj.SetActive(true);
            
        }

    }

    public string GetOtherZone(string currentZone)
    {
        if (zoneMapping.ContainsKey(currentZone))
        {
            return zoneMapping[currentZone];
        }
        return null;
    }

    public void OnButtonClick()
    {
        GameObject currentZone = null;
        GameObject[] zonesObjects = GlobalManager.GetInstance().GetZonesObjects();
        foreach (GameObject zone in zonesObjects)
        {
            if (zone.activeSelf) {
                currentZone = zone;
            }
        }
        // Obtenemos el nombre del GameObject actual
        string currentZoneName = currentZone.name;
        Debug.Log(currentZoneName);

        // Usamos el diccionario para obtener el nombre del siguiente GameObject
        string otherZoneName = GetOtherZone(currentZoneName);
        Debug.Log(otherZoneName);

        if (!string.IsNullOrEmpty(otherZoneName))
        {
            GameObject otherZone = null;
            // Buscamos el GameObject de la otra escena
            foreach (GameObject zone in zonesObjects) {
                if (zone.name == otherZoneName) {
                    otherZone = zone;
                }
            }

            if (otherZone != null)
            {
                // Desactivamos la escena actual y activamos la otra escena
                currentZone.SetActive(false);
                otherZone.SetActive(true);
                menuTelefono.SetActive(false);
            }
            else
            {
                Debug.Log("No se encontró el GameObject para la otra escena: " + otherZoneName);
            }
        }
        else
        {
            menuTelefono.SetActive(false);
            DialogueManager.GetInstance().EnterDialogueMode(dialogo);
            Debug.Log("No se encontró una escena correspondiente para: " + currentZoneName);
        }
    }
}