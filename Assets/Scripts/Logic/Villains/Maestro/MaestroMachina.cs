using System;
using Common;
using DTO;
using Logic.Helper;
using Logic.Weapons;
using UnityEngine;
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
            Debug.Log("MaestroMachina CustomReceiveEffect" + ev + EffectHandle.Resurrect);
            if (ev == EffectHandle.Resurrect)
            {
                this.NotifySubscribers(new EventUpdateVisitor
                {
                    ["ev"] =
                    {
                        ["type"] = "start-effect",
                    },
                    ["args"] =
                    {
                        ["name"] = EffectType.RESURRECTION,
                        ["current-health"] = (int)Mathf.Floor(0.7f * MAESTRO_HEALTH),
                        ["max-health"] = MAESTRO_HEALTH,
                    }
                });
                this.Health = (int)Mathf.Floor(0.7f * MAESTRO_HEALTH);
            }
            else throw new Exception("Invalid effect type.");
        }

        public override void OnDead()
        {
            
            if (base.GetAvailableSkills().Contains(3))
            {
                this.VillainWeapon.Trigger(3, this);
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