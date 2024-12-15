using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource soundEffectSource;

    void Awake()
    {
        // Configuración para mantener una única instancia del AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el AudioManager entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            soundEffectSource.PlayOneShot(clip); // Reproduce el sonido
        }
    }
}
