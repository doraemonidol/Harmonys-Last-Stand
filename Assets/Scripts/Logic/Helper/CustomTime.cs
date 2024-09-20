using System;
using UnityEngine;

namespace Logic.Helper
{
    public static class CustomTime
    {
        public static long WhatIsIt()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
            // return (long)(Time.time * 1000);
        }
    }
}