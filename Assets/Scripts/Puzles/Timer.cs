using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tiempoTexto;
    [SerializeField] private float tiempoRestante = 60f;

    public event Action OnTimerFinished;

    private float tiempoAcumulado = 0f;
    private void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            tiempoAcumulado += Time.deltaTime;

            // Actualizar el texto solo una vez por segundo
            if (tiempoAcumulado >= 1f)
            {
                ActualizarTiempoTexto();
                tiempoAcumulado = 0f;
            }
        }
        else
        {
            tiempoRestante = 0;
            ActualizarTiempoTexto(); // Asegura que se muestre "00:00"
            Debug.Log("¡El tiempo se ha agotado!");
            OnTimerFinished?.Invoke(); // Invocar el evento
        }
    }

    private void ActualizarTiempoTexto()
    {
        if (tiempoTexto != null)
        {
            int minutos = Mathf.FloorToInt(tiempoRestante / 60);
            int segundos = Mathf.FloorToInt(tiempoRestante % 60);
            tiempoTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        }
        else
        {
            Debug.LogError("¡El campo Tiempo Texto no está asignado!");
        }
    }
}
