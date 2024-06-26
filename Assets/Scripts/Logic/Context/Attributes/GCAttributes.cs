namespace Logic.Context.Attributes
{
    public class GCAttributes
    {
        private int _incHp;
        private int _incMana;
        private int _incDmg;
        private int _incSpd;
        
        public GCAttributes(int incHp, int incMana, int incDmg, int incSpd)
        {
            _incHp = incHp;
            _incMana = incMana;
            _incDmg = incDmg;
            _incSpd = incSpd;
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
                            return _incHp;
                        case "mana":
                            return _incMana;
                        case "dmg":
                            return _incDmg;
                        case "spd":
                            return _incSpd;
                    }
                    break;
                case "set":
                case "s":
                    switch (vars)
                    {
                        case "hp":
                            _incHp = value;
                            return _incHp;
                        case "mana":
                            _incMana = value;
                            return _incMana;
                        case "dmg":
                            _incDmg = value;
                            return _incDmg;
                        case "spd":
                            _incSpd = value;
                            return _incSpd;
                    }
                    break;
                case "add":
                case "+":
                    switch (vars)
                    {
                        case "hp":
                            _incHp += value;
                            return _incHp;
                        case "mana":
                            _incMana += value;
                            return _incMana;
                        case "dmg":
                            _incDmg += value;
                            return _incDmg;
                        case "spd":
                            _incSpd += value;
                            return _incSpd;
                    }
                    break;
                case "minus":
                case "-":
                    switch (vars)
                    {
                        case "hp":
                            _incHp -= value;
                            return _incHp;
                        case "mana":
                            _incMana -= value;
                            return _incMana;
                        case "dmg":
                            _incDmg -= value;
                            return _incDmg;
                        case "spd":
                            _incSpd -= value;
                            return _incSpd;
                    }
                    break;
            }
            return 0;
        }
    }
}