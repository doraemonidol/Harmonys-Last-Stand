using System;
using Runtime;

namespace Presentation.GUI
{
    public class GameManager : MonoBehaviorInstance<GameManager>
    {
        private void Start()
        {
        }

        public void RestartGame()
        {
            throw new System.NotImplementedException();
        }

        public void LoadMainMenu()
        {
            throw new System.NotImplementedException();
        }
        
        public void GameOver()
        {
            UIManager.Instance.ShowLosePanel();
        }
    }
}