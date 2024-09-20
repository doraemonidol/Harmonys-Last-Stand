using System;
using Common;
using Presentation.GUI;
using Presentation.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.UI
{
    public class GameSaveUIController : MonoBehaviour
    {
        [SerializeField] private int saveSlot = 1;
        [SerializeField] private GameObject dataPanel;
        [SerializeField] private GameObject newGamePanel;
        [SerializeField] private GameObject amadeusUnlocked;
        [SerializeField] private GameObject ludwigUnlocked;
        [SerializeField] private GameObject maestroUnlocked;
        [SerializeField] private Text weapon1Text;
        [SerializeField] private Text weapon2Text;
        [SerializeField] private Text CoinsText;

        public void Start()
        {
            PlayerData data = DataManager.Instance.LoadData("filesave" + saveSlot);

            if (data == null)
            {
                dataPanel.SetActive(false);
                newGamePanel.SetActive(true);
            }
            else
            {
                dataPanel.SetActive(true);
                newGamePanel.SetActive(false);
                amadeusUnlocked.SetActive(data.UnlockedAmadeus);
                ludwigUnlocked.SetActive(data.UnlockedLudwig);
                maestroUnlocked.SetActive(data.UnlockedMaestro);
                weapon1Text.text = EntityType.GetEntityName(data.Weapon1);
                weapon2Text.text = EntityType.GetEntityName(data.Weapon2);
                CoinsText.text = data.Money.ToString() + " coin";
                if (data.Money > 0) CoinsText.text += "s";
            }
        }

        public void OnClick()
        {
            PlayerPrefs.SetString(DataKeyType.CurrentFileSave, "filesave" + saveSlot);
            SceneManager.Instance.LoadScene(SceneType.LOBBY);
        }
    }
}