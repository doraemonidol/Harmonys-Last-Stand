using System;
using System.Collections.Generic;
using Common;

namespace DTO
{
    public class EventDto
    {
        public string Event { get; set; }

        private Dictionary<string, object> Arguments { get; set; } = new();
        
        // Implement the [string] method
        public object this[string key]
        {
            get => Arguments.GetValueOrDefault(key);
            set
            {
                if (key == "timeout")
                {
                    Arguments[key] = (int)value * GameStats.BASE_TIME_UNIT;
                }
                else Arguments[key] = value;
            } 
        }
    }
}