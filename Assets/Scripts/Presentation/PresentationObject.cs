using Logic;

namespace Presentation
{
    public abstract class PresentationObject
    {
        public abstract void AcceptAndUpdate(EventUpdateVisitor visitor);
    }
}