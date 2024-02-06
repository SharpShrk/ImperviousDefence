using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuScene
{
    public class SceneSwitcher : MonoBehaviour
    {
        public void SwitchScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}