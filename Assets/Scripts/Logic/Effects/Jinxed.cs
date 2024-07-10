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
        public  int BoostMovSpd { get; }
        public  int BoostDmg { get; }
        public  int BoostAtkSpd { get; }
        public  int BoostMana { get; }
        
        public Jinxed(ICharacter character) : base(character)
        {
            Handle = EffectHandle.Jinxed;
        }

        public Jinxed(ICharacter character, int timeout) : base(character, timeout)
        {
            Handle = EffectHandle.Jinxed;
        }

        public Jinxed(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
            Handle = EffectHandle.Jinxed;
            BoostHp = furArgs["boostHp"];
            BoostMovSpd = furArgs["boostMSp"];
            BoostAtkSpd = furArgs["boostAtkSpd"];
            BoostMana = furArgs["boostMana"];
            BoostDmg = furArgs["boostDmg"];
            BoostRange |= (BoostHp > 0) ? 1 : 0;
            BoostRange |= (BoostMovSpd > 0) ? 2 : 0;
            BoostRange |= (BoostAtkSpd > 0) ? 4 : 0;
            BoostRange |= (BoostMana > 0) ? 8 : 0;
            BoostRange |= (BoostDmg > 0) ? 16 : 0;
        }

        protected override void Disable()
        {
            Character.ReceiveEffect(EffectHandle.DisableJinxed);
        }
    }
}