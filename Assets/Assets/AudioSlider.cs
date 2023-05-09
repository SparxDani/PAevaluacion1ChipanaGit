using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public string channelName;
    public Text valueText;
    private Slider slider;
    private AudioManager audioManager;

    private void Start()
    {
        slider = GetComponent<Slider>();
        audioManager = FindObjectOfType<AudioManager>();
        valueText.text = $"{channelName}: {slider.value.ToString("0.00")}";
    }

    public void OnSliderValueChanged(float value)
    {
        valueText.text = $"{channelName}: {value.ToString("0.00")}";
        switch (channelName)
        {
            case "Master":
                audioManager.SetMasterVolume(value);
                break;
            case "Music":
                audioManager.SetMusicVolume(value);
                break;
            case "Effects":
                audioManager.SetEffectsVolume(value);
                break;
        }
    }
}
