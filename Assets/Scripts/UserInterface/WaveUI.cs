using TMPro;
using UnityEngine;
using Wave;

namespace UserInterface
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _waveText;
        [SerializeField] private Waves _waves;

        private void OnEnable()
        {
            _waves.WaveChanged += OnSetValue;
        }

        private void OnDisable()
        {
            _waves.WaveChanged -= OnSetValue;
        }

        private void OnSetValue(int value)
        {
            _waveText.text = value.ToString();
        }
    }
}