using UnityEngine;

namespace Presentation.Projectiles
{
    public class SpreadingAOETargeting : ProjectileMovement
    {
        [SerializeField] private float _maximalRange = 10f;
        [SerializeField] private float _spreadSpeed = 1f;
        public override void Start()
        {
            
        }

        public override void FixedUpdate()
        {
        }

        public override void OnCollisionEnter(Collision other)
        {
        }

        public override void Ultimate()
        {
            throw new System.NotImplementedException();
        }
    }
}