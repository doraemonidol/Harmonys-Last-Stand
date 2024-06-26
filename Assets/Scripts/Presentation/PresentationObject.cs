using System.Collections;
using Logic;
using UnityEngine;

namespace Presentation
{
    public abstract class PresentationObject : MonoBehaviour
    {
        protected readonly IPObject BoxObject;
        
        private readonly ArrayList Notifier = new ArrayList();

        public abstract void Start();

        public abstract void Update();

        public void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            BoxObject.AcceptAndUpdate(visitor);
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