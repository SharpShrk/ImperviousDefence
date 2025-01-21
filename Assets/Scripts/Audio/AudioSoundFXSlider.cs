using System;

namespace Audio
{
    public class AudioSoundFXSlider : BaseAudioSlider
    {
        public event Action<float> SFXVolumeChanged;

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected void Start()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
            _slider.value = _volumeHandler.GetVolume(FxVolumeGroup);
        }

        protected override void OnValueChanged(float value)
        {
            SFXVolumeChanged?.Invoke(value);
        }
    }
}