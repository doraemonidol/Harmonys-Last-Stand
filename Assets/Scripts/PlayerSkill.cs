using Logic;
using Presentation;
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
                break;
            case "unlock":
                isReady = true;
                break;
        }
    }
}
