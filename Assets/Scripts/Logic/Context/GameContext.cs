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
        
        /// <summary>
        /// This method is used to do the update on the attributes of the main character.
        /// </summary>
        /// <param name="actionHandle">The action handle./ See also: <seealso cref="BoostHandles"/></param>
        /// <param name="value">The value that query takes as parameter.</param>
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
        
        /// <summary>
        /// This method is used to do the get on the attributes of the main character.
        /// </summary>
        /// <param name="vars">The variable that should be gotten.</param>
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