using System;
using Common;
using UnityEngine;

namespace Presentation.Effect
{
    [Serializable]
    public abstract class BaseEffect : PresentationObject
    {
        [SerializeField] protected float duration;
        [SerializeField] protected GameObject vfx;
        protected GameObject VFXInstance;
        public abstract void PlayEffect(string effectType);
    }  
}