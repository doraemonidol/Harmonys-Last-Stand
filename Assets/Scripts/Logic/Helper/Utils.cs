using System.Collections.Generic;
using UnityEngine;

namespace Logic.Helper
{
    public class Utils
    {
        public static float GetDistance(KeyValuePair<float, float> pos1, KeyValuePair<float, float> pos2)
        {
            return Mathf.Sqrt(Mathf.Pow(pos1.Key - pos2.Key, 2) + Mathf.Pow(pos1.Value - pos2.Value, 2));
        }
    }
}