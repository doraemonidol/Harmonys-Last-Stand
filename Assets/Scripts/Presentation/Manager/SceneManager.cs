using Common;
using Runtime;

namespace Presentation.GUI
{
    public class SceneManager : MonoBehaviorInstance<SceneManager>
    {
        public void LoadScene(SceneTypeEnum sceneType)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneType.GetScene(sceneType));
        }
    }
}