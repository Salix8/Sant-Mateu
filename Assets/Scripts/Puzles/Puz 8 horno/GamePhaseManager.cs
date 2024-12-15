using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para manipular el texto de UI
using TMPro;

public class GamePhaseManager : MonoBehaviour
{
    public List<PhaseData> phases; // Lista de fases
    public List<GameObject> gameObjects; // Referencia a los objetos que cambian de posición
    private int currentPhaseIndex = 0;
    public DivisionChecker divisionChecker; // Referencia al script DivisionChecker

    [SerializeField] private Button GroupHintButton; // Referencia al botón
    [SerializeField] private Text groupHintText; // Texto del botón

    private void Start()
    {
        if (GroupHintButton != null)
        {
            groupHintText = GroupHintButton.GetComponentInChildren<Text>();
            UpdateGroupHintText(); // Actualizar el texto inicialmente
        }
    }


    public void LoadNextPhase()
    {
        if (currentPhaseIndex >= phases.Count)
        {
            Debug.Log("¡Has completado todas las fases!");
            groupHintText.text = "¡Has completado todas las fases!";
            return;
        }

        PhaseData currentPhase = phases[currentPhaseIndex];

        // Verificar que las posiciones y rotaciones coincidan con los objetos
        Debug.Log($"Fase {currentPhaseIndex + 1} cargada.");
        Debug.Log($"Objetos: {gameObjects.Count}, Posiciones: {currentPhase.objectPositions.Count}, Rotaciones: {currentPhase.objectRotations.Count}");

        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (i < currentPhase.objectPositions.Count)
            {
                gameObjects[i].transform.localPosition = currentPhase.objectPositions[i];
                Debug.Log($"Objeto {gameObjects[i].name} colocado en: {gameObjects[i].transform.localPosition}");
            }

            if (i < currentPhase.objectRotations.Count)
            {
                gameObjects[i].transform.rotation = currentPhase.objectRotations[i];
                Debug.Log($"Objeto {gameObjects[i].name} rotado a: {currentPhase.objectRotations[i]}");
            }
        }

        // Actualizar los valores de winningCount en DivisionChecker
        divisionChecker.winningCount1 = currentPhase.winningCount1;
        divisionChecker.winningCount2 = currentPhase.winningCount2;

        if (currentPhaseIndex < phases.Count) {
            UpdateGroupHintText(); // Llamada para actualizar el texto
        }

        currentPhaseIndex++;
    }

    private void UpdateGroupHintText()
    {
        if (groupHintText != null)
        {
            PhaseData currentPhase = phases[currentPhaseIndex];
            groupHintText.text = $"Forma grupos de {divisionChecker.winningCount1} y {divisionChecker.winningCount2}";
        }
        else
        {
            Debug.LogWarning("groupText no está asignado en el inspector.");
        }
    }


}
