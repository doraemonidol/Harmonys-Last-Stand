namespace Logic.MainCharacters
{
    public interface IMainCharacter : ICharacter
    {
        public bool IsDead(int health);
        public void OnDead();
    }
}