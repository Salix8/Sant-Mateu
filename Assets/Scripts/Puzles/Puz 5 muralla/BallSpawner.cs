using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab; // El prefab de la bola que quieres generar
    [SerializeField] private Muralla muralla;


    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 2.0f; // Intervalo de tiempo entre cada generaci�n de bola, cuanto menor mas salen
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(5, 0); // Coordenadas m�nimas del �rea de generaci�n
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(8, -4); // Coordenadas m�ximas del �rea de generaci�n

    [Header("Retroceso")]
    [SerializeField] private GameObject[] retroceso;
    [SerializeField] private float retrocesoDistancia = -1f; // Distancia del retroceso
    [SerializeField] private float retrocesoDuracion = 0.15f; // Duraci�n del retroceso
    [SerializeField] private float retrocesoRotacion = -8f; // Rotaci�n m�xima en grados sobre el eje Z

    private void Awake()
    {
        if (muralla == null)
            Debug.LogWarning($"Te falta a�adir el prefab de la bala en {gameObject.name}. Puz 5 muralla");
        
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
        // Genera una posici�n aleatoria dentro del �rea de generaci�n
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instancia la bola en la posici�n generada
        GameObject nuevaBala = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        Retroceso();
        BallMovement scriptBala = nuevaBala.GetComponent<BallMovement>();
        if (muralla == null)
            Debug.LogWarning($"Te falta a�adir el gameObject Muralla en {gameObject.name}. Puz 5 muralla");
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
    private IEnumerator AplicarRetroceso(Transform transformCa�on)
    {
        Vector3 posicionInicial = transformCa�on.localPosition; // Guardar la posici�n inicial
        Vector3 posicionRetroceso = posicionInicial + new Vector3(-retrocesoDistancia, 0, 0); // Movimiento hacia atr�s
        Quaternion rotacionInicial = transformCa�on.localRotation;
        Quaternion rotacionRetroceso = rotacionInicial * Quaternion.Euler(0, 0, retrocesoRotacion); // Rotaci�n hacia atr�s


        // Movimiento hacia atr�s con rotaci�n
        float tiempo = 0;
        while (tiempo < retrocesoDuracion)
        {
            float factor = tiempo / retrocesoDuracion;
            transformCa�on.localPosition = Vector3.Lerp(posicionInicial, posicionRetroceso, factor);
            transformCa�on.localRotation = Quaternion.Lerp(rotacionInicial, rotacionRetroceso, factor);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transformCa�on.localPosition = posicionRetroceso;
        transformCa�on.localRotation = rotacionRetroceso;

        // Regreso a la posici�n y rotaci�n iniciales
        tiempo = 0;
        while (tiempo < retrocesoDuracion)
        {
            float factor = tiempo / retrocesoDuracion;
            transformCa�on.localPosition = Vector3.Lerp(posicionRetroceso, posicionInicial, factor);
            transformCa�on.localRotation = Quaternion.Lerp(rotacionRetroceso, rotacionInicial, factor);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transformCa�on.localPosition = posicionInicial;
        transformCa�on.localRotation = rotacionInicial;

    }
}
