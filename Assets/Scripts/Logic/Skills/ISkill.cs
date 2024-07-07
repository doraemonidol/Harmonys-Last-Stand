using System;
using System.Collections.Generic;
using DTO;
using Logic.Helper;
using Logic.Skills.Flute;
using Logic.Skills.Guitar;
using Logic.Skills.Piano;
using Logic.Skills.Saxophone;
using Logic.Skills.SonicBass;
using Logic.Skills.Violin;
using Logic.Weapons;

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