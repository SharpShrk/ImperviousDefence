using System;

namespace Audio
{
    public class AudioSoundFXSlider : BaseAudioSlider
    {
        public event Action<float> OnSFXVolumeChanged = volume => { };

        protected void Start()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
            _slider.value = _volumeHandler.GetVolume(FxVolumeGroup);
        }

        protected override void OnValueChanged(float value)
        {
            OnSFXVolumeChanged.Invoke(value);
        }
    }
}