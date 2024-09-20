using System.Collections;
using System.Collections.Generic;
using Presentation.Sound;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSourceEnum type;
    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("Slider component not found");
        }
        
        if (type == AudioSourceEnum.MUSIC)
        {
            slider.value = SoundManager.Instance.GetMusicVolume();
        }
        else if (type == AudioSourceEnum.SFX)
        {
            slider.value = SoundManager.Instance.GetSFXVolume();
        }
    }
    public void UpdateSliderValue()
    {
        if (type == AudioSourceEnum.MUSIC)
        {
            SoundManager.Instance.SetMusicVolume(slider.value);
        }
        else if (type == AudioSourceEnum.SFX)
        {
            SoundManager.Instance.SetSFXVolume(slider.value);
        }
    }
}
