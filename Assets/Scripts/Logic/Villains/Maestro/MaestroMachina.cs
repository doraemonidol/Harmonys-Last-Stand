using System;
using DTO;
using Logic.Helper;
using Logic.Weapons;
using static Common.GameStats;

namespace Logic.Villains.Maestro
{
    public class MaestroMachina : Villain
    {
        public MaestroMachina()
        : base(
            MAESTRO_HEALTH,
            MAESTRO_ATKSPEED,
            MAESTRO_MOVSPEED,
            MAESTRO_ATTACK
        ) {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.MmWeapon);
            State = new MmSkillCasting(this);
        }
        
        public MaestroMachina(LogicObject another) : base(another)
        {
            VillainWeapon = Weapon.TransformInto(WeaponHandle.MmWeapon);
            State = new MmSkillCasting(this);
        }
        
        protected override void CustomReceiveEffect(int ev, EventDto args = null)
        {
            if (ev == EffectHandle.Resurrect)
            {
                this.NotifySubscribers(new EventUpdateVisitor
                {
                    ["ev"] =
                    {
                        ["type"] = "resurrect",
                    },
                    ["stats"] =
                    {
                        ["hp"] = 0.7f * MAESTRO_HEALTH,
                    }
                });
            }
            else throw new Exception("Invalid effect type.");
        }

        public override void OnDead()
        {
            if (base.GetAvailableSkills().Contains(4))
            {
                this.VillainWeapon.Trigger(4, this);
            }
            else
            {
                this.NotifySubscribers(new EventUpdateVisitor
                {
                    ["ev"] =
                    {
                        ["type"] = "dead",
                    }
                });
            }
        }
    }
}