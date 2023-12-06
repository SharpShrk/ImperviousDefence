using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlaceTurretView : MonoBehaviour
{
    [SerializeField] private Button _buttonUpgrade;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private int _costValue;

    public event UnityAction ButtonTurretBuyPressed;

    public int CostValue => _costValue;

    private void OnEnable()
    {
        _buttonUpgrade.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _buttonUpgrade.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ButtonTurretBuyPressed?.Invoke();
    }
}
