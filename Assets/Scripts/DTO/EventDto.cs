using System;
using System.Collections.Generic;

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
            set => Arguments[key] = value;
        }
    }
}