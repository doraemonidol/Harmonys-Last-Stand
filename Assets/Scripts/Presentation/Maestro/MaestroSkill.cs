using System.Collections;
using System.Collections.Generic;
using Logic;
using Presentation.Bosses;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Presentation.Maestro
{
    public abstract class MaestroSkill : PSkill
    {
        [SerializeField] protected VFX preCastVfx;
        protected SkillState state;
        protected Animator animator;
        protected GameObject target;
        protected NavMeshAgent navMeshAgent;
        
        public override void Start()
        {
            state = SkillState.Idle;
        }

        public override void Update()
        {
        }
        
        public void Attach(Dictionary<string, Object> data)
        {
            if (data.TryGetValue("animator", out var value1))
            {
                animator = (Animator) value1;
            }
            else
            {
                Debug.LogError("MaestroSkill: Animator not found");
            }
            
            if (data.TryGetValue("target", out var value))
            {
                target = (GameObject) value;
            }
            else
            {
                Debug.LogError("MaestroSkill: Target not found");
            }
            
            if (data.TryGetValue("navMeshAgent", out var value2))
            {
                navMeshAgent = (NavMeshAgent) value2;
            }
            else
            {
                Debug.LogError("MaestroSkill: NavMeshAgent not found");
            }
        }

        protected IEnumerator StartPrecastVFX()
        {
            if (preCastVfx.HasVFX())
            {
                GameObject instantiatedVfx =  Instantiate(preCastVfx.vfx, transform.position, Quaternion.identity);
                
                if (preCastVfx.autoDestroy)
                {
                    Destroy(instantiatedVfx, preCastVfx.duration);
                }
            }

            yield return null;
        }

        public abstract IEnumerator StartCasting();

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
        
        public bool IsReady()
        {
            return state == SkillState.Idle;
        }
    }
}