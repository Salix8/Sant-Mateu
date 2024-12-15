using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.GlobalManager;

public class SceneTransitionManager : MonoBehaviour
{
    private Dictionary<string, string> zoneMapping = new Dictionary<string, string>
    {
        { "Villores3Presente", "Villores3Pasado"    },
        { "Villores3Pasado", "PlazaPresente"        },
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
            }
            else
            {
                Debug.LogWarning("No se encontró el GameObject para la otra escena: " + otherZoneName);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró una escena correspondiente para: " + currentZoneName);
        }
    }
}