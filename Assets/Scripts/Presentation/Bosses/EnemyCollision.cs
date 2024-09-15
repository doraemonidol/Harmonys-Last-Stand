using System.Collections.Generic;
using Common;
using DTO;
using Logic.Facade;
using MockUp;
using UnityEngine;

namespace Presentation.Bosses
{
    public class EnemyCollision : MonoBehaviour
    {
        public Identity LogicHandle;
        public string Handle;
        private Dictionary<Identity, float> skillNextAffectedTime = new Dictionary<Identity, float>();
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy")) return;
            // Debug.Log(this.LogicHandle + "EnemyCollision Tag: " + other.gameObject.tag + " " + other.gameObject.name);
            
            SkillColliderInfo info = other.gameObject.GetComponent<SkillColliderInfo>();
            
            if (info == null) return;
            
            if (!skillNextAffectedTime.ContainsKey(info.Skill) || Time.time > skillNextAffectedTime[info.Skill])
            {
                skillNextAffectedTime[info.Skill] = Time.time + info.affectCooldown;
                
                Debug.Log("EnemyCollision: " + other.gameObject.name);
                Debug.Log(other.gameObject.GetComponent<SkillColliderInfo>().Attacker + " used " + 
                          other.gameObject.GetComponent<SkillColliderInfo>().Skill + " on " + 
                          this.LogicHandle);
            
                var eventd = new EventDto
                {
                    Event = "GET_ATTACKED",
                    ["attacker"] = other.gameObject.GetComponent<SkillColliderInfo>().Attacker,
                    ["target"] = LogicHandle,
                    ["context"] = null,
                    ["skill"] = other.gameObject.GetComponent<SkillColliderInfo>().Skill
                };
                LogicLayer.GetInstance().Observe(eventd);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            SkillColliderInfo info = other.GetComponent<SkillColliderInfo>();
            
            if (info == null) return;
            
            if (!skillNextAffectedTime.ContainsKey(info.Skill) || Time.time > skillNextAffectedTime[info.Skill])
            {
                skillNextAffectedTime[info.Skill] = Time.time + info.affectCooldown;
                
                Debug.Log("EnemyCollision: " + other.name);
                Debug.Log(other.GetComponent<SkillColliderInfo>().Attacker + " used " + 
                          other.GetComponent<SkillColliderInfo>().Skill + " on " + 
                          this.LogicHandle);
            
                var eventd = new EventDto
                {
                    Event = "GET_ATTACKED",
                    ["attacker"] = other.GetComponent<SkillColliderInfo>().Attacker,
                    ["target"] = LogicHandle,
                    ["context"] = null,
                    ["skill"] = other.GetComponent<SkillColliderInfo>().Skill
                };
                LogicLayer.GetInstance().Observe(eventd);
            }
        }
        
    }
}