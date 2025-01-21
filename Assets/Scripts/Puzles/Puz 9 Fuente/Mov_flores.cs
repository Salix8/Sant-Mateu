using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mov_flores : MonoBehaviour
{
    // Flores
    public GameObject[] flowers;
    public GameObject p_spawn;
    private int random_int;
    Rigidbody2D rigidbody2d;

    // Temporizador
    [SerializeField] private float max_timer;
    private float current_time;
    private bool timer_active = false;

    // Generación
    public float max_time_flowers; // Tiempo máximo entre flor y flor
    public float min_time_flowers; // Tiempo mínimo entre flor y flor
    private float random_float;

    // Panel de resultados
    [Header("Resultados")]
    [SerializeField] private GameObject panelResultados; // Panel que muestra los resultados
    [SerializeField] private Text textoResultados; // Texto dentro del panel

    // Puntuación (obtenida desde cesto_move)
    public cesto_move cesto;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Start_timer();
        StartCoroutine(Generate_flower());

        // Asegúrate de que el panel de resultados esté desactivado al inicio
        if (panelResultados != null)
        {
            panelResultados.SetActive(false);
        }
    }

    void Update()
    {
        if (timer_active)
        {
            Change_Timer();
        }
    }

    private IEnumerator Generate_flower()
    {
        while (timer_active) // Hacer que la corrutina continúe generando flores indefinidamente
        {
            random_float = Random.Range(min_time_flowers, max_time_flowers);
            yield return new WaitForSeconds(random_float);

            Vector2 position = GetComponent<Rigidbody2D>().position;
            random_int = Random.Range(-8, 9);
            position.x = random_int;

            random_int = Random.Range(0, flowers.Length);
            Instantiate(flowers[random_int], position, p_spawn.transform.rotation);
        }
    }

    private void Change_Timer()
    {
        current_time += Time.deltaTime;

        if (current_time >= max_timer)
        {
            Debug.Log("Fin del juego");
            Finish_timer();
        }
    }

    public void Start_timer()
    {
        current_time = 0;
        timer_active = true;
    }

    public void Finish_timer()
    {
        timer_active = false;
        ShowResults();
    }

    private void ShowResults()
    {
        if (panelResultados != null && textoResultados != null)
        {
            panelResultados.SetActive(true); // Activa el panel
            cesto_move cestoScript = cesto.GetComponent<cesto_move>();
            textoResultados.text = $"{cestoScript.points}";
        }
        else
        {
            Debug.LogError("Panel o texto de resultados no asignados en el Inspector.");
        }
    }
}
