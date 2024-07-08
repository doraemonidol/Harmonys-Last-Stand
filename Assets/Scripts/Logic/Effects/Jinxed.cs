using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;

namespace Logic.Effects
{
    public class Jinxed : EffectCommand
    {
        public int BoostRange { get; }
        public int BoostHp { get; }
        public  int BoostMSp { get; }
        public  int BoostDmg { get; }
        public  int BoostAtkSpd { get; }
        public  int BoostMana { get; }
        
        public Jinxed(ICharacter character) : base(character)
        {
        }

        public Jinxed(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Jinxed(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            BoostHp = furArgs["boostHP"];
            BoostMSp = furArgs["boostMSp"];
            BoostAtkSpd = furArgs["boostAtkSpd"];
            BoostMana = furArgs["boostMana"];
            BoostDmg = furArgs["boostDmg"];
            BoostRange |= (BoostHp > 0) ? 1 : 0;
            BoostRange |= (BoostMSp > 0) ? 2 : 0;
            BoostRange |= (BoostAtkSpd > 0) ? 4 : 0;
            BoostRange |= (BoostMana > 0) ? 8 : 0;
            BoostRange |= (BoostDmg > 0) ? 16 : 0;
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                // Character.ReceiveEffect(EffectHandle.Jinxed, new EventDto
                // {
                //     ["ev"] = "jinxed",
                //     ["boostHP"] = _boostHp,
                //     ["boostMSp"] = _boostMSp,
                //     ["boostAtk"] = _boostDmg,
                //     ["boostAtkSpd"] = _boostAtkSpd,
                //     ["boostMana"] = _boostMana,
                // });
                
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableJinxed);
                
                NotifyWhenEnd();
            });
        }
    }
}