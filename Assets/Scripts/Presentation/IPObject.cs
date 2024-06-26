using Logic;

namespace Presentation
{
    public interface IPObject
    {
        void AcceptAndUpdate(EventUpdateVisitor visitor);
    }
}