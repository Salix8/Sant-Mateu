using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VilloresPasadoManager : MonoBehaviour
{
    public Button[] mainButtons; // Botones principales
    public GameObject secondaryButtonsPanel; // Panel que contiene los botones secundarios
    public Button[] secondaryButtons; // Botones secundarios

    private int[] values = new int[4]; // Vector para almacenar los valores asignados
    private bool[] usedNumbers = new bool[4]; // Controla qué números están usados
    private int activeMainButtonIndex = -1; // Índice del botón principal activo

    private int[] result = new int[4]; 

    void Start()
    {
        result[0] = 2;
        result[1] = 3;
        result[2] = 1;
        result[3] = 4;

        // Verificar referencias nulas
        if (mainButtons == null || mainButtons.Length == 0)
        {
            
            return;
        }

        if (secondaryButtons == null || secondaryButtons.Length == 0)
        {
           
            return;
        }

        if (secondaryButtonsPanel == null)
        {
            
            return;
        }

        // Inicializamos el estado de los botones y el vector
        for (int i = 0; i < mainButtons.Length; i++)
        {
            int index = i; // Necesario para evitar problemas con closures
            values[i] = -1; // -1 indica que no hay número asignado

            mainButtons[i].onClick.AddListener(() => OnMainButtonClick(index));
        }

        for (int i = 0; i < secondaryButtons.Length; i++)
        {
            int number = i + 1; // Los números son del 1 al 4
            secondaryButtons[i].onClick.AddListener(() => OnSecondaryButtonClick(number));
        }

        secondaryButtonsPanel.SetActive(false); // Ocultamos los botones secundarios al inicio
    }

    void OnMainButtonClick(int index)
    {
        if (values[index] != -1) // Si ya hay un número asignado
        {
            int previousValue = values[index];
            values[index] = -1; // Eliminamos el número asignado
            usedNumbers[previousValue - 1] = false; // Marcamos el número como disponible
            var tmpText = mainButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.text = ""; // Limpiamos el texto del botón
            }
        }
        else
        {
            secondaryButtonsPanel.SetActive(true); // Mostramos los botones secundarios

            // Activamos/desactivamos botones según los números usados
            for (int i = 0; i < secondaryButtons.Length; i++)
            {
                secondaryButtons[i].interactable = !usedNumbers[i];
            }

            activeMainButtonIndex = index; // Guardamos el índice del botón principal activo
        }
    }

    void OnSecondaryButtonClick(int number)
    {
        if (activeMainButtonIndex != -1)
        {
            values[activeMainButtonIndex] = number; // Asignamos el número al vector
            usedNumbers[number - 1] = true; // Marcamos el número como usado

            if (mainButtons[activeMainButtonIndex] != null)
            {
                var tmpText = mainButtons[activeMainButtonIndex].GetComponentInChildren<TextMeshProUGUI>();
                if (tmpText != null)
                {
                    tmpText.text = number.ToString(); // Mostramos el número
                }
                
            }
           

            if (secondaryButtonsPanel != null)
            {
                secondaryButtonsPanel.SetActive(false); // Ocultamos los botones secundarios
            }
            

            activeMainButtonIndex = -1; // Reseteamos el índice activo
        }
    
    }

    public void comprobar(){
        foreach (int v in values){
            if (v == -1){
                Debug.Log("No has rellenado todos los numeros");
                return;
            }
        }
        for(int i = 0; i < 4; i++){
            Debug.Log(values[i]);
            Debug.Log(result[i]);
            if(values[i]!=result[i]) return;
        }

        Debug.Log("Has ganado");
        
        
    }
}
