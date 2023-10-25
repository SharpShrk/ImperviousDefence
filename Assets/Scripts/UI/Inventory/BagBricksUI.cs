using TMPro;
using UnityEngine;

public class BagBricksUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _bagUIText;
    [SerializeField] private PlayerBricksBag _bag;

    private int _maxBrickCapacity;

    private void OnEnable()
    {
        _bag.OnBrickCountChanged += SetTextValue;
        _maxBrickCapacity = _bag.MaxBrickCapacity;
        SetTextValue(_bag.CurrentBrickCount);
    }

    private void OnDisable()
    {
        _bag.OnBrickCountChanged -= SetTextValue;
    }

    private void SetTextValue(int currentValue)
    {
        _bagUIText.text = (currentValue.ToString() + " / " + _maxBrickCapacity.ToString());
    }
}
