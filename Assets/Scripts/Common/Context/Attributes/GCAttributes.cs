namespace Common.Context.Attributes
{
    public class GCAttributes
    {
        private int _dfHp;
        private int _dfMana;
        private int _dfDmg;
        private int _dfAtkSpd;
        private int _dfMovSpd;

        private int _curHp;

        private int _chHp;
        private int _chMana;
        private int _chDmg;
        private int _chMovSpd;
        private int _chAtkSpd;
        
        public GCAttributes(int dfHp, int dfMana, int dfDmg, int dfAtkSpd, int dfMovSpd)
        {
            _dfHp = dfHp;
            _dfMana = dfMana;
            _dfDmg = dfDmg;
            _dfAtkSpd = dfAtkSpd;
            _dfMovSpd = dfMovSpd;
            
            _curHp = _dfHp;
            
            _chHp = 0;
            _chMana = 0;
            _chDmg = 0;
            _chAtkSpd = 0;
            _chMovSpd = 0;
        }

        public int Query(
            string mode,
            string vars,
            int value = 0
        )
        {
            mode = mode.ToLower();
            vars = vars.ToLower();
            switch (mode) 
            {
                case "get":
                case "g":
                    switch (vars)
                    {
                        case "dhp":
                            return _dfHp;
                        case "dmana":
                            return _dfMana;
                        case "ddmg":
                            return _dfDmg;
                        case "datk-spd":
                            return _dfAtkSpd;
                        case "dmov-spd":
                            return _dfMovSpd;
                        case "hp":
                            return _curHp;
                        case "hp+":
                            return _chHp;
                        case "mana+":
                            return _chMana;
                        case "dmg+":
                            return _chDmg;
                        case "atk-spd+":
                            return _chAtkSpd;
                        case "mov-spd+":
                            return _chMovSpd;
                    }
                    break;
                case "set":
                case "s":
                    switch (vars)
                    {
                        case "dhp":
                            _dfHp = value;
                            return _dfHp;
                        case "dmana":
                            _dfMana = value;
                            return _dfMana;
                        case "ddmg":
                            _dfDmg = value;
                            return _dfDmg;
                        case "datk-spd":
                            _dfAtkSpd = value;
                            return _dfAtkSpd;
                        case "dmov-spd":
                            _dfMovSpd = value;
                            return _dfMovSpd;
                        case "hp":
                            _curHp = value;
                            return _curHp;
                        case "hp+":
                            _chHp = value;
                            return _chHp;
                        case "mana+":
                            _chMana = value;
                            return _chMana;
                        case "dmg+":
                            _chDmg = value;
                            return _chDmg;
                        case "atk-spd+":
                            _chAtkSpd = value;
                            return _chAtkSpd;
                        case "mov-spd+":
                            _chMovSpd = value;
                            return _chMovSpd;
                    }
                    break;
                case "add":
                case "+":
                    switch (vars)
                    {
                        case "dhp":
                            _dfHp += value;
                            return _dfHp;
                        case "dmana":
                            _dfMana += value;
                            return _dfMana;
                        case "ddmg":
                            _dfDmg += value;
                            return _dfDmg;
                        case "datk-spd":
                            _dfAtkSpd += value;
                            return _dfAtkSpd;
                        case "dmov-spd":
                            _dfMovSpd += value;
                            return _dfMovSpd;
                        case "hp":
                            _curHp += value;
                            return _curHp;
                        case "hp+":
                            _chHp += value;
                            return _chHp;
                        case "mana+":
                            _chMana += value;
                            return _chMana;
                        case "dmg+":
                            _chDmg += value;
                            return _chDmg;
                        case "atk-spd+":
                            _chAtkSpd += value;
                            return _chAtkSpd;
                        case "mov-spd+":
                            _chMovSpd += value;
                            return _chMovSpd;
                    }
                    break;
                case "minus":
                case "-":
                    switch (vars)
                    {
                        case "dhp":
                            _dfHp -= value;
                            return _dfHp;
                        case "dmana":
                            _dfMana -= value;
                            return _dfMana;
                        case "ddmg":
                            _dfDmg -= value;
                            return _dfDmg;
                        case "datk-spd":
                            _dfAtkSpd -= value;
                            return _dfAtkSpd;
                        case "dmov-spd":
                            _dfMovSpd -= value;
                            return _dfMovSpd;
                        case "hp":
                            _curHp -= value;
                            return _curHp;
                        case "hp+":
                            _chHp -= value;
                            return _chHp;
                        case "mana+":
                            _chMana -= value;
                            return _chMana;
                        case "dmg+":
                            _chDmg -= value;
                            return _chDmg;
                        case "atk-spd+":
                            _chAtkSpd -= value;
                            return _chAtkSpd;
                        case "mov-spd+":
                            _chMovSpd -= value;
                            return _chMovSpd;
                    }
                    break;
                case "reset":
                case "~":
                    switch (vars)
                    {
                        case "hp":
                            _curHp = _dfHp;
                            return _curHp;
                        case "hp+":
                            _chHp = 0;
                            return _chHp;
                        case "mana+":
                            _chMana = 0;
                            return _chMana;
                        case "dmg+":
                            _chDmg = 0;
                            return _chDmg;
                        case "atk-spd+":
                            _chAtkSpd = 0;
                            return _chAtkSpd;
                        case "mov-spd+":
                            _chMovSpd = 0;
                            return _chMovSpd;
                    }
                    break;
            }
            return 0;
        }
    }
}