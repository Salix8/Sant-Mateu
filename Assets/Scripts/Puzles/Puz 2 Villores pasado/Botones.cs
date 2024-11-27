using UnityEngine;

public class MusicosBotones : MonoBehaviour
{
    public int buttonID; // ID del botón
    private GameManager gameManager;

    private void Start()
    {
        // Busca el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
    }

    // Método llamado por el botón
    public void OnClick()
    {
        gameManager.OnButtonPressed(buttonID);
    }
}
