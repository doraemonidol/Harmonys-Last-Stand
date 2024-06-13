using System;
using System.Collections.Generic;

namespace Logic
{
    public class EventUpdateVisitor
    {
        private Dictionary<string, Dictionary<string, object>> _mEvents = new();
        
        // Overload the [] operator
        public Dictionary<string, object> this[string key]
        {
            get
            {
                if (!_mEvents.ContainsKey(key))
                {
                    _mEvents[key] = new Dictionary<string, object>();
                }
                return _mEvents[key];
            }
            set => _mEvents[key] = value;
        }
        
        // Overload the [][] operator
        public object this[string key, string subKey]
        {
            get
            {
                if (!_mEvents.ContainsKey(key))
                {
                    _mEvents[key] = new Dictionary<string, object>();
                }
                if (!_mEvents[key].ContainsKey(subKey))
                {
                    _mEvents[key][subKey] = null;
                }
                return _mEvents[key][subKey];
            }
            set => _mEvents[key][subKey] = value;
        }
    }
}