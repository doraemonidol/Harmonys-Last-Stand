using System;
using System.Collections.Generic;

namespace Logic.Villains.States
{
    public interface IVillainState
    {
        public void OnStateUpdate(Dictionary<string, object> data = null);
    }
}