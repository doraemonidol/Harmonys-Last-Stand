using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
    public class EffectUIManager : MonoBehaviour
    {
        private List<EffectUI> effects = new List<EffectUI>();
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI effectText;
        [SerializeField] private Canvas canvas;
        
        public void AddEffect(EffectUI effect)
        {
            EffectUI toRemove = null;
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].Name == effect.Name)
                {
                    toRemove = effects[i];
                    break;
                }
            }
            Debug.Log("Adding effect");
            if (toRemove != null)
            {
                effects.Remove(toRemove);
                if (toRemove.StartTime + toRemove.Duration > effect.StartTime + effect.Duration)
                {
                    effects.Add(toRemove);
                }
                else
                {
                    effects.Add(effect);
                }
            } else
            {
                effects.Add(effect);
            }
        }

        public void Start()
        {
            StartCoroutine(Test());
        }
        
        IEnumerator Test()
        {
            yield return new WaitForSeconds(4);
            AddEffect(new EffectUI()
            {
                Name = "Test",
                StartTime = Time.time,
                Duration = 10
            });
            yield return new WaitForSeconds(4);
            AddEffect(new EffectUI()
            {
                Name = "Test 2",
                StartTime = Time.time,
                Duration = 4
            });
        }

        public void Update()
        {
            int i = 0;
            while (i < effects.Count)
            {
                if (Time.time > effects[i].StartTime + effects[i].Duration)
                {
                    effects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            Debug.Log(effects.Count);
            if (effects.Count == 0)
            {
                canvas.gameObject.SetActive(false);
                return;
            }
            canvas.gameObject.SetActive(true);
            progressBar.SetEffect(effects[effects.Count - 1]);
            effectText.text = effects[effects.Count - 1].Name;
        }
    }
}