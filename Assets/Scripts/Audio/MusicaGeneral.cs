using UnityEngine;
using UnityEngine.UI;

public class MusicaGeneral : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;  
    [SerializeField] private Slider volumeSlider;  
    private AudioSource musicSource;

    private void Start()
    {
        
        musicSource = GetComponent<AudioSource>();

        
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);  
        musicSource.volume = musicVolume;

        
        volumeSlider.value = musicVolume;

        
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        
        if (musicClip != null)
        {
            PlayMusic(musicClip);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;  
        musicSource.Play();  
    }

    
    private void OnVolumeChanged(float value)
    {
        
        musicSource.volume = value;

        
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}
