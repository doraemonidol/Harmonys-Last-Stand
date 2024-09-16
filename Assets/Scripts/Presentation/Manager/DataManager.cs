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
    }
}