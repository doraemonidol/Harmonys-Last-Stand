using Common;
using DTO;
using Presentation;

namespace Logic.Facade
{
    public interface ILogicFacade
    {
        public void Instantiate(int type, PresentationObject presRef);
        
        public void Destroy(Identity identity);

        public void Observe(EventDto eventDto);
    }
}