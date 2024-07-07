using System.Collections.Generic;
using Logic.Facade;
using Logic.Helper;
using Presentation;

namespace MockUp
{
    public class WeaponMockUp : PWeapon
    {
        public override void Start()
        {
            Skills = new List<PSkill>
            {
                gameObject.AddComponent<Wp1>(),
                gameObject.AddComponent<Wp1>(),
                gameObject.AddComponent<Wp1>(),
                gameObject.AddComponent<Wp1>()
            };

            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "flu"), this);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}