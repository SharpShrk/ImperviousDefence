using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class VolumeChangeHandler : MonoBehaviour
    {
        private const string MusicVolumeGroup = "Music";
        private const string FxVolumeGroup = "FX_Sound";
        private const float ReferenceDecibels = 20f;
        private const float MinVolume = -80f;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMusicSlider _musicSlider;
        [SerializeField] private AudioSoundFXSlider _soundFXSlider;

        private void OnEnable()
        {
            _soundFXSlider.OnSFXVolumeChanged += ChangeSFXVolume;
            _musicSlider.OnMusicVolumeChanging += ChangeMusicVolume;
        }

        private void OnDisable()
        {
            _soundFXSlider.OnSFXVolumeChanged -= ChangeSFXVolume;
            _musicSlider.OnMusicVolumeChanging -= ChangeMusicVolume;
        }

        private void ChangeSFXVolume(float value)
        {
            SetVolume(FxVolumeGroup, value);
        }

        private void ChangeMusicVolume(float value)
        {
            SetVolume(MusicVolumeGroup, value);
        }

        private void SetVolume(string group, float value)
        {
            if (value <= 0.0001f)
            {
                _audioMixer.SetFloat(group, MinVolume);
            }
            else
            {
                _audioMixer.SetFloat(group, Mathf.Log10(value) * ReferenceDecibels);
            }
        }
    }
}
