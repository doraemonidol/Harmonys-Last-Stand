using System.Collections.Generic;
using DTO;
using UnityEngine;

namespace Logic.Villains
{
    public class Maestro : IVillain
    {
        public void UpdateEffect(int ev, EventDto args = null)
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveEffect(int ev, EventDto args = null)
        {
            throw new System.NotImplementedException();
        }

        public bool IsEffectApplied(int ev)
        {
            throw new System.NotImplementedException();
        }

        public void Do(int action, Dictionary<string, Object> args)
        {
            throw new System.NotImplementedException();
        }
    }
}