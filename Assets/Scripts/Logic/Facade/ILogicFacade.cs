using System;
using Common;
using DTO;

namespace Logic.Facade
{
    public interface ILogicFacade
    {
        public Identity Instantiate(int type, object presRef);
        
        public void Destroy(Identity identity);

        public IResultDto Handle(EventDto eventDto);
    }
}