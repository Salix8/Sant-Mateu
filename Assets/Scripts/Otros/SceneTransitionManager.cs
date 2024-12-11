using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    private Dictionary<string, string> sceneMapping = new Dictionary<string, string>
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

    public string GetOtherScene(string currentScene)
    {
        Debug.Log(sceneMapping[currentScene]);
        Debug.Log(sceneMapping.ContainsKey(currentScene));
        if (sceneMapping.ContainsKey(currentScene))
        {
            return sceneMapping[currentScene];
        }
        return null;
    }
}