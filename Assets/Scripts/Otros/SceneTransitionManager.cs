using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    private Dictionary<string, string> sceneMapping = new Dictionary<string, string>
    {
        { "MurallaPresente", "MurallaPasado" },
        { "MurallaPasado", "MurallaPresente" },
        { "BorrullPresente", "BorrullPasado" },
        { "BorrullPasado", "BorrullPresente" }
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