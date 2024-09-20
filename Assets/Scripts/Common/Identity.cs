using System;

namespace Common
{
    public class Identity
    {
        public string Handle { get; }

        public Identity()
        {
            // Generate a random string UUID
            Handle = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return Handle;
        }
    }
}