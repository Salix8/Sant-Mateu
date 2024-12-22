using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Muralla : MonoBehaviour
{
    [SerializeField] private int vida = 3;
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite loseBackground;

    [SerializeField] private Timer timer;
    private bool isWin = false;
    private bool isLose = false;

    [SerializeField] private TextAsset dialogo;
    [SerializeField] private TextAsset dialogo2;
    [SerializeField] private GameObject[] ocultar;

    private void Awake()
    {
        if (timer != null)
        {
            timer.OnTimerFinished += HandleTimerFinished; // Suscribirse al evento
            //Debug.Log("Se ha suscrito al evento OnTimerFinished.");
        }
        vida = 3;
    }
    private void OnDestroy()
    {
        if (timer != null)
            timer.OnTimerFinished -= HandleTimerFinished; // Desuscribirse del evento al destruirse
    }

    private void HandleTimerFinished()
    {
        hasGanado();
    }

    public bool GetIsWin()
    {
        return isWin;
    }
    public bool GetIsLose()
    {
        return isLose;
    }
    public int GetVidaMuralla()
    {
        return vida;
    }

    public void SetVidaMuralla(int valor)
    {
        //Debug.Log($"Vida = {vida}, y {vida} > 0 = {vida > 0}");
        if (vida > 0)
            vida += valor;
        else
            hasPerdido();
        Debug.Log($"Tienes {vida} vidas de 3");
    }

    private void hasGanado()
    {
        if (!isWin && !isLose)
        {
            isWin = true;
            ocultarElementos();
            destroyProjectiles();
            DialogueManager.GetInstance().EnterDialogueMode(dialogo);
            ProgresionManager.GetInstance().SetComplete(5);
            //GlobalManager.GetInstance().LoadMainScene();
        }
    }

    private void hasPerdido()
    {
        if (!isLose && !isWin)
        {
            isLose = true;
            ocultarElementos();
            destroyProjectiles();
            Image imagen = background.GetComponent<Image>();
            imagen.sprite = loseBackground;
            DialogueManager.GetInstance().EnterDialogueMode(dialogo2);
            //if (GlobalManager.GetInstance() != null)
            //    GlobalManager.GetInstance().LoadMainScene();
            //else
            //    Debug.LogWarning($"No se ha creado ninguna instancia del GlobalManger. Puz 5 muralla");

            //if (ProgresionManager.GetInstance() != null)
            //    ProgresionManager.GetInstance().SetComplete(5);
            //else
            //    Debug.LogWarning($"No se ha creado ninguna instancia del ProgresionManger. Puz 5 muralla");
        }
    }

    private void ocultarElementos()
    {
        foreach (GameObject obj in ocultar)
        {
            obj.SetActive(false);
        }
    }

    private void destroyProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject obj in projectiles)
        {
            Destroy(obj);
        }
    }
}
