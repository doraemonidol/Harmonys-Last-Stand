using Common;
using UnityEngine;

namespace MockUp
{
    public class SkillColliderInfo : MonoBehaviour
    {
        [SerializeField] public Identity Attacker;
        [SerializeField] public Identity Skill;
        [SerializeField] public float affectCooldown = 0;
        
        public void Initialize(Identity attacker, Identity skill)
        {
            Attacker = attacker;
            Skill = skill;
        }
        
        public SkillColliderInfo(Identity attacker, Identity skill)
        {
            Attacker = attacker;
            Skill = skill;
        }
    }
}