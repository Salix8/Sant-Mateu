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

    [SerializeField] private TextAsset dialogo;

    private void Awake()
    {
        if (timer != null)
        {
            timer.OnTimerFinished += HandleTimerFinished; // Suscribirse al evento
            //Debug.Log("Se ha suscrito al evento OnTimerFinished.");
        }
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

    private void hasGanado()
    {
        DialogueManager.GetInstance().EnterDialogueMode(dialogo);
        while (DialogueManager.GetInstance().dialogueIsPlaying)
        {

        }
        GlobalManager.GetInstance().LoadMainScene();
    }

    private void hasPerdido()
    {
        Image imagen = background.GetComponent<Image>();
        imagen.sprite = loseBackground;

        if (GlobalManager.GetInstance() != null)
            GlobalManager.GetInstance().LoadMainScene();
        else
            Debug.LogWarning($"No se ha creado ninguna instancia del GlobalManger. Puz 5 muralla");

        if (ProgresionManager.GetInstance() != null)
            ProgresionManager.GetInstance().SetComplete(5);
        else
            Debug.LogWarning($"No se ha creado ninguna instancia del ProgresionManger. Puz 5 muralla");
    }

    public int GetVidaMuralla()
    {
        return vida;
    }

    public void SetVidaMuralla(int valor)
    {
        if(vida > 0)
            vida += valor;
        else
            hasPerdido();
        Debug.Log($"Tienes {vida} vidas de 3");
    }
}
