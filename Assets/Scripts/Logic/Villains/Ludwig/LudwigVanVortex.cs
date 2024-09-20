using Logic.Helper;
using Logic.Weapons;
using static Common.GameStats;

namespace Logic.Villains.Ludwig
{
    public class LudwigVanVortex : Villain
    {
        public LudwigVanVortex()
        : base(
            LUDWIG_HEALTH,
            LUDWIG_ATKSPEED,
            LUDWIG_MOVSPEED,
            LUDWIG_ATTACK
        ) {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.LwWeapon);
            State = new LwSkillCasting(this);
        }
        
        public LudwigVanVortex(LogicObject another) : base(another)
        {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.LwWeapon);
            State = new LwSkillCasting(this);
        }
    }
}