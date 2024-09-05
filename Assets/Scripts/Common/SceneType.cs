namespace Common
{
    public static class SceneType
    {
        public const string MAINMENU = "MainMenu";
        
        public const string LOBBY = "LobbyScene";
        
        public const string AMADEUS_BOSS = "BossScene1";
        public const string AMADEUS_TROOP = "TroopScene1";
        public const string LUDWIG_BOSS = "BossScene2";
        public const string LUDWIG_TROOP = "TroopScene2";
        public const string MAESTRO_TROOP = "BossScene3";
        public const string MAESTRO_BOSS = "BossScene3";
        
        public static string GetScene(SceneTypeEnum sceneType) => sceneType switch
        {
            SceneTypeEnum.MAINMENU => MAINMENU,
            SceneTypeEnum.LOBBY => LOBBY,
            SceneTypeEnum.AMADEUS_BOSS => AMADEUS_BOSS,
            SceneTypeEnum.AMADEUS_TROOP => AMADEUS_TROOP,
            SceneTypeEnum.LUDWIG_BOSS => LUDWIG_BOSS,
            SceneTypeEnum.LUDWIG_TROOP => LUDWIG_TROOP,
            SceneTypeEnum.MAESTRO_TROOP => MAESTRO_TROOP,
            SceneTypeEnum.MAESTRO_BOSS => MAESTRO_BOSS,
            _ => throw new System.NotImplementedException()
        };
    }
}