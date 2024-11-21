using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab; // El prefab de la bola que quieres generar

    [Header("Spawn")]
    [SerializeField] private bool usePointSpawn = false;
    [SerializeField] private float spawnInterval = 2.0f; // Intervalo de tiempo entre cada generación de bola
    [SerializeField] private Vector2 spawnAreaMin; // Coordenadas mínimas del área de generación
    [SerializeField] private Vector2 spawnAreaMax; // Coordenadas máximas del área de generación
    [SerializeField] private float minAngle = 120f; // Ángulo mínimo de rotación
    [SerializeField] private float maxAngle = 180f; // Ángulo máximo de rotación


    private void Start()
    {
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
        Instantiate(ballPrefab, spawnPosition, spawnRotation);
    }
}
