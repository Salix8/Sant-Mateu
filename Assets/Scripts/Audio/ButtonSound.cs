using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound; // El sonido que reproducirá este botón
    [SerializeField] private bool playSound = true;      // Habilitar o deshabilitar sonido para este botón

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        if (button != null && playSound)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    void PlayButtonSound()
    {
        if (playSound)
        {
            AudioManager.Instance.PlaySound(buttonClickSound);
        }
    }
}
