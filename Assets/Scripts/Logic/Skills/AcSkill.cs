using System;
using System.Collections.Generic;
using System.Threading;
using Common;
using DTO;
using Logic.Helper;
using Logic.Skills.AmadeusPrime;
using Logic.Skills.Flute;
using Logic.Skills.Guitar;
using Logic.Skills.LudwigVanVortex;
using Logic.Skills.MaestroMachina;
using Logic.Skills.Piano;
using Logic.Skills.Saxophone;
using Logic.Skills.SonicBass;
using Logic.Skills.Troop;
using Logic.Skills.Violin;
using Logic.Villains;
using Logic.Weapons;
using Presentation.Maestro;
using JackInTheBoxMayhem = Logic.Skills.MaestroMachina.JackInTheBoxMayhem;

namespace Logic.Skills
{
    public abstract class AcSkill : LogicObject, ISkill
    {
        protected Weapon Owner;
        
        protected ICharacter User;
        
        public long NextTimeToAvailable { get; set; }
        
        protected long CoolDownTime { get; set; }
        
        protected bool Locked = false;

        private Thread _thread;
        
        protected AcSkill(Weapon owner)
        {
            this.Owner = owner;
            this.CoolDownTime = 5000;
        }

        protected AcSkill(Weapon owner, ICharacter user)
        {
            User = user;
        }

        protected AcSkill(Weapon owner, long coolDownTime)
        {
            this.Owner = owner;
            this.CoolDownTime = coolDownTime;
        }
        
        protected AcSkill(Weapon owner, long coolDownTime, ICharacter user)
        {
            this.Owner = owner;
            this.CoolDownTime = coolDownTime;
            User = user;
        }

        public bool IsAvailable()
        {
            return (!Locked) && (CustomTime.WhatIsIt() >= NextTimeToAvailable);
        }
        
        public static AcSkill TransformInto(int wpName, Weapon wp, int index, int coolDownTime = 200, ICharacter user = null)
        {
            return wpName switch
            {
                WeaponHandle.Violin => index switch
                {
                    1 => new MelodicSlash(wp, coolDownTime),
                    2 => new EchoingWaltz(wp, coolDownTime),
                    3 => new HarmonyShield(wp, coolDownTime),
                    4 => new CresendoBurst(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.SuperBass => index switch
                {
                    1 => new SonicSound(wp, coolDownTime),
                    2 => new JuliaSong(wp, coolDownTime),
                    3 => new BombasticDrop(wp, coolDownTime),
                    4 => new VoidCancellation(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.Saxophone => index switch
                {
                    1 => new BlazingSolo(wp, coolDownTime),
                    2 => new SmoothSerenade(wp, coolDownTime),
                    3 => new JazzJolt(wp, coolDownTime),
                    4 => new SonicSwirl(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.Flute => index switch
                {
                    1 => new WhistlingWind(wp, coolDownTime),
                    2 => new TranquilTune(wp, coolDownTime),
                    4 => new TempestTones(wp, 6 * GameStats.BASE_TIME_UNIT),
                    3 => new MysticMelody(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.Guitar => index switch
                {
                    1 => new StringStrike(wp, coolDownTime),
                    2 => new PowerChord(wp, coolDownTime),
                    3 => new SoloSurge(wp, coolDownTime), 
                    4 => new RiffRumble(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.Piano => index switch
                {
                    1 => new ResonantStrike(wp, coolDownTime),
                    2 => new NocturneEmbrace(wp, coolDownTime),
                    3 => new RhapsodyRampage(wp, coolDownTime),
                    4 => new AllegroAgility(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.ApWeapon => index switch
                {
                    1 => new RequiemWrath(wp, coolDownTime),
                    2 => new SymphonyStorm(wp, coolDownTime),
                    3 => new Encore(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.LwWeapon => index switch
                {
                    1 => new FuryOfTheFifth(wp, coolDownTime),
                    2 => new MoonlightMenace(wp, coolDownTime),
                    3 => new TheSkiesDescend(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.MmWeapon => index switch
                {
                    1 => new PhantomStab(wp, coolDownTime),
                    2 => new JackInTheBoxMayhem(wp, coolDownTime),
                    3 => new IllusionCarnival(wp, coolDownTime),
                    4 => new RequiemResurrection(wp, 30 * GameStats.BASE_TIME_UNIT),
                    _ => throw new Exception("Unknown skill"),
                },
                WeaponHandle.TrWeapon => index switch
                {
                    1 => new NormalAttack(wp, coolDownTime),
                    _ => throw new Exception("Unknown skill"),
                },
                _ => throw new Exception("Unknown weapon"),
            };
        }

        public abstract void Affect(ICharacter attacker, ICharacter target, EventDto context);

        public virtual void Activate(ICharacter activator)
        {
            User = activator;
            
            if (this.Locked)
            {
                throw new Exception("Exception thrown when trying to activate a locked skill");
            }
            if (this.NextTimeToAvailable > CustomTime.WhatIsIt())
            {
                throw new Exception("Exception thrown when trying to activate a skill that is not available");
            }
            
            this.NextTimeToAvailable = CustomTime.WhatIsIt() + CoolDownTime;
            
            this.Lock();
        }
        
        private void CallVisitor(string typeEv, Dictionary<string, object> context)
        {
            var visitor = new EventUpdateVisitor
            {
                ["ev"] =
                {
                    ["type"] = typeEv
                },
                ["context"] = new Dictionary<string, object>(context)
            };
            this.NotifySubscribers(visitor);
        }

        public void Lock()
        {
            this.Locked = true;
            CallVisitor(
                "lock",
                new Dictionary<string, object>
                {
                    ["timeout"] = (float)this.CoolDownTime / GameStats.BASE_TIME_UNIT
                }
            );
            _thread = new Thread(() =>
            {
                while (CustomTime.WhatIsIt() < this.NextTimeToAvailable)
                {
                    Thread.Sleep(500);
                }
                this.Unlock();
            });
            _thread.Start();
        }

        public void Unlock()
        {
            this.Locked = false;
            CallVisitor("unlock", new Dictionary<string, object>());
        }
        
        public void ImmediateUnlock()
        {
            _thread.Abort();
            this.Locked = false;
            CallVisitor("unlock", new Dictionary<string, object>());
        }

        public virtual void Cancel()
        {
        }
    }
}