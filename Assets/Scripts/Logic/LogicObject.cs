using System.Collections;
using Common;
using Presentation;

namespace Logic
{
    public class LogicObject
    {
        public Identity SelfHandle { get; set; }
        public Identity PresentationHandle { get; set; }
        
        protected readonly ArrayList Subscribers = new ArrayList();

        protected LogicObject()
        {
            
        }

        protected LogicObject(LogicObject another)
        {
            foreach (var subscriber in another.Subscribers)
            {
                this.Subscribers.Add(subscriber);
            }
        }
        
        public void Subscribe(PresentationObject subscriber)
        {
            Subscribers.Add(subscriber);
        }
        
        public void Unsubscribe(PresentationObject subscriber)
        {
            Subscribers.Remove(subscriber);
        }

        public void Disconnect()
        {
            foreach (var subscriber in Subscribers)
            {
                var presentSubscriber = (PresentationObject)subscriber;
                presentSubscriber.Unsubscribe(this);
                this.Unsubscribe(presentSubscriber);
            }
        }
        
        public void NotifySubscribers(EventUpdateVisitor visitor)
        {
            foreach (PresentationObject subscriber in Subscribers)
            {
                subscriber.AcceptAndUpdate(visitor);
            }
        }
    }
}