using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReady : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        StartCoroutine(CheckGameReady());
    }

    private IEnumerator CheckGameReady()
    {
        while (YandexGamesSdk.IsInitialized == false)
        {
            yield return null;
        }

        YandexGamesSdk.GameReady();
        gameObject.SetActive(false);
    }
}
