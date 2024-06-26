using System;
using System.Collections;
using Common;
using Logic.Facade;
using Logic.Skills;
using Logic.Weapons.Attributes;

namespace Logic.Weapons
{
    public class Violin : IWeapon
    {
        private WAttributes _attributes;

        public Violin()
        {
            _attributes = new WAttributes();
            var identity = new Identity("Violin");
            LogicWorld.Register(this, new Identity(""));
        }
        
        public void Trigger(int index)
        {
            var skill = _attributes.Get(index);
            //skill.Activate();
        }
    }
}