using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void Start() 
    {
        // Get the value from the audio to refresh the slider value
        slider.value = AudioManager.instance.GetOverallVolume();    
    }

    private void OnSliderValueChanged(float value)
    {
        AudioManager.instance.UpdateOverallVolume(value);
    }
}