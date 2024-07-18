using Logic;
using Logic.Helper;
using Time = UnityEngine.Time;

namespace Presentation.Bosses
{
    public class BossSkill : BaseSkill
    {
        public float nextCastTime;
        
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

        public bool IsOnCoolDown()
        {
            return Time.time < nextCastTime;
        }
    }
}