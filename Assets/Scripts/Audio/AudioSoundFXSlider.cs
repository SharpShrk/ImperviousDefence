using System;

namespace Audio
{
    public class AudioSoundFXSlider : BaseAudioSlider
    {
        public static event Action<float> OnSFXVolumeChanged = volume => { };

        private void Start()
        {
            VolumeHandler audioResources = FindObjectOfType<VolumeHandler>();
            _slider.value = audioResources.GetVolume(FxVolumeGroup);
        }

        protected override void OnValueChanged(float value)
        {
            OnSFXVolumeChanged.Invoke(value);
        }
    }
}