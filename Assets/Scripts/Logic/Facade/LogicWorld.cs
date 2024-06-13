using System;
using System.Collections.Generic;
using Common;
using DTO;
using Logic.Helper;
using Logic.MainCharacters;
using Logic.Villains;

namespace Logic.Facade
{
    public class LogicWorld : ILogicFacade
    {
        private static LogicWorld _instance;
        
        private static readonly object Lock = new object();

        private readonly Dictionary<Identity, object> _identities;

        private readonly Dictionary<object, Identity> _objects;

        private LogicWorld()
        {
            _identities = new Dictionary<Identity, object>();
            _objects = new Dictionary<object, Identity>();
        }

        private static object Create(int type)
        {
            return type switch
            {
                LogicObjHandle.Aurelia => new Aurelia(),
                LogicObjHandle.Mozart => new Mozart(),
                LogicObjHandle.Maestro => new Maestro(),
                _ => null
            };
        }

        public static LogicWorld GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            lock (Lock)
            {
                return _instance ??= new LogicWorld();
            }
        }
        
        /**
         * <param name="type">The type of the object/ See also: <seealso cref="LogicObjHandle"/></param>
         * <param name="presRef">The presentation object itself.</param>
         * <summary>
         * Instantiate an object in the logic view.
         * Subscribe the presentation object to the logic object.
         * Finally, return the identity of the logic object.
         * </summary>
         */
        public Identity Instantiate(int type, object presRef)
        {
            var identity = new Identity(presRef.GetType().Name);
            var logicRef = Create(type);
            _identities.Add(identity, logicRef);
            _objects.Add(logicRef, identity);
            throw new Exception("This is not done yet.");
            return identity;
        }
        
        /**
         * <param name="identity">The identity of the object to be destroyed.</param>
         * <summary>
         * Destroy the Logic View of the object.
         * </summary>
         */
        public void Destroy(Identity identity)
        {
            var obj = _identities[identity];
            _identities.Remove(identity);
            _objects.Remove(obj);
        }

        public IResultDto Handle(EventDto eventDto)
        {
            throw new System.NotImplementedException();
        }
    }
}