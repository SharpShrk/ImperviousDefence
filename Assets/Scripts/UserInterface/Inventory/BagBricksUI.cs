using PlayerScripts;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class BagBricksUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bagUIText;
        [SerializeField] private PlayerBricksBag _bag;

        private int _maxBrickCapacity;

        private void OnEnable()
        {
            _bag.BrickCountChanging += OnSetTextValue;
            _maxBrickCapacity = _bag.MaxBrickCapacity;
            OnSetTextValue(_bag.CurrentBrickCount);
        }

        private void OnDisable()
        {
            _bag.BrickCountChanging -= OnSetTextValue;
        }

        private void OnSetTextValue(int currentValue)
        {
            string formattedText = currentValue.ToString() + " / " + _maxBrickCapacity.ToString();
            _bagUIText.text = formattedText;
        }
    }
}