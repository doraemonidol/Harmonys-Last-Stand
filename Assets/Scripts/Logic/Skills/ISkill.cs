using System.Collections.Generic;
using DTO;

namespace Logic.Skills
{
    public interface ISkill
    {
        public bool IsAvailable();

        public void Affect(ICharacter attacker, ICharacter target = null, EventDto context = null);
        
        public void Activate();

        public void Lock();

        public void Unlock();
    }
}