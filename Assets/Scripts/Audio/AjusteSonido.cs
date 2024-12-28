using UnityEngine;
using UnityEngine.UI;

public class AjusteSonido : MonoBehaviour
{
    public Slider volumeSlider;  
    private float currentVolume = 1f;  // Valor del volumen (1 = 100%)

    private void Start()
    {
        // Configura el volumen inicial desde PlayerPrefs o el valor predeterminado
        currentVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);
        volumeSlider.value = currentVolume;

        // Escuchar los cambios del slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Guardar y actualizar el volumen actual
        currentVolume = value;
        PlayerPrefs.SetFloat("SoundVolume", currentVolume);
    }

    public float GetVolume()
    {
        // Permitir que otros scripts obtengan el volumen actual
        return currentVolume;
    }
}
