using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipes : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer de la pieza

    float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation; // Posiciones correctas permitidas para esta pieza

    [SerializeField]
    bool isPlaced = false;

    int PossibleRots = 1;

    GameManagerPipes gameManager;

    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerPipes>();
    }

    private void Start()
    {
        PossibleRots = correctRotation.Length;

        // Inicializar la rotaci�n aleatoria pero evitar que est� en la posici�n correcta
        int randIndex;
        do
        {
            randIndex = Random.Range(0, rotations.Length); // Elegir un �ndice aleatorio
            transform.eulerAngles = new Vector3(0, 0, rotations[randIndex]);
        }
        while (IsCorrectRotation()); // Asegurarse de que no empiece en una rotaci�n correcta

        // Hacer la pieza m�s oscura al inicio
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Intenta obtener el SpriteRenderer autom�ticamente
        }
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f); // Color oscuro
    }

    private void OnMouseDown()
    {
        // Si la pieza ya est� en su lugar, no permite moverla
        if (isPlaced) return;

        // Rotar la pieza
        RotatePiece();

        // Verificar si la nueva rotaci�n es correcta
        CheckPlacement();
    }

    private void RotatePiece()
    {
        // Rotar exactamente 90 grados y ajustar la rotaci�n para evitar problemas flotantes
        float currentZ = NormalizeRotation(transform.eulerAngles.z);
        float nextRotation = currentZ + 90;
        if (nextRotation >= 360) nextRotation -= 360; // Asegurar que no exceda 360
        transform.eulerAngles = new Vector3(0, 0, nextRotation);
    }

    private void CheckPlacement()
    {
        // Verificar si la pieza está en la posición correcta
        if (IsCorrectRotation() && !isPlaced)
        {
            isPlaced = true;
            gameManager.CorrectMove();
            StartCoroutine(ChangeColor(spriteRenderer.color, Color.white, 0.5f)); // Transición a blanco
        }
        else if (!IsCorrectRotation() && isPlaced)
        {
            isPlaced = false;
            gameManager.WrongMove();
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f); // Cambio directo al color oscuro
        }
    }

    private IEnumerator ChangeColor(Color startColor, Color endColor, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(startColor, endColor, elapsed / duration);
            yield return null;
        }

        spriteRenderer.color = endColor; // Asegúrate de establecer el color final
    }


    // M�todo para verificar si la rotaci�n actual es correcta
    private bool IsCorrectRotation()
    {
        float currentRotation = NormalizeRotation(transform.eulerAngles.z);

        foreach (float correctRot in correctRotation)
        {
            if (Mathf.Approximately(currentRotation, NormalizeRotation(correctRot)))
            {
                return true;
            }
        }
        return false;
    }

    // M�todo para normalizar la rotaci�n (convi�rtela a valores de 0� a 360�)
    private float NormalizeRotation(float rotation)
    {
        rotation = rotation % 360; // Mantiene el valor en [-360, 360]
        if (rotation < 0) rotation += 360; // Convierte valores negativos a positivos
        return Mathf.Round(rotation); // Redondea la rotaci�n a n�meros enteros
    }
}
