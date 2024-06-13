using System;
using System.Collections.Generic;

namespace DTO
{
    public class EventDto
    {
        public string Event { get; set; }
        
        public Dictionary<string, object> Arguments { get; set; }
    }
}