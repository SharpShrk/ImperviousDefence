using System;
using UnityEngine;

namespace Audio
{
    public class AudioMusicSlider : BaseAudioSlider
    {
        public event Action<float> OnMusicVolumeChanging = volume => { };

        protected void Start()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
            _slider.value = _volumeHandler.GetVolume(MusicVolumeGroup);
        }

        protected override void OnValueChanged(float value)
        {
            OnMusicVolumeChanging.Invoke(value);
        }
    }
}