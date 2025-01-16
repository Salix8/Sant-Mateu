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

        // Inicializar la rotación aleatoria pero evitar que esté en la posición correcta
        int randIndex;
        do
        {
            randIndex = Random.Range(0, rotations.Length); // Elegir un índice aleatorio
            transform.eulerAngles = new Vector3(0, 0, rotations[randIndex]);
        }
        while (IsCorrectRotation()); // Asegurarse de que no empiece en una rotación correcta

        // Hacer la pieza más oscura al inicio
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>(); // Intenta obtener el SpriteRenderer automáticamente
        }
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f); // Color oscuro
    }

    private void OnMouseDown()
    {
        // Si la pieza ya está en su lugar, no permite moverla
        if (isPlaced) return;

        // Rotar la pieza
        RotatePiece();

        // Verificar si la nueva rotación es correcta
        CheckPlacement();
    }

    private void RotatePiece()
    {
        // Rotar exactamente 90 grados y ajustar la rotación para evitar problemas flotantes
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
            spriteRenderer.color = Color.white; // Cambia el color a blanco
        }
        else if (!IsCorrectRotation() && isPlaced)
        {
            isPlaced = false;
            gameManager.WrongMove();
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f); // Vuelve al color oscuro
        }
    }

    // Método para verificar si la rotación actual es correcta
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

    // Método para normalizar la rotación (conviértela a valores de 0° a 360°)
    private float NormalizeRotation(float rotation)
    {
        rotation = rotation % 360; // Mantiene el valor en [-360, 360]
        if (rotation < 0) rotation += 360; // Convierte valores negativos a positivos
        return Mathf.Round(rotation); // Redondea la rotación a números enteros
    }
}
