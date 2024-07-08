using System.Collections;
using Logic.Context.Attributes;
using Logic.Helper;

namespace Common.Context
{
    public class GameContext
    {
        public string SessionId { get; set; }
        
        private static readonly object Lock = new object();
        
        private static GCAttributes _attributes;
        
        private static GameContext _instance;
        
        private static EffectData _effectData;

        private static int UnlockedLevel { get; set; } = 0;

        private static ArrayList UnlockedWeapons { get; } = new ArrayList();
        
        private static ArrayList Inventory { get; } = new ArrayList();
        
        private static int Money { get; set; } = 0;
        public bool Saved { get; set; }
        
        public static GameContext GetInstance()
        {
            lock (Lock)
            {
                return _instance ??= new GameContext();
            }
        }
        
        /// <summary>
        /// This method is used to do the update on the attributes of the main character.
        /// </summary>
        /// <param name="actionHandle">The action handle./ See also: <seealso cref="BoostHandles"/></param>
        /// <param name="value">The value that query takes as parameter.</param>
        public void Do(int actionHandle, int value = 0)
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
                case BoostHandles.Reset:
                    _attributes.Query("~", "mov-spd");
                    _attributes.Query("~", "atk-spd");
                    _attributes.Query("~", "dmg");
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
            return varsLower switch
            {
                "hp" or "mana" or "atk-spd" or "mov-spd" or "dmg" => _attributes.Query("get", varsLower),
                _ => 0
            };
        }

        public void LoadWeapon(string data)
        {
        }

        public void LoadInventory(string data)
        {
        }

        public void LoadStats(string data)
        {
            
        }

        public void LoadGameplay(string data)
        {
            
        }
    }
}