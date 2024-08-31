using Logic.Helper;

namespace Common
{
    public static class EntityType
    {
        public const int AURELIA = 0;
        public const int TROOP = 1;
        public const int VIOLON = WeaponHandle.Violin;
        public const int SAXOPHONE = WeaponHandle.Saxophone;
        public const int SUPERBASS = WeaponHandle.SuperBass;
        public const int PIANO = WeaponHandle.Piano;
        public const int FLUTE = WeaponHandle.Flute;
        public const int GUITAR = WeaponHandle.Guitar;
        public const int TROOP11 = 8;
        public const int TROOP12 = 9;
        public const int TROOP21 = 10;
        public const int TROOP22 = 11;
        public const int AMADEUS = 12;
        public const int LUDWIG = 13;
        public const int MAESTRO = 14;
        public const int WEAPON_MAESTRO = WeaponHandle.MmWeapon;
        public const int WEAPON_LUDWIG = WeaponHandle.LwWeapon;
        public const int WEAPON_AMADEUS = WeaponHandle.ApWeapon;

        public static int GetEntityType(EntityTypeEnum entityType)
        {
            return entityType switch
            {
                EntityTypeEnum.AURELIA => AURELIA,
                EntityTypeEnum.TROOP => TROOP,
                EntityTypeEnum.VIOLIN => VIOLON,
                EntityTypeEnum.SAXOPHONE => SAXOPHONE,
                EntityTypeEnum.SUPERBASS => SUPERBASS,
                EntityTypeEnum.PIANO => PIANO,
                EntityTypeEnum.FLUTE => FLUTE,
                EntityTypeEnum.GUITAR => GUITAR,
                EntityTypeEnum.TROOP11 => TROOP11,
                EntityTypeEnum.TROOP12 => TROOP12,
                EntityTypeEnum.TROOP21 => TROOP21,
                EntityTypeEnum.TROOP22 => TROOP22,
                EntityTypeEnum.AMADEUS => AMADEUS,
                EntityTypeEnum.LUDWIG => LUDWIG,
                EntityTypeEnum.MAESTRO => MAESTRO,
                EntityTypeEnum.WEAPON_MAESTRO => WEAPON_MAESTRO,
                EntityTypeEnum.WEAPON_LUDWIG => WEAPON_LUDWIG,
                EntityTypeEnum.WEAPON_AMADEUS => WEAPON_AMADEUS,
                _ => throw new System.Exception("Entity type not found")
            };
        }
    }
}