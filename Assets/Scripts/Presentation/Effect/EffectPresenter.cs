using Common;
using Logic;
using UnityEngine;

namespace Presentation.Effect
{
    public class EffectPresenter : BaseEffect
    {
        [SerializeField] private GameObject mainCameraVfxContainer;
        [SerializeField] private GameObject entityVfxContainer;
        
        public override void OnEnable()
        {
            
        }

        public override void Update()
        {
            
        }

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            throw new System.NotImplementedException();
        }

        public override void PlayEffect(string type)
        {
            if (type == EffectType.SHIELD)
            {
                 VFXInstance = Instantiate(vfx, transform.position, Quaternion.identity);
                 vfx.transform.SetParent(entityVfxContainer.transform);
            }
        }
    }
}