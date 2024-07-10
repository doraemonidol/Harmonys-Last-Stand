using System.Collections.Generic;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;

namespace MockUp
{
    public class WeaponMockUp2 : PWeapon
    {
        public override void Start()
        {
            
            NormalSkills = new List<PSkill>
            {
                gameObject.AddComponent<Wp1>(),
                gameObject.AddComponent<Wp1>(),
            };
            
            SpecialSkills = new List<PSkill>
            {
                gameObject.AddComponent<Wp1>(),
            };

            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "vio"), this);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            throw new System.NotImplementedException();
        }
    }
}