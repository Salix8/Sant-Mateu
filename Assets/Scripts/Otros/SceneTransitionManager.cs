using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    private Dictionary<string, string> sceneMapping = new Dictionary<string, string>
    {
        { "Villores1", "Villores2"                  },
        { "Villores2", "Villores3Presente"          },
        { "Villores3Presente", "Villores3Pasado"    },
        { "Villores3Pasado", "Villores1"            },
        { "Villores1", "PlazaPresente"              },
        { "PlazaPresente", "PlazaPasado"            },
        { "PlazaPasado", "PlazaPresente"            },
        { "PlazaPresente", "ArciprestalPresente"    },
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
        { "ConventoPasado", "ConventoPresente"      },
        { "ConventoPresente", "ConventoGalletas"    },
        { "ConventoGalletas", "Villores1"           }


    };

    public string GetOtherScene(string currentScene)
    {
        if (sceneMapping.ContainsKey(currentScene))
        {
            return sceneMapping[currentScene];
        }
        return null;
    }
}