using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Audio
{
    public class VolumeHandler : MonoBehaviour
    {
        private const string MusicVolumeGroup = "Music";
        private const string FxVolumeGroup = "FX_Sound";
        private const float DecibelBase = 10f;
        private const float ReferenceDecibels = 20f;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioClip _menuClip;
        [SerializeField] private AudioClip _gameClip;

        private float _originalMusicVolume;
        private float _originalSFXVolume;
        private float _minVolume = -80f;
        private bool _isPaused = false;

        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            _audioPlayer = gameObject.GetComponent<AudioPlayer>();

            OnSceneLoaded(SceneManager.GetActiveScene());
        }

        public void MuteAndPause()
        {           
            if (_isPaused) return;

            _audioMixer.GetFloat(MusicVolumeGroup, out _originalMusicVolume);
            _audioMixer.GetFloat(FxVolumeGroup, out _originalSFXVolume);

            _audioMixer.SetFloat(MusicVolumeGroup, _minVolume);
            _audioMixer.SetFloat(FxVolumeGroup, _minVolume);

            Time.timeScale = 0f;
            _isPaused = true;
        }

        public void UnmuteAndResume()
        {
            if (!_isPaused) return;

            _audioMixer.SetFloat(MusicVolumeGroup, _originalMusicVolume);
            _audioMixer.SetFloat(FxVolumeGroup, _originalSFXVolume);

            Time.timeScale = 1f;
            _isPaused = false;
        }

        public float GetVolume(string group)
        {
            _audioMixer.GetFloat(group, out float volume);
            return Mathf.Pow(DecibelBase, volume / ReferenceDecibels);   
        }

        private void OnSceneLoaded(Scene scene)
        {
            switch (scene.name)
            {
                case "Menu":
                    _audioPlayer.PlayClip(_menuClip);
                    break;

                case "Game":
                    _audioPlayer.PlayClip(_gameClip);
                    break;
            }
        }
    }
}