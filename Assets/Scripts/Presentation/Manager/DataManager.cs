using System.Collections.Generic;
using Common;
using Runtime;
using UnityEngine;

namespace Presentation.Manager
{
    public class PlayerData
    {
        public bool UnlockedAmadeus = true;
        public bool UnlockedLudwig = false;
        public bool UnlockedMaestro = false;
        public int Money = 0;
        public EntityTypeEnum Weapon1 = EntityTypeEnum.FLUTE;
        public EntityTypeEnum Weapon2 = EntityTypeEnum.VIOLIN;
        public List<EntityTypeEnum> UnlockedWeapons = new List<EntityTypeEnum>();
    }
    public class DataManager : MonoBehaviorInstance<DataManager>
    {
        public void SaveData(PlayerData data)
        {
            string currentFileSave = PlayerPrefs.GetString(DataKeyType.CurrentFileSave, "filesave1");
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(currentFileSave, json);
        }
        
        public PlayerData LoadData()
        {
            string currentFileSave = PlayerPrefs.GetString(DataKeyType.CurrentFileSave, "filesave1");
            string json = PlayerPrefs.GetString(currentFileSave, "");
            if (json == "")
            {
                return new PlayerData();
            }
            return JsonUtility.FromJson<PlayerData>(json);
        }
        
        public void UnlockScene(string sceneType)
        {
            PlayerData data = LoadData();
            switch (sceneType)
            {
                case SceneType.AMADEUS_BOSS:
                    data.UnlockedAmadeus = true;
                    break;
                case SceneType.LUDWIG_BOSS:
                    data.UnlockedLudwig = true;
                    break;
                case SceneType.MAESTRO_BOSS:
                    data.UnlockedMaestro = true;
                    break;
            }
            SaveData(data);
        }
        
        public void AddMoney(int money)
        {
            PlayerData data = LoadData();
            data.Money += money;
            SaveData(data);
        }
        
        public void SetWeapon1(EntityTypeEnum weapon)
        {
            PlayerData data = LoadData();
            if (weapon == data.Weapon2)
            {
                data.Weapon2 = data.Weapon1;
            }
            data.Weapon1 = weapon;
            SaveData(data);
        }
        
        public void SetWeapon2(EntityTypeEnum weapon)
        {
            PlayerData data = LoadData();
            if (weapon == data.Weapon1)
            {
                data.Weapon1 = data.Weapon2;
            }
            data.Weapon2 = weapon;
            SaveData(data);
        }
    }
}