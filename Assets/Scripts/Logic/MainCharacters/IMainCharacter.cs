using System.Collections.Generic;
using Logic.Weapons;

namespace Logic.MainCharacters
{
    public interface IMainCharacter : ICharacter
    {
        public void TakeWeapon(List<Weapon> wpLists);
        public void ReceiveNewWeapon(int weapon);
        public void Switch(int wpIndex);
        public bool IsDead(int health);
    }
}