using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Presentation.UI;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
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
        [SerializeField] private EffectPrefabManager effectPrefabManager;

        private float lastUpdateFear = 0;
        
        public void AddEffect(EffectUI effect)
        {
            effect.Duration /= GameStats.BASE_TIME_UNIT;
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

        public void RemoveEffect(string name)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].Name == name)
                {
                    // deactivatePrefab(effects[i].Name);
                    effects[i].Duration = 0;
                    break;
                }
            }
        }
        
        public void Start()
        {
            if (effectPrefabManager == null)
            {
                Debug.LogError("Please assign EffectPrefabManager to the boss");
                return;
            }
            
            effectPrefabManager.stuntPrefab.gameObject.SetActive(false);
            effectPrefabManager.bleedingPrefab.gameObject.SetActive(false);
            effectPrefabManager.hallucinationPrefab.gameObject.SetActive(false);
            // fearPrefab.gameObject.SetActive(false);
            effectPrefabManager.nearsightPrefab.gameObject.SetActive(false);
            effectPrefabManager.shieldPrefab.gameObject.SetActive(false);
            effectPrefabManager.rootedPrefab.gameObject.SetActive(false);
            effectPrefabManager.resistancePrefab.gameObject.SetActive(false);
            effectPrefabManager.jinxPrefab.gameObject.SetActive(false);
            effectPrefabManager.knockbackPrefab.gameObject.SetActive(false);
            effectPrefabManager.reversePrefab.gameObject.SetActive(false);
            effectPrefabManager.charmPrefab.gameObject.SetActive(false);
            effectPrefabManager.getHitPrefab.gameObject.SetActive(false);
            effectPrefabManager.sleepyPrefab.gameObject.SetActive(false);
            effectPrefabManager.resonancePrefab.gameObject.SetActive(false);
            effectPrefabManager.exhaustedPrefab.gameObject.SetActive(false);
            effectPrefabManager.resurrectionPrefab.gameObject.SetActive(false);
            effectPrefabManager.silentPrefab.gameObject.SetActive(false);
            effectPrefabManager.healingPrefab.gameObject.SetActive(false);
            effectPrefabManager.voidPrefab.gameObject.SetActive(false);
            // StartCoroutine(Test());
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
                    
                    if (effects[i].Name == EffectType.FEAR)
                    {
                        this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                        this.gameObject.GetComponent<NavMeshAgent>().ResetPath();
                    }
                    
                    deactivatePrefab(effects[i].Name);
                    Debug.Log("Removing effect");
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

            if (IsAppliedEffect(EffectType.FEAR) && Time.time > lastUpdateFear + 0.4f)
            {
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(
                    this.gameObject.transform.position + 
                    new Vector3(UnityEngine.Random.Range(-8f, 8f), 0, UnityEngine.Random.Range(-8f, 8f))
                );
                lastUpdateFear = Time.time;
            }
        }
        
        private GameObject getEffectPrefab(string name)
        {
            if (name == EffectType.STUNT)
            {
                return effectPrefabManager.stuntPrefab;
            }
            else if (name == EffectType.BLEEDING)
            {
                return effectPrefabManager.bleedingPrefab;
            }
            else if (name == EffectType.HALLUCINATION)
            {
                return effectPrefabManager.hallucinationPrefab;
            }
            else if (name == EffectType.FEAR)
            {
                return effectPrefabManager.fearPrefab;
            }
            else if (name == EffectType.NEARSIGHT)
            {
                return effectPrefabManager.nearsightPrefab;
            }
            else if (name == EffectType.SHIELD)
            {
                return effectPrefabManager.shieldPrefab;
            }
            else if (name == EffectType.ROOTED)
            {
                return effectPrefabManager.rootedPrefab;
            }
            else if (name == EffectType.RESISTANCE)
            {
                return effectPrefabManager.resistancePrefab;
            }
            else if (name == EffectType.JINX)
            {
                return effectPrefabManager.jinxPrefab;
            }
            else if (name == EffectType.KNOCKBACK)
            {
                return effectPrefabManager.knockbackPrefab;
            }
            else if (name == EffectType.REVERSE)
            {
                return effectPrefabManager.reversePrefab;
            }
            else if (name == EffectType.CHARM)
            {
                return effectPrefabManager.charmPrefab;
            }
            else if (name == EffectType.GET_HIT)
            {
                return effectPrefabManager.getHitPrefab;
            }
            else if (name == EffectType.SLEEPY)
            {
                return effectPrefabManager.sleepyPrefab;
            }
            else if (name == EffectType.RESONANCE)
            {
                return effectPrefabManager.resonancePrefab;
            }
            else if (name == EffectType.EXHAUSTED)
            {
                return effectPrefabManager.exhaustedPrefab;
            }
            else if (name == EffectType.RESURRECTION)
            {
                return effectPrefabManager.resurrectionPrefab;
            }
            else if (name == EffectType.SILENT)
            {
                return effectPrefabManager.silentPrefab;
            }
            else if (name == EffectType.HEALING)
            {
                return effectPrefabManager.healingPrefab;
            }
            else if (name == EffectType.VOID)
            {
                return effectPrefabManager.voidPrefab;
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
        
        public bool IsAppliedEffect(string name)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}