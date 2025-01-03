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
            _waves.OnWaveChanged += SetValue;
        }

        private void OnDisable()
        {
            _waves.OnWaveChanged -= SetValue;
        }

        private void SetValue(int value)
        {
            _waveText.text = value.ToString();
        }
    }
}