using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Slider slider;
    void Start()
    {
        // Add a listener to call the LogSliderPercentage function whenever the slider value changes
        slider.onValueChanged.AddListener(delegate { LogSliderPercentage(); });
    }
    public void LogSliderPercentage()
    {
        float percentage = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue) * 100f;
    
        Debug.Log("Slider Percentage: " + percentage + "%");
    }
}
