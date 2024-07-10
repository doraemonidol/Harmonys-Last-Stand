using System.Collections;
using System.Collections.Generic;
using Presentation;
using UnityEngine.UI;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float minimum;

    public float maximum;

    public float current;

    public Image mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        if (maximumOffset == 0)
        {
            return;
        }
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    public void SetEffect(EffectUI effect)
    {
        minimum = 0;
        maximum = effect.Duration;
        current = effect.StartTime + effect.Duration - Time.time;
    }
}
