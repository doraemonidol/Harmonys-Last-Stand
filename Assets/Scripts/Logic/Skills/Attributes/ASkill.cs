using UnityEngine;

namespace Logic.Skills
{
    public class ASkill
    {
        public long NextTimeAvailable { get; }
        public bool IsLocked { get; }

        public ASkill()
        {
            // Get the current time
            var currentTime = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
            // Set the next time available to the current time
            NextTimeAvailable = currentTime;
            // Set the skill to be unlocked
            IsLocked = false;
        }
    }
}