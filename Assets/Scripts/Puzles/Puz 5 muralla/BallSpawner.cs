using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab; // El prefab de la bola que quieres generar

    [Header("Spawn")]
    [SerializeField] private bool usePointSpawn = false;
    [SerializeField] private float spawnInterval = 2.0f; // Intervalo de tiempo entre cada generaci�n de bola
    [SerializeField] private Vector2 spawnAreaMin; // Coordenadas m�nimas del �rea de generaci�n
    [SerializeField] private Vector2 spawnAreaMax; // Coordenadas m�ximas del �rea de generaci�n
    [SerializeField] private float minAngle = 120f; // �ngulo m�nimo de rotaci�n
    [SerializeField] private float maxAngle = 180f; // �ngulo m�ximo de rotaci�n


    private void Start()
    {
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }

    private void SpawnBall()
    {
        // Genera una posici�n aleatoria dentro del �rea de generaci�n
        float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        float randomAngle = Random.Range(minAngle, maxAngle);
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, randomAngle);

        // Instancia la bola en la posici�n generada
        Instantiate(ballPrefab, spawnPosition, spawnRotation);
    }
}
