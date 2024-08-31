using System;
using System.Collections;
using Common;
using Logic;
using UnityEngine;

namespace Presentation
{
    public abstract class PresentationObject : MonoBehaviour
    {
        [SerializeField] public EntityTypeEnum entityType;
        public Identity SelfHandle { get; set; }
        
        public Identity LogicHandle { get; set; }
        
        private readonly ArrayList Notifier = new ArrayList();

        public abstract void Start();

        public abstract void Update();

        public abstract void AcceptAndUpdate(EventUpdateVisitor visitor);
        
        public void Subscribe(LogicObject notifier)
        {
            Notifier.Add(notifier);
        }
        
        public void Unsubscribe(LogicObject notifier)
        {
            Notifier.Remove(notifier);
        }
    }
}