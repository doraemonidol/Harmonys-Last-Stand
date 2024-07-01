using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        public abstract void Start();
        public abstract void FixedUpdate();
        public abstract void OnCollisionEnter(Collision other);
        
        public abstract void Ultimate();
    }
}