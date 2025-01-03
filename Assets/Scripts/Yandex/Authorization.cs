using UnityEngine;
using UnityEngine.UI;

namespace Yandex
{
    public class Authorization : MonoBehaviour
    {
        [SerializeField] private Button _autorizeButton;
        [SerializeField] private GameObject _warningPanel;
        [SerializeField] private GameObject _startPanel;

        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            HideButton();
        }
#endif
            _autorizeButton.onClick.AddListener(OnOpenWarningPanel);
        }

        private void OnDisable()
        {
            _autorizeButton.onClick.RemoveListener(OnOpenWarningPanel);
        }

        public void TryAuthorize()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerAccount.Authorize(OnAuthorizeSuccess, OnAuthorizeError);
#endif
        }

        private void OnOpenWarningPanel()
        {
            _warningPanel.SetActive(true);
            _startPanel.SetActive(false);
        }

        private void HideButton()
        {
            _autorizeButton.gameObject.SetActive(false);
        }

        private void OnAuthorizeSuccess()
        {
            HideButton();
        }
    }
}