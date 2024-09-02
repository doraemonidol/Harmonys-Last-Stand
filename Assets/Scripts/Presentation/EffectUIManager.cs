using System;
using System.Collections;
using System.Collections.Generic;
using Common;
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
        
        [Header("Effect Prefabs")]
        [SerializeField] private GameObject stuntPrefab;
        [SerializeField] private GameObject bleedingPrefab;
        [SerializeField] private GameObject hallucinationPrefab;
        [SerializeField] private GameObject fearPrefab;
        [SerializeField] private GameObject nearsightPrefab;
        [SerializeField] private GameObject shieldPrefab;
        [SerializeField] private GameObject rootedPrefab;
        [SerializeField] private GameObject resistancePrefab;
        [SerializeField] private GameObject jinxPrefab;
        [SerializeField] private GameObject knockbackPrefab;
        [SerializeField] private GameObject reversePrefab;
        [SerializeField] private GameObject charmPrefab;
        [SerializeField] private GameObject getHitPrefab;
        [SerializeField] private GameObject sleepyPrefab;
        [SerializeField] private GameObject resonancePrefab;
        [SerializeField] private GameObject exhaustedPrefab;
        [SerializeField] private GameObject resurrectionPrefab;
        [SerializeField] private GameObject silentPrefab;
        [SerializeField] private GameObject healingPrefab;
        [SerializeField] private GameObject voidPrefab;
        
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
                    deactivatePrefab(toRemove.Name);
                    activatePrefab(toRemove.Name);
                    effects.Add(effect);
                }
            } else
            {
                activatePrefab(effect.Name);
                effects.Add(effect);
            }
        }

        public void Start()
        {
            stuntPrefab.gameObject.SetActive(false);
            bleedingPrefab.gameObject.SetActive(false);
            hallucinationPrefab.gameObject.SetActive(false);
            // fearPrefab.gameObject.SetActive(false);
            nearsightPrefab.gameObject.SetActive(false);
            shieldPrefab.gameObject.SetActive(false);
            rootedPrefab.gameObject.SetActive(false);
            resistancePrefab.gameObject.SetActive(false);
            jinxPrefab.gameObject.SetActive(false);
            knockbackPrefab.gameObject.SetActive(false);
            reversePrefab.gameObject.SetActive(false);
            charmPrefab.gameObject.SetActive(false);
            getHitPrefab.gameObject.SetActive(false);
            sleepyPrefab.gameObject.SetActive(false);
            resonancePrefab.gameObject.SetActive(false);
            exhaustedPrefab.gameObject.SetActive(false);
            resurrectionPrefab.gameObject.SetActive(false);
            silentPrefab.gameObject.SetActive(false);
            healingPrefab.gameObject.SetActive(false);
            voidPrefab.gameObject.SetActive(false);
            StartCoroutine(Test());
        }
        
        IEnumerator Test()
        {
            yield return new WaitForSeconds(4);
            AddEffect(new EffectUI()
            {
                Name = EffectType.STUNT,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.BLEEDING,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.HALLUCINATION,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.FEAR,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.NEARSIGHT,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.SHIELD,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.ROOTED,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.RESISTANCE,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.JINX,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.KNOCKBACK,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.REVERSE,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.CHARM,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.GET_HIT,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.SLEEPY,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.RESONANCE,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.EXHAUSTED,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.RESURRECTION,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.SILENT,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.HEALING,
                StartTime = Time.time,
                Duration = 2
            });
            yield return new WaitForSeconds(2);
            AddEffect(new EffectUI()
            {
                Name = EffectType.VOID,
                StartTime = Time.time,
                Duration = 2
            });
        }

        public void Update()
        {
            int i = 0;
            while (i < effects.Count)
            {
                if (Time.time > effects[i].StartTime + effects[i].Duration)
                {
                    deactivatePrefab(effects[i].Name);
                    effects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            
            if (effects.Count == 0)
            {
                canvas.gameObject.SetActive(false);
                return;
            }
            canvas.gameObject.SetActive(true);
            progressBar.SetEffect(effects[effects.Count - 1]);
            effectText.text = effects[effects.Count - 1].Name;
        }
        
        private GameObject getEffectPrefab(string name)
        {
            if (name == EffectType.STUNT)
            {
                return stuntPrefab;
            }
            else if (name == EffectType.BLEEDING)
            {
                return bleedingPrefab;
            }
            else if (name == EffectType.HALLUCINATION)
            {
                return hallucinationPrefab;
            }
            else if (name == EffectType.FEAR)
            {
                return fearPrefab;
            }
            else if (name == EffectType.NEARSIGHT)
            {
                return nearsightPrefab;
            }
            else if (name == EffectType.SHIELD)
            {
                return shieldPrefab;
            }
            else if (name == EffectType.ROOTED)
            {
                return rootedPrefab;
            }
            else if (name == EffectType.RESISTANCE)
            {
                return resistancePrefab;
            }
            else if (name == EffectType.JINX)
            {
                return jinxPrefab;
            }
            else if (name == EffectType.KNOCKBACK)
            {
                return knockbackPrefab;
            }
            else if (name == EffectType.REVERSE)
            {
                return reversePrefab;
            }
            else if (name == EffectType.CHARM)
            {
                return charmPrefab;
            }
            else if (name == EffectType.GET_HIT)
            {
                return getHitPrefab;
            }
            else if (name == EffectType.SLEEPY)
            {
                return sleepyPrefab;
            }
            else if (name == EffectType.RESONANCE)
            {
                return resonancePrefab;
            }
            else if (name == EffectType.EXHAUSTED)
            {
                return exhaustedPrefab;
            }
            else if (name == EffectType.RESURRECTION)
            {
                return resurrectionPrefab;
            }
            else if (name == EffectType.SILENT)
            {
                return silentPrefab;
            }
            else if (name == EffectType.HEALING)
            {
                return healingPrefab;
            }
            else if (name == EffectType.VOID)
            {
                return voidPrefab;
            }
            return null;
        }

        public void deactivatePrefab(string name)
        {
            GameObject prefab = getEffectPrefab(name);
            if (prefab != null)
            {
                prefab.gameObject.SetActive(false);
            } else {
                Debug.LogError("Prefab not found: " + name);
            }
        }
        
        public void activatePrefab(string name)
        {
            GameObject prefab = getEffectPrefab(name);
            if (prefab != null)
            {
                prefab.gameObject.SetActive(true);
            } else {
                Debug.LogError("Prefab not found: " + name);
            }
        }
    }
}