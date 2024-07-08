using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Helper;
using Logic.Skills;
using Logic.Weapons;
using UnityEngine.TextCore.Text;

namespace Logic.Effects
{
    public class Clone : EffectCommand
    {
        public Clone(ICharacter character) : base(character)
        {
        }

        public Clone(ICharacter character, int timeout) : base(character, timeout)
        {
        }

        public Clone(ICharacter character, int timeout, Dictionary<string, int> furArgs) : base(character, timeout, furArgs)
        {
        }

        public override void Execute()
        {
            var thread = new Thread(() =>
            {
                // Character.UpdateEffect(EffectHandle.Clone, new EventDto
                // {
                //     ["ev"] = "clone",
                // });
                
                while (System.DateTime.Now.Millisecond < EffectEndTime)
                {
                    Thread.Sleep(1000);
                }
                
                Character.ReceiveEffect(EffectHandle.DisableClone);
                
                NotifyWhenEnd();
            });
        }
    }
}