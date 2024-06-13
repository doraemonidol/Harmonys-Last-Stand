namespace Logic.Skills
{
    public interface ISkill
    {
        public void IsAvailable();
        public void Activate(ICharacter activator, ICharacter affected);

        public void Lock();

        public void Unlock();
    }
}