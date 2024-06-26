using System;

namespace Common
{
    public class Identity
    {
        public string Handle { get; }

        public string Type { get; set; }

        public Identity(string type)
        {
            // Generate a random string UUID
            Handle = new Guid( /* 84ef02ac-e02e-4583-888a-b6dca0333a0a */).ToString();
            Type = type;
        }
    }
}