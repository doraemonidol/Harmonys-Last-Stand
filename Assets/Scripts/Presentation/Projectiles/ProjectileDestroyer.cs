using UnityEngine;

namespace Presentation.Projectiles
{
    public class ProjectileDestroyer
    {
        [SerializeField] private bool _destroyAfterDuration = false;
        [SerializeField] private float _destroyTime = 5f;
        [SerializeField] private bool _destroyOnCollision = true;
        [SerializeField] private bool _destroyOnReachingRange = true;
    }
}