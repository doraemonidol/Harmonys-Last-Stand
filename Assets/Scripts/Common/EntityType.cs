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
        public const int WEAPON_TROOP = WeaponHandle.TrWeapon;

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
                EntityTypeEnum.WEAPON_TROOP => WEAPON_TROOP,
                _ => throw new System.Exception("Entity type not found")
            };
        }
        
        public static string GetEntityName(EntityTypeEnum entityType)
        {
            return entityType switch
            {
                EntityTypeEnum.NONE => "None",
                EntityTypeEnum.AURELIA => "Aurelia",
                EntityTypeEnum.TROOP => "Troop",
                EntityTypeEnum.VIOLIN => "Violin",
                EntityTypeEnum.SAXOPHONE => "Saxophone",
                EntityTypeEnum.SUPERBASS => "Superbass",
                EntityTypeEnum.PIANO => "Piano",
                EntityTypeEnum.FLUTE => "Flute",
                EntityTypeEnum.GUITAR => "Guitar",
                EntityTypeEnum.TROOP11 => "Troop11",
                EntityTypeEnum.TROOP12 => "Troop12",
                EntityTypeEnum.TROOP21 => "Troop21",
                EntityTypeEnum.TROOP22 => "Troop22",
                EntityTypeEnum.AMADEUS => "Amadeus",
                EntityTypeEnum.LUDWIG => "Ludwig",
                EntityTypeEnum.MAESTRO => "Maestro",
                EntityTypeEnum.WEAPON_MAESTRO => "Maestro Weapon",
                EntityTypeEnum.WEAPON_LUDWIG => "Ludwig Weapon",
                EntityTypeEnum.WEAPON_AMADEUS => "Amadeus Weapon",
                EntityTypeEnum.WEAPON_TROOP => "Troop Weapon",
                _ => throw new System.Exception("Entity type not found")
            };
        }
    }
}