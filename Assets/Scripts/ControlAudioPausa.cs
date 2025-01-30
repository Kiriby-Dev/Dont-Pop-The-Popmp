using UnityEngine;
using UnityEngine.UI;

public class ControlAudioPausa : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    private float lastVolume = 1f; // Guardar el último volumen antes de mutear
    public Button muteButton;
    public Sprite unmutedSprite, mutedSprite;
    private bool isMute = false;

    void Start()
    {
        sliderValue = PlayerPrefs.GetFloat("valorAudio", 1f);
        slider.value = sliderValue;
        AudioListener.volume = sliderValue;
        isMute = (sliderValue == 0);
        RevisarSiEstoyMute();
        muteButton.onClick.AddListener(ToggleMute);
        slider.onValueChanged.AddListener(ChangeSlider);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("valorAudio", sliderValue);
        AudioListener.volume = sliderValue;
        isMute = (sliderValue == 0);
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        muteButton.GetComponent<Image>().sprite = isMute ? mutedSprite : unmutedSprite;
    }

    public void ToggleMute()
    {
        isMute = !isMute;
        if (isMute)
        {
            lastVolume = slider.value > 0 ? slider.value : lastVolume; // Guarda el volumen solo si es mayor a 0
            slider.value = 0;
            AudioListener.volume = 0;
        }
        else
        {
            slider.value = lastVolume; // Restaura el volumen anterior
            AudioListener.volume = lastVolume;
        }
        PlayerPrefs.SetFloat("valorAudio", slider.value);
        RevisarSiEstoyMute();
    }
}