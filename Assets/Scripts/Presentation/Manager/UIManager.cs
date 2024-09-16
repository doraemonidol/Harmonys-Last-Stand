using Common;
using Presentation.Sound;
using Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation.GUI
{
    public class UIManager : MonoBehaviorInstance<UIManager>
    {
        [Header("Overlays")]
        [SerializeField] private GameObject _overlay;

        [Header("Main menu")]
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionMenu;
        
        [Header("Option Menu")]
        [SerializeField] private GameObject _audioSettings;
        [SerializeField] private GameObject _controlSettings;

        [Header("Lobby")]
        [SerializeField] private GameObject _startGamePanel;

        [SerializeField] private GameObject _amadeusConfirmationText;
        [SerializeField] private GameObject _ludwigConfirmationText;
        [SerializeField] private GameObject _maestroConfirmationText;
        [SerializeField] private string _sceneType;
        
        [SerializeField] private Canvas bookCanvas;
        [SerializeField] private GameObject weaponConfirmationPanel;
    
        [Header("In game menu")]
        [SerializeField] private GameObject _ingameUI;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private GameObject _winPanel;

        [Header("Pause game menu")]
        [SerializeField] private GameObject _pauseGameMenu;
        
        [Header("Instruction Panel")]
        [SerializeField] private GameObject _instructionPanel;
    
        // Start is called before the first frame update
        void Start()
        {
            // if current scene is not main menu, hide main menu
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == SceneBuildIndex.MAINMENU)
            {
                _mainMenu.SetActive(true);
                _optionMenu.SetActive(false);
                _audioSettings.SetActive(false);
                _controlSettings.SetActive(false);
            } else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == SceneBuildIndex.LOBBY)
            {
                _overlay.SetActive(false);
            
                _startGamePanel.SetActive(false);
                _amadeusConfirmationText.SetActive(false);
                _ludwigConfirmationText.SetActive(false);
                _maestroConfirmationText.SetActive(false);
                
                bookCanvas.gameObject.SetActive(false);
                weaponConfirmationPanel.SetActive(false);
            }
            else
            {
                _overlay.SetActive(false);
                
                _ingameUI.SetActive(true);
                _losePanel.SetActive(false);
                _pauseGameMenu.SetActive(false);
                
                if (_instructionPanel != null)
                {
                    GameManager.Instance.IsGamePaused = true;
                    ShowInstruction();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        #region Main Menu
        
        public void ShowMainMenu()
        {
            _mainMenu.SetActive(true);
        }
        
        public void HideMainMenu()
        {
            _mainMenu.SetActive(false);
        }
        
        public void ShowOptionMenu()
        {
            HideMainMenu();
            _optionMenu.SetActive(true);
        }
        
        public void HideOptionMenu()
        {
            _optionMenu.SetActive(false);
        }

        public void OnMainMenu_StartGame()
        {
            // create a new game save in player prefs
            SceneManager.Instance.LoadScene(SceneType.LOBBY);
        }
        
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
        
        #endregion

        #region Option Menu
        
        public void ShowAudioSettings()
        {
            HideOptionMenu();
            _audioSettings.SetActive(true);
        }
        
        public void HideAudioSettings()
        {
            _audioSettings.SetActive(false);
        }
        
        public void ShowControlSettings()
        {
            HideOptionMenu();
            _controlSettings.SetActive(true);
        }
        
        public void HideControlSettings()
        {
            _controlSettings.SetActive(false);
        }
        
        public void OnMenu_Back()
        {
            if (_audioSettings.activeSelf)
            {
                HideAudioSettings();
                ShowOptionMenu();
            }
            else if (_controlSettings.activeSelf)
            {
                HideControlSettings();
                ShowOptionMenu();
            }
            else if (_optionMenu.activeSelf)
            {
                HideOptionMenu();
                ShowMainMenu();
            }
        }
        
        #endregion
        
        #region Lobby

        public void OnLoadScene(SceneTypeEnum sceneType)
        {
            _sceneType = SceneType.GetScene(sceneType);
            
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == SceneBuildIndex.LOBBY)
            {
                Time.timeScale = 0;
                _overlay.SetActive(true);
                _startGamePanel.SetActive(true);
                switch (sceneType)
                {
                    case SceneTypeEnum.AMADEUS_TROOP:
                        _amadeusConfirmationText.SetActive(true);
                        break;
                    case SceneTypeEnum.LUDWIG_TROOP:
                        _ludwigConfirmationText.SetActive(true);
                        break;
                    case SceneTypeEnum.MAESTRO_TROOP:
                        _maestroConfirmationText.SetActive(true);
                        break;
                }
            }
            else
            {
                SceneManager.Instance.LoadScene(_sceneType);
            }
            
        }
        
        public void OnStartGameButtonClicked()
        {
            Time.timeScale = 1;
            _overlay.SetActive(false);
            _startGamePanel.SetActive(false);
            _amadeusConfirmationText.SetActive(false);
            _ludwigConfirmationText.SetActive(false);
            _maestroConfirmationText.SetActive(false);
            SceneManager.Instance.LoadScene(_sceneType);
        }
        
        public void OnBackButtonClicked()
        {
            _overlay.SetActive(false);
            _startGamePanel.SetActive(false);
            _amadeusConfirmationText.SetActive(false);
            _ludwigConfirmationText.SetActive(false);
            _maestroConfirmationText.SetActive(false);
        }

        #endregion

        #region Ingame Menu
        
        public void ShowIngameMenu()
        {
            _ingameUI.SetActive(true);
        }
    
        public void HideIngameMenu()
        {
            _ingameUI.SetActive(false);
        }
        
        #region Lose Panel
        public void ShowLosePanel()
        {
            _overlay.SetActive(true);
            _losePanel.SetActive(true);
        }
    
        public void HideLosePanel()
        {
            _overlay.SetActive(false);
            _losePanel.SetActive(false);
        }
        
        public void OnRestartButtonClicked()
        {
            HideLosePanel();
            GameManager.Instance.RestartGame();
        }
        
        public void OnMainMenuButtonClicked()
        {
            HideLosePanel();
            GameManager.Instance.LoadMainMenu();
        }
        #endregion
        
        #region Win Panel
        public void ShowWinPanel()
        {
            _overlay.SetActive(true);
            _winPanel.SetActive(true);
        }
        
        public void HideWinPanel()
        {
            _overlay.SetActive(false);
            _winPanel.SetActive(false);
        }
        
        public void OnWin_Continue()
        {
            HideWinPanel();
            OnLoadScene(SceneTypeEnum.LOBBY);
        }
        #endregion
    
        #region Pause Game Menu
        public void ShowPauseGameMenu()
        {
            _overlay.SetActive(true);
            _pauseGameMenu.SetActive(true);
        }
    
        public void HidePauseGameMenu()
        {
            GameManager.Instance.IsGamePaused = false;
            _overlay.SetActive(false);
            _pauseGameMenu.SetActive(false);
        }
        
        public void OnPause_Continue()
        {
            HidePauseGameMenu();
        }
        
        public void OnPause_Restart()
        {
            HidePauseGameMenu();
            GameManager.Instance.RestartGame();
        }
        
        public void OnPauseLobby_Quit()
        {
            HidePauseGameMenu();
            GameManager.Instance.LoadMainMenu();
        }
        
        public void OnPause_Quit()
        {
            HidePauseGameMenu();
            SceneManager.Instance.LoadScene(SceneType.LOBBY);
        }
        
        #endregion
        
        
        #region Instruction
        public void ShowInstruction()
        {
            _overlay.SetActive(true);
            _instructionPanel.SetActive(true);
        }
    
        public void HideInstruction()
        {
            GameManager.Instance.IsGamePaused = false;
            _overlay.SetActive(false);
            _instructionPanel.SetActive(false);
        }
        
        public void OnInstruction_Continue()
        {
            HideInstruction();
        }
        
        #endregion

        #endregion

        public void ShowWeaponPanel()
        {
            bookCanvas.gameObject.SetActive(true);
            weaponConfirmationPanel.SetActive(true);
        }

        public void HideWeaponPanel()
        {
            bookCanvas.gameObject.SetActive(false);
            weaponConfirmationPanel.SetActive(false);
        }

        public void OnWeaponPanel_Back()
        {
            HideWeaponPanel();
            // BookController.Instance.CloseBook();
        }
    }
}
