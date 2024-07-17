using UnityEngine;

namespace Presentation.Projectiles
{
    public class StaticTargeting : ProjectileMovement
    {
        public void Start()
        {
            base.Start();
            var muzzleVfx = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            
            muzzleVfx.transform.parent = transform;
            AssignSkillCollideInfo(this.skillCollideInfo);
            
            var ps = muzzleVfx.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(muzzleVfx, ps.main.duration);
                Destroy(gameObject, ps.main.duration);
            }
            else
            {
                if (muzzleVfx.transform.childCount > 0)
                {
                    var psChild = muzzleVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(muzzleVfx, psChild.main.duration);
                    Destroy(gameObject, psChild.main.duration);
                }
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
