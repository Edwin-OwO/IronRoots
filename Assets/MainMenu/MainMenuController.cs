using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private string gameSceneName;
        [SerializeField] private string optionsSceneName;
        
        public void PlayGame()
        {
            SceneManager.LoadScene(gameSceneName);
        }
        
        public void PlayOptions()
        {
            SceneManager.LoadScene(optionsSceneName);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}