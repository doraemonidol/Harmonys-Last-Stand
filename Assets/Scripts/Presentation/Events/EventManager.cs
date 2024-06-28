using Logic.Effects;
using UnityEngine;

namespace Presentation.Events
{
    public class EventManager
    {
        private EffectManager _effectManager;
        private MovementManager _movementManager;
        
        public EventManager()
        {
            _effectManager = new EffectManager();
            _movementManager = new MovementManager();
        }
        
        
    }
}