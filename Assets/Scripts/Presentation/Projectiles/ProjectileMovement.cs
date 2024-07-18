using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using MockUp;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Presentation.Projectiles
{
    [Serializable]
    public abstract class ProjectileMovement : MonoBehaviour
    {
        public GameObject muzzlePrefab;
        public GameObject hitPrefab;
        public List<GameObject> trails;
        protected Vector3 StartPos;
        public bool hasUltimate = false;
        protected Dictionary<string, object> skillCollideInfo;

        public void AssignSkillCollideInfo(Dictionary<string, object> info)
        {
            skillCollideInfo = info;
            Stack<GameObject> stack = new Stack<GameObject>();
            stack.Push(gameObject);
            while (stack.Count > 0)
            {
                GameObject current = stack.Pop();
                if (current.GetComponent<SkillColliderInfo>() == null)
                {
                    current.AddComponent<SkillColliderInfo>();
                    current.GetComponent<SkillColliderInfo>().Attacker = (Identity)info["Attacker"];
                    current.GetComponent<SkillColliderInfo>().Skill = (Identity)info["Skill"];
                    current.GetComponent<SkillColliderInfo>().affectCooldown = (float)info["affectCooldown"];
                }
                foreach (Transform child in current.transform)
                {
                    stack.Push(child.gameObject);
                }
            }
        }

        public void Start()
        {
            // Loop through every child of the projectile, check whether it contains any collider, if yes, add component SkillCollideInfo
            
        }
        public abstract void FixedUpdate();
        public abstract void OnCollisionEnter(Collision other);
        
        public abstract void Ultimate();
    }
}