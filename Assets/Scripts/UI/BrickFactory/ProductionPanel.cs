using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductionPanel : MonoBehaviour
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private GameObject _panel;
    [SerializeField] private InfoMessagePanel _infoPanel; //вывести ошибку сюда, если деняк нехватает

    public event UnityAction OnButtonConfirmProductionClicked;

    private void OnEnable()
    {
        _confirmButton.onClick.AddListener(OnButtonConfirmClick);
        _cancelButton.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        _confirmButton.onClick.RemoveListener(OnButtonConfirmClick);
        _cancelButton.onClick.RemoveListener(ClosePanel);
    }

    private void ClosePanel()
    {
        Time.timeScale = 1;
        _panel.SetActive(false);
    }

    private void OnButtonConfirmClick()
    {
        OnButtonConfirmProductionClicked?.Invoke();
    }
}
