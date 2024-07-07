using System;
using Common;

namespace Logic.Helper
{
    public static class Google
    {
        public static int Search(string page, string key)
        {
            return page switch
            {
                "instantiate" or "ins" => SearchInPageInstantiate(key),
                "effect" or "eff" => SearchInPageEffect(key),
                _ => throw new ArgumentOutOfRangeException(nameof(page), page, null)
            };
        }

        private static int SearchInPageEffect(string key)
        {
            key = key.ToLower();
            return key switch
            {
                "hallucination" or "hal" => EffectHandle.Hallucinate,
                "silent" or "sil" => EffectHandle.Silent,
                "guitar" => EntityType.GUITAR,
                "piano" => EntityType.PIANO,
                "saxophone" => EntityType.SAXOPHONE,
                "superbass" => EntityType.SUPERBASS,
                "violin" => EntityType.VIOLON,
                "troop" => EntityType.TROOP,
                "troop11" => EntityType.TROOP11,
                "amadeus" => EntityType.AMADEUS,
                "ludwig" => EntityType.LUDWIG,
                "maestro" => EntityType.MAESTRO,
                _ => throw new System.Exception("Key not found at page effect")
            };
        }

        private static int SearchInPageInstantiate(string key)
        {
            key = key.ToLower();
            return key switch
            {
                "aurelia" or "aur" => EntityType.AURELIA,
                "flute" or "flu" => EntityType.FLUTE,
                "guitar" or "gui" => EntityType.GUITAR,
                "piano" or "pia" => EntityType.PIANO,
                "saxophone" or "sax" => EntityType.SAXOPHONE,
                "superbass" or "sup" => EntityType.SUPERBASS,
                "violin" or "vio" => EntityType.VIOLON,
                "troop" or "tro" => EntityType.TROOP,
                "troop11" => EntityType.TROOP11,
                "amadeus" or "ama" => EntityType.AMADEUS,
                "ludwig" or "lud" => EntityType.LUDWIG,
                "maestro" or "mae" => EntityType.MAESTRO,
                _ => throw new System.Exception("Key not found at page instantiate")
            };
        }
    }
}