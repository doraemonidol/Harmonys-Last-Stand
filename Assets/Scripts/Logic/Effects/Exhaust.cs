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
            Handle = EffectHandle.Exhausted;
        }

        public Exhaust(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Exhausted;
        }

        public Exhaust(ICharacter character, int timeout, Dictionary<string, int> furArgs) : 
            base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Exhausted;
            ExHp = furArgs["exHp"];
            ExMSp = furArgs["exMSp"];
            ExASp = furArgs["exAtkSpd"];
            ExMana = furArgs["exMana"];
            ExDmg = furArgs["exDmg"];
            ExRange |= (ExHp > 0) ? 1 : 0;
            ExRange |= (ExMSp > 0) ? 2 : 0;
            ExRange |= (ExASp > 0) ? 4 : 0;
            ExRange |= (ExMana > 0) ? 8 : 0;
            ExRange |= (ExDmg > 0) ? 16 : 0;
        }
        
        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableExhausted);
        }
    }
}