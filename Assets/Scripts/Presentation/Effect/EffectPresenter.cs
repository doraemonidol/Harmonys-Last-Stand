using Common;
using UnityEngine;

namespace Presentation.Effect
{
    public class EffectPresenter : BaseEffect
    {
        [SerializeField] private GameObject mainCameraVfxContainer;
        [SerializeField] private GameObject entityVfxContainer;
        
        public override void Start()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void PlayEffect(EffectType type)
        {
            if (type == EffectType.SHIELDED)
            {
                 VFXInstance = Instantiate(vfx, transform.position, Quaternion.identity);
                 vfx.transform.SetParent(entityVfxContainer.transform);
            }
        }
    }
}