using UnityEngine;

public class PlayerSpecialSkill : PlayerSkill
{
    public override void UpdateChannelingTime(float timeScaleFactor)
    {
        Debug.LogError("Can't Channel Special Skill");
    }

    public override void StartChanneling()
    {
        Debug.LogError("Can't Channel Special Skill");
    }

    public override void StopChanneling()
    {
        Debug.LogError("Can't Channel Special Skill");
    }

    public override double GetChannelingTime()
    {
        Debug.LogError("Can't Channel Special Skill");
        return 0;
    }
}