namespace Logic.MainCharacters
{
    public interface IMainCharacter : ICharacter
    {
        public void ReceiveNewWeapon(int weapon);
        public void Switch(int wpIndex);
        public bool IsDead(int health);
        public void OnDead();
    }
}