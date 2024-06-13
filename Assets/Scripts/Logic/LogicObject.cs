using System.Collections;
using Presentation;

namespace Logic.Skills
{
    public abstract class LogicObject
    {
        protected ArrayList _subscribers = new ArrayList();
        
        public void Subscribe(PresentationObject subscriber)
        {
            _subscribers.Add(subscriber);
        }
        
        public void Unsubscribe(PresentationObject subscriber)
        {
            _subscribers.Remove(subscriber);
        }
        
        public void NotifySubscribers(EventUpdateVisitor visitor)
        {
            foreach (PresentationObject subscriber in _subscribers)
            {
                subscriber.AcceptAndUpdate(visitor);
            }
        }
    }
}