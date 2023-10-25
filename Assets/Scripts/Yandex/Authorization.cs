using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class Authorization : MonoBehaviour
{
    [SerializeField] private Button _autorizeButton;

    private void Start()
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

    public void TryAuthorize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.Authorize(OnAuthorizeSuccess, OnAuthorizeError);
#endif
    }

    private void HideButton()
    {
        _autorizeButton.gameObject.SetActive(false);
    }

    private void OnAuthorizeSuccess()
    {
        HideButton();
    }

    private void OnAuthorizeError(string error)
    {
        Debug.Log(error);
    }
}
