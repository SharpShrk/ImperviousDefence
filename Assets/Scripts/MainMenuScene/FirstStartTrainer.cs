using UnityEngine;

namespace MainMenuScene
{
    public class FirstStartTrainer : MonoBehaviour
    {
        private const string FirstStartKey = "FirstStart";

        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private GameObject _startPanel;

        private bool _isFirstStart;

        public bool IsFirstStart => _isFirstStart;

        private void Start()
        {
            _isFirstStart = true;
            _isFirstStart = PlayerPrefs.GetInt(FirstStartKey, 0) == 0;
        }

        public void FirstStartTutorial()
        {
            PlayerPrefs.SetInt(FirstStartKey, 1);
            PlayerPrefs.Save();

            _isFirstStart = false;

            _tutorialPanel.SetActive(true);
            _startPanel.SetActive(false);
        }
    }
}