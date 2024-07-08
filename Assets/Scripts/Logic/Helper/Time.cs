using System;

namespace Logic.Helper
{
    public static class Time
    {
        public static long WhatIsIt()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}