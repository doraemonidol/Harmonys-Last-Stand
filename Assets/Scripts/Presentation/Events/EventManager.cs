using Logic.Effects;
using UnityEngine;

namespace Presentation.Events
{
    public class EventManager
    {
        private EffectUIManager _effectUIManager;
        private MovementManager _movementManager;
        
        public EventManager()
        {
            _effectUIManager = new EffectUIManager();
            _movementManager = new MovementManager();
        }
        
        
    }
}