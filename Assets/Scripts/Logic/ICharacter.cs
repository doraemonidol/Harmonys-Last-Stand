using System.Collections.Generic;
using DTO;

namespace Logic
{
    public interface ICharacter
    {
        public void UpdateEffect(int ev, EventDto args = null);
        public void ReceiveEffect(int ev, EventDto args = null);
        public bool IsEffectApplied(int ev);
        public void OnDead();
        public void Do(int action, Dictionary<string, object> args);
    }
}