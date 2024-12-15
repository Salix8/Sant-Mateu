using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralControllerBorrull : MonoBehaviour
{
    // Referencias a los contenedores de las líneas (configurados en el Editor)
    public List<Transform> lineContainers;

    // Imágenes para los diferentes tipos de objetos (0, 1, 2)
    public Image image0;
    public Image image1;
    public Image image2;

    private List<List<int>> lines = new List<List<int>>();
    private int lastNumber = -1; // Último número seleccionado
    private int selectedLineIndex = -1; // Índice de la línea seleccionada

    void Start()
    {
        // Inicializamos las líneas
        lines.Add(new List<int> { 1, 2, 1, 0 }); // Línea 1
        lines.Add(new List<int> { 2, 0, 1, 2 }); // Línea 2
        lines.Add(new List<int> { 0, 1, 2, 0 }); // Línea 3
        lines.Add(new List<int>()); // Línea 4 (vacía)

        // Actualizamos la representación visual inicial
        UpdateVisuals();

        foreach(var line in lineContainers){
            line.localScale =  new Vector3(0.19f, 0.19f, 1f);
        }
        Debug.Log("Juego iniciado. Usa los botones para seleccionar una línea.");
    }

    public void SelectLine(int lineIndex)
    {
        if (selectedLineIndex == -1)
        {
            // Seleccionar una línea
            if (lines[lineIndex].Count > 0)
            {
                selectedLineIndex = lineIndex;
                lastNumber = lines[lineIndex][lines[lineIndex].Count - 1];
                Debug.Log($"Línea {lineIndex + 1} seleccionada. Último número: {lastNumber}");
            }
            else
            {
                Debug.Log($"La línea {lineIndex + 1} está vacía. Selecciona otra.");
            }
        }
        else
        {
            // Intentar mover el número a la nueva línea
            TryMoveToLine(lineIndex);
        }
    }

    private void TryMoveToLine(int targetLineIndex)
    {
        if (selectedLineIndex == targetLineIndex)
        {
            Debug.Log("No puedes mover a la misma línea seleccionada.");
            ResetSelection();
            return;
        }

        var targetLine = lines[targetLineIndex];
        var selectedLine = lines[selectedLineIndex];

        if (targetLine.Count == 0 || (targetLine[targetLine.Count - 1] == lastNumber && targetLine.Count < 4))
        {
            // Movimiento válido
            targetLine.Add(lastNumber);
            selectedLine.RemoveAt(selectedLine.Count - 1);

            Debug.Log($"Número {lastNumber} movido de línea {selectedLineIndex + 1} a línea {targetLineIndex + 1}.");

            // Actualizar la representación visual
            MoveImage(selectedLineIndex, targetLineIndex);
        }
        else
        {
            Debug.Log($"Movimiento inválido. No puedes mover a la línea {targetLineIndex + 1}.");
        }

        ResetSelection();

        // Comprobar la condición de victoria
        if (CheckVictory())
        {
            Debug.Log("¡Has ganado! Todas las líneas son homogéneas o están vacías.");
        }
    }

    private void ResetSelection()
    {
        selectedLineIndex = -1;
        lastNumber = -1;
    }

    private bool CheckVictory()
    {
        foreach (var line in lines)
        {
            if (!IsLineValid(line)) return false;
        }
        return true;
    }

    private bool IsLineValid(List<int> line)
    {
        if (line.Count == 0) return true; // Vacía es válida
        int first = line[0];
        foreach (int num in line)
        {
            if (num != first) return false; // Si hay un número distinto, no es válida
        }
        return true; // Todos los números son iguales
    }

    private void UpdateVisuals()
    {
        // Limpiar y reconstruir las imágenes en las líneas visuales
        for (int i = 0; i < lineContainers.Count; i++)
        {
            // Limpia los hijos existentes
            foreach (Transform child in lineContainers[i])
            {
                Destroy(child.gameObject);
            }

            // Añade las imágenes correspondientes
            foreach (int number in lines[i])
            {
                AddImageToLine(lineContainers[i], GetImageForNumber(number));
            }
        }
    }

    private Image GetImageForNumber(int number)
    {
        // Devuelve la imagen correspondiente según el número
        switch (number)
        {
            case 0: return image0;
            case 1: return image1;
            case 2: return image2;
            default: return null;
        }
    }

private void AddImageToLine(Transform line, Image image)
{
    if (image == null) return;

    Debug.Log("Entra");

    // Crear un nuevo GameObject para la imagen
    GameObject newImageObject = new GameObject("Image", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
    newImageObject.transform.SetParent(line); // Lo añadimos como hijo del contenedor de la línea
    Image newImage = newImageObject.GetComponent<Image>();
    newImage.sprite = image.sprite; // Asignar la imagen

    // Desactivar el RaycastTarget para evitar que la imagen bloquee la interacción con otros elementos
    newImage.raycastTarget = false;

    // Obtener las dimensiones originales del sprite
    float spriteWidth = image.sprite.texture.width;
    float spriteHeight = image.sprite.texture.height;

    // Ajustar el tamaño del RectTransform para que coincida con el tamaño del sprite
    RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
    rectTransform.sizeDelta = new Vector2(spriteWidth, spriteHeight); // Ajustar tamaño de la imagen al tamaño del sprite

    // Asegurarse de que la escala local sea (1, 1, 1) para evitar que se distorsione
    rectTransform.localScale = Vector3.one;

    // Asegurarse de que el rectTransform se ajuste al contenedor
    rectTransform.anchorMin = new Vector2(0.5f, 0); // Centrado en el eje X
    rectTransform.anchorMax = new Vector2(0.5f, 0); // Centrado en el eje X
    rectTransform.pivot = new Vector2(0.5f, 0); // El pivote está en la parte inferior

    // Posicionamos la imagen sobre los objetos existentes en la línea
    rectTransform.anchoredPosition = new Vector2(0, rectTransform.rect.height * line.childCount);
}




    private void MoveImage(int sourceLineIndex, int targetLineIndex)
    {
        // Referencias a los contenedores de las líneas
        Transform sourceContainer = lineContainers[sourceLineIndex];
        Transform targetContainer = lineContainers[targetLineIndex];

        // Eliminar el último GameObject de la línea origen
        Transform imageToMove = sourceContainer.GetChild(sourceContainer.childCount - 1);
        imageToMove.SetParent(targetContainer);

        // Ajustar la posición y escala del objeto movido
        imageToMove.localScale = Vector3.one;
        imageToMove.SetAsLastSibling();
    }

    public void ResetGame()
{
    // Restablecer las líneas a su estado inicial
    lines.Clear();
    lines.Add(new List<int> { 1, 2, 1, 0 }); // Línea 1
    lines.Add(new List<int> { 2, 0, 1, 2 }); // Línea 2
    lines.Add(new List<int> { 0, 1, 2, 0 }); // Línea 3
    lines.Add(new List<int>()); // Línea 4 (vacía)

    // Restablecer las variables de selección
    selectedLineIndex = -1;
    lastNumber = -1;

    // Actualizar la visualización para reflejar el estado inicial
    UpdateVisuals();

    // Imprimir mensaje de reinicio
    Debug.Log("La partida ha sido reiniciada.");
}

}
