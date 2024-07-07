using Logic.Facade;
using Logic.Helper;
using Presentation;

namespace MockUp
{
    public class MaestroMockUp : PresentationObject
    {
        public override void Start()
        {
            LogicLayer.GetInstance().Instantiate(Google.Search("ins", "mae"), this);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}