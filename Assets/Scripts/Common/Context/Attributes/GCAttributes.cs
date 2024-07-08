namespace Logic.Context.Attributes
{
    public class GCAttributes
    {
        private int _dfHp;
        private int _dfMana;
        private int _dfDmg;
        private int _dfAtkSpd;
        private int _dfMovSpd;

        private int _curHp;
        private int _curMana;
        private int _curDmg;
        private int _curMovSpd;
        private int _curAtkSpd;
        
        public GCAttributes(int dfHp, int dfMana, int dfDmg, int dfAtkSpd, int dfMovSpd)
        {
            _dfHp = dfHp;
            _dfMana = dfMana;
            _dfDmg = dfDmg;
            _dfAtkSpd = dfAtkSpd;
            _dfMovSpd = dfMovSpd;
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
                        case "hp":
                            return _curHp;
                        case "mana":
                            return _curMana;
                        case "dmg":
                            return _curDmg;
                        case "atk-spd":
                            return _curAtkSpd;
                        case "mov-spd":
                            return _curMovSpd;
                    }
                    break;
                case "set":
                case "s":
                    switch (vars)
                    {
                        case "hp":
                            _dfHp = value;
                            return _curHp;
                        case "mana":
                            _dfMana = value;
                            return _curMana;
                        case "dmg":
                            _dfDmg = value;
                            return _curDmg;
                        case "atk-spd":
                            _dfAtkSpd = value;
                            return _curAtkSpd;
                        case "mov-spd":
                            _dfMovSpd = value;
                            return _curMovSpd;
                    }
                    break;
                case "add":
                case "+":
                    switch (vars)
                    {
                        case "hp":
                            _dfHp += value;
                            return _curHp;
                        case "mana":
                            _dfMana += value;
                            return _curMana;
                        case "dmg":
                            _dfDmg += value;
                            return _curDmg;
                        case "atk-spd":
                            _dfAtkSpd += value;
                            return _curAtkSpd;
                        case "mov-spd":
                            _dfMovSpd += value;
                            return _curMovSpd;
                    }
                    break;
                case "minus":
                case "-":
                    switch (vars)
                    {
                        case "hp":
                            _dfHp -= value;
                            return _curHp;
                        case "mana":
                            _dfMana -= value;
                            return _curMana;
                        case "dmg":
                            _dfDmg -= value;
                            return _curDmg;
                        case "atk-spd":
                            _dfAtkSpd -= value;
                            return _curAtkSpd;
                        case "mov-spd":
                            _dfMovSpd -= value;
                            return _curMovSpd;
                    }
                    break;
                case "reset":
                case "~":
                    switch (vars)
                    {
                        case "hp":
                            _curHp = _dfHp;
                            return _curHp;
                        case "mana":
                            _curMana = _dfMana;
                            return _curMana;
                        case "dmg":
                            _curDmg = _dfDmg;
                            return _curDmg;
                        case "atk-spd":
                            _curAtkSpd = _dfAtkSpd;
                            return _curAtkSpd;
                        case "mov-spd":
                            _curMovSpd = _dfMovSpd;
                            return _curMovSpd;
                    }
                    break;
            }
            return 0;
        }
    }
}