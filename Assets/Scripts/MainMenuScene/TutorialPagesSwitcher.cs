using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenuScene
{
    public class TutorialPagesSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _panels = new List<GameObject>();
        [SerializeField] private Transform _panelTransform;
        [SerializeField] private Button _buttonPrev;
        [SerializeField] private Button _buttonNext;

        private int _page = 0;
        private bool _isReady = false;

        private void OnEnable()
        {
            _buttonPrev.onClick.AddListener(OnClickPrevious);
            _buttonNext.onClick.AddListener(OnClickNext);
        }

        private void OnDisable()
        {
            _buttonPrev.onClick.RemoveListener(OnClickPrevious);
            _buttonNext.onClick.RemoveListener(OnClickNext);
        }

        private void Start()
        {
            foreach (Transform tutorialPanel in _panelTransform)
            {
                _panels.Add(tutorialPanel.gameObject);
                tutorialPanel.gameObject.SetActive(false);
            }

            _panels[_page].SetActive(true);
            _isReady = true;

            UpdateNavigationButtons();
        }

        public void OnClickPrevious()
        {
            if (_page <= 0 || !_isReady) return;

            _panels[_page].SetActive(false);
            _panels[_page -= 1].SetActive(true);

            UpdateNavigationButtons();
        }

        public void OnClickNext()
        {
            if (_page >= _panels.Count - 1) return;

            _panels[_page].SetActive(false);
            _panels[_page += 1].SetActive(true);

            UpdateNavigationButtons();
        }

        private void UpdateButtonVisibility()
        {
            _buttonPrev.gameObject.SetActive(_page > 0);
            _buttonNext.gameObject.SetActive(_page < _panels.Count - 1);
        }

        private void UpdateNavigationButtons()
        {
            UpdateButtonVisibility();
        }
    }
}