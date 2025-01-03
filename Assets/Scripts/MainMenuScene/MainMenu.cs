using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScene
{
    [RequireComponent(typeof(FirstStartTrainer))]

    public class MainMenu : MonoBehaviour
    {
        private const string _gameScene = "Game";

        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _optionPanel;
        [SerializeField] private Button _optionButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private SceneSwitcher _sceneSwitcher;

        private void OnEnable()
        {
            _optionPanel.SetActive(false);
            _startButton.onClick.AddListener(OnStartClick);
            _optionButton.onClick.AddListener(OnOptionClick);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartClick);
            _optionButton.onClick.RemoveListener(OnOptionClick);
        }

        private void OnStartClick()
        {
            FirstStartTrainer firstStartTrainer = GetComponent<FirstStartTrainer>();

            if (firstStartTrainer.IsFirstStart)
            {
                firstStartTrainer.FirstStartTutorial();
            }
            else
            {
                _sceneSwitcher.SwitchScene(_gameScene);
            }
        }

        private void OnOptionClick()
        {
            _startPanel.SetActive(false);
            _optionPanel.SetActive(true);
        }
    }
}