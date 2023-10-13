using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitialize : MonoBehaviour
{
    private const string _menuSceneName = "Menu";

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize(TransitionMenuScene);
    }

    private void TransitionMenuScene()
    {
        SceneManager.LoadScene(_menuSceneName);
    }
}