using System.Collections;
using Common.Context.Attributes;
using Logic;
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

        private static int UnlockedLevel { get; set; } = 0;

        private static ArrayList UnlockedWeapons { get; } = new ArrayList();
        
        private static ArrayList Inventory { get; } = new ArrayList();
        
        public ICharacter MainCharacter { get; set; }
        
        public int Money { get; set; } = 0;
        public bool Saved { get; set; }
        
        private GameContext()
        {
            _attributes = new GCAttributes (
                GameStats.AURELIA_HEALTH, 
                100, 
                GameStats.AURELIA_ATTACK,
                GameStats.AURELIA_ATKSPEED,
                GameStats.AURELIA_MOVSPEED
            );
        }
        
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
                case BoostHandles.BoostDefHealth:
                    _attributes.Query("+", "hp", value);
                    break;
                case BoostHandles.BoostDefDamage:
                    _attributes.Query("+", "dmg", value);
                    break;
                case BoostHandles.BoostDefAtkSpd:
                    _attributes.Query("+", "atk-spd", value);
                    break;
                case BoostHandles.BoostDefMovSpd:
                    _attributes.Query("+", "mov-spd", value);
                    break;
                case BoostHandles.BoostMovSpd:
                    _attributes.Query("+", "mov-spd+", value);
                    break;
                case BoostHandles.BoostHealth:
                    _attributes.Query("+", "hp", value);
                    break;
                case BoostHandles.BoostDamage:
                    _attributes.Query("+", "dmg+", value);
                    break;
                case BoostHandles.BoostMana:
                    _attributes.Query("+", "mana+", value);
                    break;
                case BoostHandles.BoostAtkSpd:
                    _attributes.Query("+", "atk-spd+", value);
                    break;
                case BoostHandles.ReduceMovSpd:
                    _attributes.Query("-", "mov-spd+", value);
                    break;
                case BoostHandles.ReduceHealth:
                    _attributes.Query("-", "hp", value);
                    break;
                case BoostHandles.ReduceDamage:
                    _attributes.Query("-", "dmg+", value);
                    break;
                case BoostHandles.ReduceMana:
                    _attributes.Query("-", "mana+", value);
                    break;
                case BoostHandles.Reset:
                    _attributes.Query("~", "mov-spd+");
                    _attributes.Query("~", "atk-spd+");
                    _attributes.Query("~", "dmg+");
                    break;
                case BoostHandles.HardReset:
                    _attributes.Query("~", "mov-spd+");
                    _attributes.Query("~", "atk-spd+");
                    _attributes.Query("~", "dmg+");
                    _attributes.Query("~", "hp");
                    _attributes.Query("~", "mana+");
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
                "hp" 
                or "mana+" 
                or "atk-spd+" 
                or "mov-spd+" 
                or "dmg+" 
                or "dfhp"
                or "dfmana"
                or "dfatk-spd"
                or "dfmov-spd"
                or "dfdmg"
                    => _attributes.Query("get", varsLower),
                _ => 0
            };
        }

        public void Set(string vars, int value)
        {
            var varsLower = vars.ToLower();
            switch (varsLower)
            {
                case "hp":
                    _attributes.Query("set", "hp", value);
                    break;
                case "mana+":
                    _attributes.Query("set", "mana+", value);
                    break;
                case "atk-spd+":
                    _attributes.Query("set", "atk-spd+", value);
                    break;
                case "mov-spd+":
                    _attributes.Query("set", "mov-spd+", value);
                    break;
                case "dmg+":
                    _attributes.Query("set", "dmg+", value);
                    break;
            }
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