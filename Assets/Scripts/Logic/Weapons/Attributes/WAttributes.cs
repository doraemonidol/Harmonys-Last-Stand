using System.Collections;
using Logic.Skills;

namespace Logic.Weapons.Attributes
{
    public class WAttributes
    {
        private readonly ArrayList _skills = new();
        
        private bool IfSkillExists(ISkill skill)
        {
            return _skills.Contains(skill);
        }

        public void Add(ISkill skill)
        {
            _skills.Add(skill);
        }
        
        public ISkill Get(int index)
        {
            if (index > _skills.Count || index < 0)
            {
                throw new System.Exception(
                    "At WAttributes: The skill index is out of range. " +
                    $"Size: {_skills.Count}. Query: {index}."
                );
            }
            return (ISkill) _skills[index];
        }
    }
}