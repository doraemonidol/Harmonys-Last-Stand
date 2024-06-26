namespace Logic.Helper
{
    public static class EffectHandle
    {
        // ------------------------------------- EFFECTS ----------------------------------- //
        public const int Hallucinate = 1;
        public const int Silent = 2;
        public const int Stunt = 3;
        public const int Bleeding = 4;
        public const int Rooted = 5;
        public const int Fear = 6;
        public const int Nearsight = 7;
        public const int SlowDown = 8;
        public const int Shielded = 9;
        public const int Healing = 10;
        public const int Void = 11;
        public const int Exhausted = 12;
        public const int Sleepy = 13;
        public const int Jinxed = 14;
        public const int Resonance = 15;
        
        // --------------------------------------- GET HIT ---------------------------------- //
        public const int GetHit = 11;
        
        // -------------------------------------- DISABLE ----------------------------------- //
        public const int DisableHallucinate = 100;
        public const int DisableSilent = 99;
        public const int DisableStunt = 98;
        public const int DisableBleeding = 97;
        public const int DisableRooted = 96;
        public const int DisableFear = 95;
        public const int DisableNearsight = 94;
        public const int DisableSlowDown = 93;
        public const int DisableShielded = 92;
        
        
        // --------------------------------------- PARAMS ----------------------------------- //
        public const string HpDrain = "HpDrain";
        public const string HpReduce = "HpReduce";
        public const string HpGain = "HpGain";
        public const string Timeout = "Timeout";
        
        // --------------------------------------- STATS ------------------------------------ //
        public const int MysticMelodyDmg = 15;
        
    }
}