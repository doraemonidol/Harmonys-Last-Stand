using System;
using System.Collections.Generic;
using System.Threading;
using DTO;
using Logic.Villains.Amadeus;
using Logic.Villains.Ludwig;
using Logic.Villains.Maestro;

namespace Logic.Villains.States
{
    public class ResultWaiting : IVillainState
    {
        private readonly Villain _owner;

        private readonly SkillCastingSession _session;
        
        private readonly object _lock = new object();
        
        private readonly Thread _thread;

        public ResultWaiting(Villain owner, SkillCastingSession session)
        {
            this._owner = owner;
            this._session = session;
            _thread = new Thread(() =>
            {
                var randomNumber = new Random().Next(1, 10);
                Thread.Sleep(randomNumber * 1000);
                OnStateUpdate(new Dictionary<string, object>
                {
                    ["result"] = false,
                });
            });
            _thread.Start();
        }
        
        public void OnStateUpdate(Dictionary<string, object> data = null)
        {
            lock (_lock)
            {
                var id = _session.SkillCastingIds[^1];
                _owner.VillainWeapon.Skills[id].Cancel();
                var result = (bool)data?["result"]!;
                if (!Thread.CurrentThread.Equals(_thread)) 
                    _thread.Abort();
                _session.SkillCastingResults.Add(result);
                _owner.SetState(
                    _owner.GetType() == typeof(MaestroMachina) ? 
                        new MmSkillCasting(_owner, _session) 
                        : _owner.GetType() == typeof(LudwigVanVortex) ? 
                            new LwSkillCasting(_owner, _session) 
                            : _owner.GetType() == typeof(AmadeusPrime) ? 
                                new AmadeusSkillCasting(_owner, _session) 
                                : throw new Exception("Invalid villain type")
                );
            }
        }
    }
}