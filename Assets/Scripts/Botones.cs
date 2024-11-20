using UnityEngine;

public class MusicosBotones : MonoBehaviour
{
    public int buttonID; // ID del bot�n
    private GameManager gameManager;

    private void Start()
    {
        // Busca el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    // M�todo llamado por el bot�n
    public void OnClick()
    {
        gameManager.OnButtonPressed(buttonID);
    }
}
