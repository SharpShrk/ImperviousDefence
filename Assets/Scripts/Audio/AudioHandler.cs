using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Audio
{
    [RequireComponent(typeof(AudioPlayer))]
    public class AudioHandler : MonoBehaviour
    {
        private const string MusicVolumeGroup = "Music";
        private const string FxVolumeGroup = "FX_Sound";
        private const string NameSceneMenu = "Menu";
        private const string NameSceneGame = "Game";
        private const float DecibelBase = 10f;
        private const float ReferenceDecibels = 20f;
        private const float MinVolume = -80f;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioClip _menuClip;
        [SerializeField] private AudioClip _gameClip;

        private float _originalMusicVolume;
        private float _originalSFXVolume;

        private AudioPlayer _audioPlayer;

        private void Awake()
        {
            _audioPlayer = GetComponent<AudioPlayer>();
            OnSceneLoaded(SceneManager.GetActiveScene());
        }

        public void MuteAudio()
        {
            _audioMixer.GetFloat(MusicVolumeGroup, out _originalMusicVolume);
            _audioMixer.GetFloat(FxVolumeGroup, out _originalSFXVolume);

            _audioMixer.SetFloat(MusicVolumeGroup, MinVolume);
            _audioMixer.SetFloat(FxVolumeGroup, MinVolume);
        }

        public void UnmuteAudio()
        {
            _audioMixer.SetFloat(MusicVolumeGroup, _originalMusicVolume);
            _audioMixer.SetFloat(FxVolumeGroup, _originalSFXVolume);
        }

        public float GetVolume(string group)
        {
            _audioMixer.GetFloat(group, out float volume);
            return Mathf.Pow(DecibelBase, volume / ReferenceDecibels);
        }

        private void OnSceneLoaded(Scene scene)
        {
            if (scene.name == NameSceneMenu)
            {
                _audioPlayer.PlayClip(_menuClip);
            }
            else if (scene.name == NameSceneGame)
            {
                _audioPlayer.PlayClip(_gameClip);
            }
        }
    }
}