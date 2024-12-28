using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource soundEffectSource;  // AudioSource para efectos de sonido
    [SerializeField] private AudioSource musicSource;        // AudioSource para música
    [SerializeField] private AjusteSonido ajusteSonido;      // Referencia al script de ajuste de sonido
    [SerializeField] private AjusteMusica ajusteMusica;      // Referencia al script de ajuste de música

    void Awake()
    {
        // Configuración para mantener una única instancia del AudioManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Mantiene el AudioManager entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        // Asegurarse de que el volumen de la música se ajusta correctamente desde PlayerPrefs
        if (musicSource != null)
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);  // Valor predeterminado 1 si no está guardado
            musicSource.volume = musicVolume;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
           
            soundEffectSource.volume = ajusteSonido.GetVolume();
            soundEffectSource.PlayOneShot(clip);  
        }
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip != null && musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;  
            musicSource.volume = ajusteMusica.GetVolume();  
            musicSource.Play();  
        }
    }

    public void AdjustMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }
}
