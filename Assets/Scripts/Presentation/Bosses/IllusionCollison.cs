using Common;
using DTO;
using Logic.Facade;
using MockUp;
using UnityEngine;

namespace Presentation.Bosses
{
    public class IllusionCollison : MonoBehaviour
    {
        public Identity LogicHandle;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy")) return;
            // Debug.Log(this.LogicHandle + "EnemyCollision Tag: " + other.gameObject.tag + " " + other.gameObject.name);
            
            if (other.gameObject.GetComponent<SkillColliderInfo>() == null) return;
            
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

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log(this.LogicHandle + "Particle Collided with " + other.name);
            if (other.GetComponent<SkillColliderInfo>())
            {
                Debug.Log(this.LogicHandle + "Particle Collided with " + other.name);
                // Debug.Log(other.GetComponent<SkillColliderInfo>().Attacker + " used " + 
                //           other.GetComponent<SkillColliderInfo>().Skill + " on " + 
                //           this.LogicHandle);

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
        
    }
}