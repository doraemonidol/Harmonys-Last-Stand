using Logic;

namespace Presentation.Bosses
{
    public class BossSkill : BaseSkill
    {
        public override void Start()
        {
            
        }

        public override void Update()
        {
        }

        public override void AcceptAndUpdate(EventUpdateVisitor visitor)
        {
            switch (visitor["ev"]["type"])
            {
                case "lock":
                    isReady = false;
                    break;
                case "unlock":
                    isReady = true;
                    break;
            }
        }
    }
}