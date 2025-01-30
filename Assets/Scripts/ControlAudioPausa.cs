using UnityEngine;
using UnityEngine.UI;

public class ControlAudioPausa : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
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
            sliderValue = slider.value; // Guarda el valor antes de mutear
            slider.value = 0;
            AudioListener.volume = 0;
        }
        else
        {
            slider.value = sliderValue; // Restaura el volumen anterior
            AudioListener.volume = sliderValue;
        }
        PlayerPrefs.SetFloat("valorAudio", slider.value);
        RevisarSiEstoyMute();
    }
}
