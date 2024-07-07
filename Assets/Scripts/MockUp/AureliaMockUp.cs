using Common;
using Logic.Facade;
using Presentation;

namespace MockUp
{
    public class AureliaMockUp : PresentationObject
    {
        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(EntityType.AURELIA, this);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}