using System;
using System.Collections.Generic;

namespace DTO
{
    public class EventDto
    {
        public string Event { get; set; }
        
        public Dictionary<string, object> Arguments { get; set; }
        
        // Implement the [string] method
        public object this[string key]
        {
            get => Arguments[key];
            set => Arguments[key] = value;
        }
    }
}