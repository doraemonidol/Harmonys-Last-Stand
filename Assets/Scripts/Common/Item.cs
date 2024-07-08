namespace Common
{
    public static class Item
    {
        public const int HEALTH_POTION = 0;
        public const int MANA_POTION = 1;
        public const int MOVE_SPEED_POTION = 2;
        public const int ATK_SPEED_POTION = 3;
        public const int DAMAGE_POTION = 4;
        public const int FLUTE = 5;
        public const int PIANO = 6;
        public const int GUITAR = 7;
        public const int VIOLIN = 8;
        public const int SUPER_BASS = 9;
        public const int SAXOPHONE = 10;

        private const int PRICE_HEALTH_POTION = 100;
        private const int PRICE_MANA_POTION = 100;
        private const int PRICE_MOVE_SPEED_POTION = 100;
        private const int PRICE_ATK_SPEED_POTION = 100;
        private const int PRICE_DAMAGE_POTION = 100;
        private const int PRICE_FLUTE = 100;
        private const int PRICE_PIANO = 100;
        private const int PRICE_GUITAR = 100;
        private const int PRICE_SAXOPHONE = 100;
        private const int PRICE_VIOLIN = 100;
        private const int PRICE_SUPER_BASS = 100;

        public static int GetPrice(int itemId)
        {
            return itemId switch
            {
                HEALTH_POTION => PRICE_HEALTH_POTION,
                MANA_POTION => PRICE_MANA_POTION,
                MOVE_SPEED_POTION => PRICE_MOVE_SPEED_POTION,
                ATK_SPEED_POTION => PRICE_ATK_SPEED_POTION,
                DAMAGE_POTION => PRICE_DAMAGE_POTION,
                FLUTE => PRICE_FLUTE,
                PIANO => PRICE_PIANO,
                GUITAR => PRICE_GUITAR,
                SAXOPHONE => PRICE_SAXOPHONE,
                VIOLIN => PRICE_VIOLIN,
                SUPER_BASS => PRICE_SUPER_BASS,
                _ => 0
            };
        }
    }
}