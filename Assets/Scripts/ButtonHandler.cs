using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField]
    private int sceneName; 

    
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ChangeSkillInput()
    {
        Debug.Log("Change Skill Input");
    }

  
    
}
