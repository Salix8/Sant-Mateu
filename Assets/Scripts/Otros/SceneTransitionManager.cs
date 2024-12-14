using System.Collections.Generic;
using UnityEngine;

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
        string aux = GetOtherZone("PlazaPresente");
        Debug.Log(aux);
    }
}