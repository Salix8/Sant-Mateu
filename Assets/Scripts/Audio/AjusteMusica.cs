using UnityEngine;
using UnityEngine.UI;

public class AjusteMusica : MonoBehaviour
{
    public Slider volumeSlider;
    private float currentVolume = 1f;  // Valor del volumen (1 = 100%)

    private void Start()
    {
        
        currentVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = currentVolume;

        
        volumeSlider.onValueChanged.AddListener(SetVolume);

        
        AudioManager.Instance.AdjustMusicVolume(currentVolume);
    }

    public void SetVolume(float value)
    {
        
        currentVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", currentVolume);  

        
        AudioManager.Instance.AdjustMusicVolume(currentVolume);
    }

    public float GetVolume()
    {
        return currentVolume;
    }
}
