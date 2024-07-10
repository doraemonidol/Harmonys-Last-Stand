using UnityEngine;

namespace Presentation.Projectiles
{
    public class StaticTargeting : ProjectileMovement
    {
        
        public override void Start()
        {
            var muzzleVfx = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            
            var ps = muzzleVfx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(muzzleVfx, ps.main.duration);
            }
            else
            {
                var psChild = muzzleVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVfx, psChild.main.duration);
            }
        }

        public override void FixedUpdate()
        {
            // throw new System.NotImplementedException();
        }

        public override void OnCollisionEnter(Collision other)
        {
            // throw new System.NotImplementedException();
        }

        public override void Ultimate()
        {
        }
    }
}
