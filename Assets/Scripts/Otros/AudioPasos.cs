using UnityEngine;

public class AudioPasos : MonoBehaviour
{
    private static AudioPasos instance;
    [SerializeField] private GameObject audioSourcePrefab; // Prefab con AudioSource
    [SerializeField] private AudioClip interactionSoundDefault; // Sonido al interactuar

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Se ha encontrado mas de un AudioPasos en la escena.");
            return;
        }
        instance = this;
    }

    public static AudioPasos GetInstance()
    {
        return instance;
    }

    public void PlayOneShot(AudioClip clip, float volume = 0.5f)
    {
        GameObject audioObj = Instantiate(audioSourcePrefab);
        AudioSource audioSource = audioObj.GetComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        // Destruir el objeto cuando termine de reproducirse
        Destroy(audioObj, clip.length);
    }
    public void PlayOneShot()
    {
        GameObject audioObj = Instantiate(audioSourcePrefab);
        AudioSource audioSource = audioObj.GetComponent<AudioSource>();
        AudioClip clip = interactionSoundDefault;

        audioSource.clip = clip;
        audioSource.volume = 0.5f;
        audioSource.Play();

        // Destruir el objeto cuando termine de reproducirse
        Destroy(audioObj, clip.length);
    }
}
