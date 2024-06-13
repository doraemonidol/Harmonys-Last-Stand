namespace Logic.MainCharacters.Attributes
{
    public abstract class McAttributes
    {
        private int _baseHp;
        private int _baseMana;
        private int _baseSpeed;
        private int _baseDamage;
        protected abstract bool HasMode(string mode);
        protected abstract void HandleMoreQuery(string mode, string vars, int value);
        
        public int Query(string mode, string vars, int value = 0)
        {
            mode = mode.ToLower();
            vars = vars.ToLower();
            if (!HasMode(mode))
            {
                throw new System.Exception($"Mode {mode} does not exist.");
            }
            if (mode is "get" or "g")
            {
                switch (vars)
                {
                    case "hp":
                        return _baseHp;
                    case "mana":
                        return _baseMana;
                    case "spd":
                        return _baseSpeed;
                    case "dmg":
                        return _baseDamage;
                }
            }
            else if (mode is "set" or "s")
            {
                switch (vars)
                {
                    case "hp":
                        _baseHp = value;
                        break;
                    case "mana":
                        _baseMana = value;
                        break;
                    case "spd":
                        _baseSpeed = value;
                        break;
                    case "dmg":
                        _baseDamage = value;
                        break;
                }
            }
            HandleMoreQuery(mode, vars, value);
            return 0;
        }
    }
}