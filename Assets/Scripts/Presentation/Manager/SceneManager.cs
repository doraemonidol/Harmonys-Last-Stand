using System;
using System.Collections.Generic;
using Common;
using Presentation.Manager;
using Runtime;
using UnityEngine;

namespace Presentation.GUI
{
    public class SceneManager : MonoBehaviorInstance<SceneManager>
    {
        [SerializeField] private List<SceneTrigger> _sceneTriggers;

        private void Start()
        {
        }

        public bool IsCurrentScene(SceneTypeEnum sceneType)
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == SceneType.GetScene(sceneType);
        }
        
        public void InitializeGame(PlayerData data)
        {
            if (IsCurrentScene(SceneTypeEnum.LOBBY))
            {
                foreach (var sceneTrigger in _sceneTriggers)
                {
                    sceneTrigger.Deactivate();
                }

                if (_sceneTriggers.Count == 3)
                {
                    if (data.UnlockedAmadeus)
                    {
                        _sceneTriggers[0].Activate();
                    }

                    if (data.UnlockedLudwig)
                    {
                        _sceneTriggers[1].Activate();
                    }

                    if (data.UnlockedMaestro)
                    {
                        _sceneTriggers[2].Activate();
                    }
                }
            }
        }
        
        public void LoadScene(string sceneType)
        {
            // UnityEngine.SceneManagement.SceneManager.LoadScene(sceneType);
            bl_SceneLoaderManager.LoadScene(sceneType);
        }
        
        public void ReloadCurrentScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void UnlockSceneTrigger()
        {
            foreach (var sceneTrigger in _sceneTriggers)
            {
                sceneTrigger.gameObject.SetActive(true);
            }
        }
    }
}