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

    private int[] result = new int[4]; // Vector con los valores correctos
    private Color defaultColor; // Color predeterminado de los botones

    public ProgressionManagerProxy progressionmanagerproxy; 

    void Start()
    {
        result[0] = 2;
        result[1] = 3;
        result[2] = 1;
        result[3] = 4;

        // Verificar referencias nulas
        if (mainButtons == null || mainButtons.Length == 0) return;
        if (secondaryButtons == null || secondaryButtons.Length == 0) return;
        if (secondaryButtonsPanel == null) return;

        // Almacenamos el color predeterminado de los botones principales
        if (mainButtons[0] != null)
        {
            defaultColor = mainButtons[0].GetComponent<Image>().color;
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
        // Siempre activa el contenedor de botones secundarios en un clic
        secondaryButtonsPanel.SetActive(true);

        if (values[index] != -1) // Si ya hay un número asignado
        {
            int previousValue = values[index];
            values[index] = -1; // Eliminamos el número asignado
            usedNumbers[previousValue - 1] = false; // Marcamos el número como disponible
            var tmpText = mainButtons[index].GetComponentInChildren<TextMeshProUGUI>();
            if (tmpText != null) tmpText.text = ""; // Limpiamos el texto del botón

            // Restauramos el color predeterminado
            mainButtons[index].GetComponent<Image>().color = defaultColor;
        }

        // Activamos/desactivamos botones según los números usados
        for (int i = 0; i < secondaryButtons.Length; i++)
        {
            secondaryButtons[i].interactable = !usedNumbers[i];
        }

        activeMainButtonIndex = index; // Guardamos el índice del botón principal activo
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
                if (tmpText != null) tmpText.text = number.ToString(); // Mostramos el número
            }

            if (secondaryButtonsPanel != null)
            {
                secondaryButtonsPanel.SetActive(false); // Ocultamos los botones secundarios
            }

            activeMainButtonIndex = -1; // Reseteamos el índice activo
        }
    }

    public void comprobar()
    {
        StartCoroutine(CheckNumbers());
    }

    private IEnumerator CheckNumbers()
    {
        bool allCorrect = true;

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == -1)
            {
                Debug.Log("No has rellenado todos los números");
                yield break;
            }

            var buttonImage = mainButtons[i].GetComponent<Image>();
            if (values[i] == result[i])
            {
                buttonImage.color = Color.green; // Correcto
            }
            else
            {
                buttonImage.color = Color.red; // Incorrecto
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            Debug.Log("Has ganado");
            progressionmanagerproxy.SetComplete(1);
        }
        else
        {
            Debug.Log("Hay errores en el orden");
            yield return new WaitForSeconds(1f); // Esperar 1 segundo

            // Restaurar colores y mover números incorrectos al contenedor de botones secundarios
            for (int i = 0; i < values.Length; i++)
            {
                var buttonImage = mainButtons[i].GetComponent<Image>();
                if (values[i] != result[i])
                {
                    buttonImage.color = defaultColor; // Restaurar color predeterminado
                    var tmpText = mainButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (tmpText != null) tmpText.text = ""; // Limpiar texto del botón principal

                    // Hacer el número disponible en los botones secundarios
                    usedNumbers[values[i] - 1] = false;
                    secondaryButtons[values[i] - 1].interactable = true;
                }
            }
        }
    }
}
