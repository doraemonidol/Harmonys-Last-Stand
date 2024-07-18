using Common;
using UnityEngine;

namespace MockUp
{
    public class SkillColliderInfo : MonoBehaviour
    {
        [SerializeField] public Identity Attacker;
        [SerializeField] public Identity Skill;
        [SerializeField] public float affectCooldown = 0;
        
        // add constructor
        public SkillColliderInfo(Identity attacker, Identity skill)
        {
            Attacker = attacker;
            Skill = skill;
        }
    }
}