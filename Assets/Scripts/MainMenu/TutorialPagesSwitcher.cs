using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        _buttonPrev.onClick.AddListener(Click_Prev);
        _buttonNext.onClick.AddListener(Click_Next);
    }

    private void OnDisable()
    {
        _buttonPrev.onClick.RemoveListener(Click_Prev);
        _buttonNext.onClick.RemoveListener(Click_Next);
    }

    private void Start()
    {
        foreach (Transform t in _panelTransform)
        {
            _panels.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        _panels[_page].SetActive(true);
        _isReady = true;

        CheckControl();
    }

    public void Click_Prev()
    {
        if (_page <= 0 || !_isReady) return;

        _panels[_page].SetActive(false);
        _panels[_page -= 1].SetActive(true);

        CheckControl();
    }

    public void Click_Next()
    {
        if (_page >= _panels.Count - 1) return;

        _panels[_page].SetActive(false);
        _panels[_page += 1].SetActive(true);

        CheckControl();
    }

    void SetArrowActive()
    {
        _buttonPrev.gameObject.SetActive(_page > 0);
        _buttonNext.gameObject.SetActive(_page < _panels.Count - 1);
    }

    private void CheckControl()
    {
        SetArrowActive();
    }
}
