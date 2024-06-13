using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public interface ICharacter
    {
        public void ReceiveEffect(int ev);
        public void Do(int action, Dictionary<string, Object> args);
    }
}