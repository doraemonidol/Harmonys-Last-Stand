using System.Collections.Generic;
using UnityEngine;

namespace Presentation.Bosses
{
    public class BossCastSkill : MonoBehaviour
    {
        [SerializeField] public List<BossSkill> skills;
        [SerializeField] private RotateToTargetScript _rotateToTarget;
        
        [SerializeField] private GameObject firePoint;
        [SerializeField] private GameObject target;
        
        // Start is called before the first frame update
        void Start()
        {
            if (!_rotateToTarget)
            {
                Debug.LogError("Please assign RotateToTargetScript to the boss");
            }
            
            InitializeSkills();
        }

        private void InitializeSkills()
        {
            for (int i = 0; i < skills.Count; i++)
            {
                skills[i].AttachRotator(_rotateToTarget);
                skills[i].AttachFirePoint(firePoint);
                skills[i].AttachTarget(target);
            }
        }

        public bool StartCasting(int skillIndex)
        {
            // if (skillIndex < 0 || skillIndex >= skills.Count) return false;
            // if (!skills[skillIndex].IsOnCoolDown())
            // {
                skills[skillIndex].nextCastTime = Time.time + Random.Range(1.0f, 10.0f);
                skills[skillIndex].StartCasting();
                return true;
            // }
            //
            // return false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // if (Random.Range(0, 2) == 1)
            // {
            //     int randomSkillIndex = Random.Range(0, skills.Count);
            //     if (!skills[randomSkillIndex].IsOnCoolDown())
            //     {
            //         skills[randomSkillIndex].nextCastTime = Time.time + Random.Range(1.0f, 10.0f);
            //         skills[randomSkillIndex].StartCasting();
            //     }
            // }
        }
    }
}
