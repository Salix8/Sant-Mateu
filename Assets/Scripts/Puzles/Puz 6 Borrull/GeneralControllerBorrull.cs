using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralControllerBorrull : MonoBehaviour
{
    public List<Transform> lineContainers;
    public ProgressionManagerProxy progressionmanagerproxy;

    public Image image0;
    public Image image1;
    public Image image2;

    private List<List<int>> lines = new List<List<int>>();
    private int lastNumber = -1;
    private int selectedLineIndex = -1;

    void Start()
    {
        lines.Add(new List<int> { 1, 2, 1, 0 });
        lines.Add(new List<int> { 2, 0, 1, 2 });
        lines.Add(new List<int> { 0, 1, 2, 0 });
        lines.Add(new List<int>());

        UpdateVisuals();

        foreach (var line in lineContainers)
        {
            line.localScale = new Vector3(0.14f, 0.19f, 1f);
        }
        Debug.Log("Juego iniciado. Usa los botones para seleccionar una línea.");
    }

    public void SelectLine(int lineIndex)
    {
        if (selectedLineIndex == -1)
        {
            if (lines[lineIndex].Count > 0)
            {
                selectedLineIndex = lineIndex;
                lastNumber = lines[lineIndex][lines[lineIndex].Count - 1];
                HighlightLine(lineIndex, true);
                Debug.Log($"Línea {lineIndex + 1} seleccionada. Último número: {lastNumber}");
            }
            else
            {
                Debug.Log($"La línea {lineIndex + 1} está vacía. Selecciona otra.");
            }
        }
        else
        {
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
            targetLine.Add(lastNumber);
            selectedLine.RemoveAt(selectedLine.Count - 1);

            Debug.Log($"Número {lastNumber} movido de línea {selectedLineIndex + 1} a línea {targetLineIndex + 1}.");

            MoveImage(selectedLineIndex, targetLineIndex);
        }
        else
        {
            Debug.Log($"Movimiento inválido. No puedes mover a la línea {targetLineIndex + 1}.");
        }

        ResetSelection();
    }

    private void ResetSelection()
    {
        if (selectedLineIndex != -1)
        {
            HighlightLine(selectedLineIndex, false);
        }

        selectedLineIndex = -1;
        lastNumber = -1;
    }

    private void HighlightLine(int lineIndex, bool highlight)
    {
        var container = lineContainers[lineIndex];
        var image = container.GetComponent<Image>();

        if (image != null)
        {
            image.color = highlight ? new Color(255, 255, 255, 0.3f) : new Color(0, 0, 0, 0);
        }
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
        if (line.Count == 0) return true;
        int first = line[0];
        foreach (int num in line)
        {
            if (num != first) return false;
        }
        return true;
    }

    public void CheckVictoryButton()
    {
        if (CheckVictory())
        {
            progressionmanagerproxy.SetComplete(5);
            Debug.Log("¡Has ganado! Todas las líneas son homogéneas o están vacías.");
        }
        else
        {
            Debug.Log("Aún no has ganado. Sigue intentando.");
        }
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < lineContainers.Count; i++)
        {
            foreach (Transform child in lineContainers[i])
            {
                Destroy(child.gameObject);
            }

            foreach (int number in lines[i])
            {
                AddImageToLine(lineContainers[i], GetImageForNumber(number));
            }
        }
    }

    private Image GetImageForNumber(int number)
    {
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

        GameObject newImageObject = new GameObject("Image", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        newImageObject.transform.SetParent(line);
        Image newImage = newImageObject.GetComponent<Image>();
        newImage.sprite = image.sprite;
        newImage.raycastTarget = false;

        float spriteWidth = image.sprite.texture.width;
        float spriteHeight = image.sprite.texture.height;

        RectTransform rectTransform = newImageObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(spriteWidth, spriteHeight);
        rectTransform.localScale = Vector3.one;
        rectTransform.anchorMin = new Vector2(0.5f, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0);
        rectTransform.pivot = new Vector2(0.5f, 0);
        rectTransform.anchoredPosition = new Vector2(0, rectTransform.rect.height * line.childCount);
    }

    private void MoveImage(int sourceLineIndex, int targetLineIndex)
    {
        Transform sourceContainer = lineContainers[sourceLineIndex];
        Transform targetContainer = lineContainers[targetLineIndex];

        Transform imageToMove = sourceContainer.GetChild(sourceContainer.childCount - 1);
        imageToMove.SetParent(targetContainer);

        imageToMove.localScale = Vector3.one;
        imageToMove.SetAsLastSibling();
    }

    public void ResetGame()
    {
        lines.Clear();
        lines.Add(new List<int> { 1, 2, 1, 0 });
        lines.Add(new List<int> { 2, 0, 1, 2 });
        lines.Add(new List<int> { 0, 1, 2, 0 });
        lines.Add(new List<int>());

        selectedLineIndex = -1;
        lastNumber = -1;

        UpdateVisuals();

        Debug.Log("La partida ha sido reiniciada.");
    }
}
