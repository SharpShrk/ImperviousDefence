using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeListener : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            AudioManager.Instance.ChangeClip(AudioManager.Instance.MenuClip);
        }
        else if (scene.name == "Game")
        {
            AudioManager.Instance.ChangeClip(AudioManager.Instance.GameClip);
        }
    }
}
