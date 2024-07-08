using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;
using Logic.Skills;
using Logic.Weapons;

namespace Logic.Effects
{
    public class Exhaust : EffectCommand
    {
        public int ExRange { get; }
        public int ExHp { get; }
        public int ExMSp{ get; }
        public int ExASp{ get; }
        public int ExMana{ get; }
        public int ExDmg{ get; }
        
        public Exhaust(ICharacter character) : base(character)
        {
        }

        public Exhaust(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Exhaust(ICharacter character, int timeout, Dictionary<string, int> furArgs) : 
            base(character, timeout, furArgs)
        {
            ExHp = furArgs["exHp"];
            ExMSp = furArgs["exMSp"];
            ExASp = furArgs["exASp"];
            ExMana = furArgs["exMana"];
            ExDmg = furArgs["exDmg"];
            ExRange |= (ExHp > 0) ? 1 : 0;
            ExRange |= (ExMSp > 0) ? 2 : 0;
            ExRange |= (ExASp > 0) ? 4 : 0;
            ExRange |= (ExMana > 0) ? 8 : 0;
            ExRange |= (ExDmg > 0) ? 16 : 0;
        }
        

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableExhausted);
                
                NotifyWhenEnd();
            });
        }
    }
}