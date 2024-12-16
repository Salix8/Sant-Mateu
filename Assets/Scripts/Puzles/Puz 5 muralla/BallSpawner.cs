using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab; // El prefab de la bola que quieres generar
    [SerializeField] private Muralla muralla;


    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 2.0f; // Intervalo de tiempo entre cada generación de bola, cuanto menor mas salen
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(5, 0); // Coordenadas mínimas del área de generación
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(8, -4); // Coordenadas máximas del área de generación

    [Header("Retroceso")]
    [SerializeField] private GameObject[] retroceso;
    [SerializeField] private float retrocesoDistancia = -1f; // Distancia del retroceso
    [SerializeField] private float retrocesoDuracion = 0.15f; // Duración del retroceso
    [SerializeField] private float retrocesoRotacion = -8f; // Rotación máxima en grados sobre el eje Z

    private void Awake()
    {
        if (muralla == null)
            Debug.LogWarning($"Te falta añadir el prefab de la bala en {gameObject.name}. Puz 5 muralla");
        
        InvokeRepeating("SpawnBall", 0f, spawnInterval);
    }
    private void Update()
    {
        if (muralla.GetIsWin() || muralla.GetIsLose())
        {
            Destroy(gameObject);
        }
    }

    private void SpawnBall()
    {
        // Genera una posición aleatoria dentro del área de generación
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instancia la bola en la posición generada
        GameObject nuevaBala = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Retroceso();
        BallMovement scriptBala = nuevaBala.GetComponent<BallMovement>();
        if (muralla == null)
            Debug.LogWarning($"Te falta añadir el gameObject Muralla en {gameObject.name}. Puz 5 muralla");
        else
            scriptBala.SetMuralla(muralla);
    }

    private void Retroceso()
    {
        foreach (GameObject obj in retroceso)
        {
            StartCoroutine(AplicarRetroceso(obj.transform));
        }
    }
    private IEnumerator AplicarRetroceso(Transform transformCañon)
    {
        Vector3 posicionInicial = transformCañon.localPosition; // Guardar la posición inicial
        Vector3 posicionRetroceso = posicionInicial + new Vector3(-retrocesoDistancia, 0, 0); // Movimiento hacia atrás
        Quaternion rotacionInicial = transformCañon.localRotation;
        Quaternion rotacionRetroceso = rotacionInicial * Quaternion.Euler(0, 0, retrocesoRotacion); // Rotación hacia atrás


        // Movimiento hacia atrás con rotación
        float tiempo = 0;
        while (tiempo < retrocesoDuracion)
        {
            float factor = tiempo / retrocesoDuracion;
            transformCañon.localPosition = Vector3.Lerp(posicionInicial, posicionRetroceso, factor);
            transformCañon.localRotation = Quaternion.Lerp(rotacionInicial, rotacionRetroceso, factor);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transformCañon.localPosition = posicionRetroceso;
        transformCañon.localRotation = rotacionRetroceso;

        // Regreso a la posición y rotación iniciales
        tiempo = 0;
        while (tiempo < retrocesoDuracion)
        {
            float factor = tiempo / retrocesoDuracion;
            transformCañon.localPosition = Vector3.Lerp(posicionRetroceso, posicionInicial, factor);
            transformCañon.localRotation = Quaternion.Lerp(rotacionRetroceso, rotacionInicial, factor);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transformCañon.localPosition = posicionInicial;
        transformCañon.localRotation = rotacionInicial;

    }
}
