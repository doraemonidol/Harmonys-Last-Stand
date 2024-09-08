using System;
using Common;
using Presentation.Helper;
using Presentation.Manager;
using Presentation.Sound;
using Runtime;
using UnityEngine;

namespace Presentation.GUI
{
    public class GameManager : MonoBehaviorInstance<GameManager>
    {
        public bool IsGamePaused { 
            get => Time.timeScale == 0;
            set => Time.timeScale = value ? 0 : 1;
        }
        
        private void Start()
        {
            Debug.Log("Game Manager Start");
        }

        private void OnEnable()
        {
            Debug.Log("Game Manager OnEnable");
            GameManager.Instance.InitializeGameSave();
        }
        
        public void PauseGame()
        {
            IsGamePaused = true;
            UIManager.Instance.ShowPauseGameMenu();
        }

        public void RestartGame()
        {
            SceneManager.Instance.ReloadCurrentScene();
        }

        public void LoadMainMenu()
        {
            SceneManager.Instance.LoadScene(SceneType.MAINMENU);
            // SoundManager.Instance.PlayMusic(MusicEnum.MenuMusic);
        }
        
        public void GameOver()
        {
            UIManager.Instance.ShowLosePanel();
        }

        public void InitializeGameSave()
        {
            PlayerData data = DataManager.Instance.LoadData();
            SceneManager.Instance.InitializeGame(data);
        }
        
        public void OnDefeatBoss(SceneTypeEnum sceneType)
        {
            DataManager.Instance.UnlockScene(SceneType.GetNextBossScene(sceneType));
            // SceneManager.Instance.LoadScene(SceneType.GetNextBossScene(sceneType));
            GlobalVariables.Set("isWin", true);
            UIManager.Instance.ShowWinPanel();
        }
    }
}