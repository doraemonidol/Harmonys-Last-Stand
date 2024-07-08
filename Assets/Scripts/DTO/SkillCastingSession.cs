using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// Represents a skill casting session. Use this to retrieve the information of a skill casting of one villain.
    /// </summary>
    public class SkillCastingSession
    {
        public List<int> SkillCastingIds { get; set; } = new();
        
        public List<long> SkillCastingTimes { get; set; } = new();
        
        public List<bool> SkillCastingResults { get; set; } = new();
        
        /*
         * The definition of one success casting phase is to be able to cause damage to the target.
         */
    }
}