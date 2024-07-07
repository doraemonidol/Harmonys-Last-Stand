using System;
using System.Collections;
using Common;
using Logic;
using UnityEngine;

namespace Presentation
{
    public abstract class PresentationObject : MonoBehaviour
    {
        public Identity SelfHandle { get; set; }
        
        public Identity LogicHandle { get; set; }
        
        private readonly ArrayList Notifier = new ArrayList();

        public abstract void Start();

        public abstract void Update();

        public void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            //throw new Exception("Receive an new message from visitor");
        }
        
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