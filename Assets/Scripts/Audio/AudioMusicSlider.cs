using System;

namespace Audio
{
    public class AudioMusicSlider : BaseAudioSlider
    {
        public event Action<float> MusicVolumeChanged;

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected void Start()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
            _slider.value = _volumeHandler.GetVolume(MusicVolumeGroup);
        }

        protected override void OnValueChanged(float value)
        {
            MusicVolumeChanged?.Invoke(value);
        }
    }
}