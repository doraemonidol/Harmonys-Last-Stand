using System.Collections.Generic;
using DTO;
using Logic.Skills;

namespace Logic.Villains.States
{
    public abstract class SkillCasting : IVillainState
    {
        protected Villain Owner;
        
        protected List<int> SkillIds;
        
        protected readonly SkillCastingSession Session;
        
        public int ChosenSkillId { get; protected set; }
        
        public SkillCasting(Villain owner, Dictionary<string, object> data = null)
        {
            Owner = owner;
            SkillIds = owner.GetAvailableSkills();
            Session = new SkillCastingSession();
            if (data != null)
            {
                ChosenSkillId = OnStateEnter(data);
            }
            else
            {
                ChosenSkillId = OnStateEnter();
            }
        }

        public SkillCasting(Villain owner, SkillCastingSession session, Dictionary<string, object> data = null)
            : this(owner, data)
        {
            Session = session;
        }

        protected abstract int OnStateEnter(Dictionary<string, object> data = null);

        protected abstract void OnStateExit(Dictionary<string, object> data = null);

        public abstract void OnStateUpdate(Dictionary<string, object> data = null);
    }
}