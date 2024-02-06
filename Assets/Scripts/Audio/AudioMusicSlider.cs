using System;

namespace Audio
{
    public class AudioMusicSlider : BaseAudioSlider
    {
        public static event Action<float> OnMusicVolumeChanging = volume => { };


        private void Start()
        {
            VolumeHandler audioResources = FindObjectOfType<VolumeHandler>();
            _slider.value = audioResources.GetVolume(MusicVolumeGroup);
        }

        protected override void OnValueChanged(float value)
        {
            OnMusicVolumeChanging.Invoke(value);
        }
    }
}