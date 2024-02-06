using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;
using Yandex;

namespace LeaderBoard
{
    public class LeaderboardWarning : MonoBehaviour
    {
        [SerializeField] private Button _authorizeButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _rankButton;
        [SerializeField] private GameObject _warningPanel;
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _leaderboardPanel;
        [SerializeField] private Authorization _authorization;

        private void OnEnable()
        {
            _warningPanel.SetActive(false);
            _authorizeButton.onClick.AddListener(OnButtonAuthorizeClick);
            _cancelButton.onClick.AddListener(OnCloseWarningPanel);
            _rankButton.onClick.AddListener(OnRankButtonClick);
        }

        private void OnDisable()
        {
            _authorizeButton.onClick.RemoveListener(OnButtonAuthorizeClick);
            _cancelButton.onClick.RemoveListener(OnCloseWarningPanel);
            _rankButton.onClick.RemoveListener(OnRankButtonClick);
        }

        private void OpenWarningPanel()
        {
            _warningPanel.SetActive(true);
            _startPanel.SetActive(false);
        }

        private void OnCloseWarningPanel()
        {
            _warningPanel.SetActive(false);
            _startPanel.SetActive(true);
        }

        private void OnButtonAuthorizeClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            _authorizeButton.gameObject.SetActive(false);
        }
#endif

            _authorization.TryAuthorize();
        }

        private void OnRankButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
        {
            _leaderboardPanel.SetActive(true);
            _startPanel.SetActive(false);
        }
        else
        {
            OpenWarningPanel();
        }
#endif
        }
    }
}