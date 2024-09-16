using Logic;
using Presentation;
using UnityEngine;
using Debug = UnityEngine.Debug;

public abstract class PlayerSkill : BaseSkill
{
    public abstract void UpdateChannelingTime(float timeScaleFactor);
    public abstract void StartChanneling();
    public abstract void StopChanneling();
    public abstract double GetChannelingTime();

    public override void AcceptAndUpdate(EventUpdateVisitor visitor)
    {
        switch (visitor["ev"]["type"])
        {
            case "lock":
                isReady = false;
                float timeout = (float) visitor["context"]["timeout"];
                SkillDetailUI.Initialize(timeout);
                break;
            case "unlock":
                isReady = true;
                break;
        }
    }
    
    public PlayerSkill Copy()
    {
        return (PlayerSkill) MemberwiseClone();
    }
}
