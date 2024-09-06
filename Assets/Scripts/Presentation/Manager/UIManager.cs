using Common;
using Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Presentation.GUI
{
    public class UIManager : MonoBehaviorInstance<UIManager>
    {
        [Header("Overlays")]
        [SerializeField] private GameObject _overlay;

        [Header("Lobby")]
        [SerializeField] private GameObject _startGamePanel;

        [SerializeField] private GameObject _amadeusConfirmationText;
        [SerializeField] private GameObject _ludwigConfirmationText;
        [SerializeField] private GameObject _maestroConfirmationText;
        [SerializeField] private SceneTypeEnum _sceneType;
    
        [Header("In game menu")]
        [SerializeField] private GameObject _ingameUI;

        [Header("Game lose menu")]
        [SerializeField] private GameObject _losePanel;

        [Header("Pause game menu")]
        [SerializeField] private GameObject _pauseGameMenu;
    
        // Start is called before the first frame update
        void Start()
        {
            _overlay.SetActive(false);
            
            _startGamePanel.SetActive(false);
            _amadeusConfirmationText.SetActive(false);
            _ludwigConfirmationText.SetActive(false);
            _maestroConfirmationText.SetActive(false);
            
            // _ingameMenu.SetActive(false);
            _losePanel.SetActive(false);
            _pauseGameMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        #region Lobby

        public void OnLoadScene(SceneTypeEnum sceneType)
        {
            _sceneType = sceneType;
            
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == SceneBuildIndex.LOBBY)
            {
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
                SceneManager.Instance.LoadScene(sceneType);
            }
            
        }
        
        public void OnStartGameButtonClicked()
        {
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
    
        public void ShowPauseGameMenu()
        {
            _overlay.SetActive(true);
            _pauseGameMenu.SetActive(true);
        }
    
        public void HidePauseGameMenu()
        {
            _overlay.SetActive(false);
            _pauseGameMenu.SetActive(false);
        }
    }
}
