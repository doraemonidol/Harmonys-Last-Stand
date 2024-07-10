using System.Collections.Generic;
using Logic;
using Logic.Facade;
using Logic.Helper;
using Presentation;

namespace MockUp
{
    public class WeaponMockUp : PWeapon
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

            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "wam"), this);
        }

        public override void Update()
        {
            
        }

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            
        }
    }
}