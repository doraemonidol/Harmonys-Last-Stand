using Logic.Context.Attributes;
using Logic.Helper;

namespace Logic.Context
{
    public class GameContext
    {
        private static GCAttributes _attributes;
        
        private static GameContext _instance;
        
        private static EffectData _effectData;
        
        public static GameContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameContext();
            }
            return _instance;
        }

        public void Do(int actionHandle, int value)
        {
            switch (actionHandle)
            {
                case BoostHandles.BoostSpeed:
                    _attributes.Query("+", "spd", value);
                    break;
                case BoostHandles.BoostHealth:
                    _attributes.Query("+", "hp", value);
                    break;
                case BoostHandles.BoostDamage:
                    _attributes.Query("+", "dmg", value);
                    break;
                case BoostHandles.BoostMana:
                    _attributes.Query("+", "mana", value);
                    break;
                case BoostHandles.ReduceSpeed:
                    _attributes.Query("-", "spd", value);
                    break;
                case BoostHandles.ReduceHealth:
                    _attributes.Query("-", "hp", value);
                    break;
                case BoostHandles.ReduceDamage:
                    _attributes.Query("-", "dmg", value);
                    break;
                case BoostHandles.ReduceMana:
                    _attributes.Query("-", "mana", value);
                    break;
            }
        }

        public int Get(string vars)
        {
            var varsLower = vars.ToLower();
            switch (varsLower)
            {
                case "hp":
                    return _attributes.Query("get", "hp");
                case "mana":
                    return _attributes.Query("get", "mana");
                case "spd":
                    return _attributes.Query("get", "spd");
                case "dmg":
                    return _attributes.Query("get", "dmg");
                default:
                    return 0;
            }
        }
    }
}