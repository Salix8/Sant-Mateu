using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab; // El prefab de la bola que quieres generar
    [SerializeField] private Muralla muralla;

    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 2.0f; // Intervalo de tiempo entre cada generación de bola
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(5,0); // Coordenadas mínimas del área de generación
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(8,-4); // Coordenadas máximas del área de generación
    [SerializeField] private float minAngle = 130f; // Ángulo mínimo de rotación
    [SerializeField] private float maxAngle = 190f; // Ángulo máximo de rotación



    private void Start()
    {
        if (muralla == null)
            Debug.LogWarning($"Te falta añadir el prefab de la bala en {gameObject.name}. Puz 5 muralla");

        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    private void SpawnBall()
    {
        // Genera una posición aleatoria dentro del área de generación
        float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        float randomAngle = Random.Range(minAngle, maxAngle);
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, randomAngle);

        // Instancia la bola en la posición generada
        GameObject nuevaBala = Instantiate(ballPrefab, spawnPosition, spawnRotation);
        BallMovement scriptBala = nuevaBala.GetComponent<BallMovement>();
        if (muralla == null)
            Debug.LogWarning($"Te falta añadir el gameObject Muralla en {gameObject.name}. Puz 5 muralla");
        else
            scriptBala.SetMuralla(muralla);
    }
}
