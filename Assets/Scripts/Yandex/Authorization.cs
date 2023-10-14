using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Authorization : MonoBehaviour
{
    [SerializeField] private Button _autorizeButton;

    private void OnEnable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            HideButton();
        }
#endif
        _autorizeButton.onClick.AddListener(TryAuthorize);
    }

    private void OnDisable()
    {
        _autorizeButton.onClick.RemoveListener(TryAuthorize);
    }

    private void HideButton()
    {
        _autorizeButton.gameObject.SetActive(false);
    }

    public void TryAuthorize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.Authorize(OnAuthorizeSuccess, OnAuthorizeError);
#endif
    }

    private void OnAuthorizeSuccess()
    {
        /*_playerData.AuthorizeBy(this);
        _menuManager.TryOpenLeaderboard();*/
    }

    private void OnAuthorizeError(string error)
    {
        Debug.Log(error);
    }
}
