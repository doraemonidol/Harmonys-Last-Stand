namespace Logic.Skills.Violin
{
    public class MelodicSlash : ISkill
    {
        private ASkill _attribute;
        
        public void IsAvailable()
        {
            if (_attribute.IsLocked)
            {
                return;
            }
        }

        public void Activate(ICharacter activator, ICharacter affected)
        {
            try
            {
                affected.ReceiveEffect("");
            }
            catch (System.Exception e)
            {
                throw new System.Exception("Failed to apply effect to affected character", e);
            }
        }

        public void Lock()
        {
            throw new System.NotImplementedException();
        }

        public void Unlock()
        {
            throw new System.NotImplementedException();
        }
    }
}