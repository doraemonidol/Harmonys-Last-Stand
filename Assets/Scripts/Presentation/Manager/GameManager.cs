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
        public Cinemachine.CinemachineVirtualCamera BookCamera;
        public bool isOpeningBook = false;
        
        private void Start()
        {
            Debug.Log("Game Manager Start");
            BookCamera.gameObject.SetActive(false);
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
            IsGamePaused = false;
            SceneManager.Instance.ReloadCurrentScene();
        }

        public void LoadMainMenu()
        {
            IsGamePaused = false;
            SceneManager.Instance.LoadScene(SceneType.MAINMENU);
            // SoundManager.Instance.PlayMusic(MusicEnum.MenuMusic);
        }
        
        public void GameOver()
        {
            IsGamePaused = true;
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

        public void OpenBook()
        {
            Debug.Log("Open Book");
            isOpeningBook = true;
            UIManager.Instance.OpenBook();
            BookCamera.gameObject.SetActive(true);
        }
        
        public void CloseBook()
        {
            isOpeningBook = false;
            UIManager.Instance.CloseBook();
            BookCamera.gameObject.SetActive(false);
        }
    }
}