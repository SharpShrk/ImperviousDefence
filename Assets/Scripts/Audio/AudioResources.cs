using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioResources : MonoBehaviour
{
    private const string MusicVolumeGroup = "Music";
    private const string FxVolumeGroup = "FX_Sound";

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioClip _menuClip;
    [SerializeField] private AudioClip _gameClip;

    private float _originalMusicVolume;
    private float _originalSFXVolume;
    private bool _isPaused = false;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        AudioSoundFXSlider.OnSFXVolumeChanged += ChangeSFXVolume;
        AudioMusicSlider.OnMusicVolumeChanged += ChangeMusicVolume;
    }

    private void OnDisable()
    {
        AudioSoundFXSlider.OnSFXVolumeChanged -= ChangeSFXVolume;
        AudioMusicSlider.OnMusicVolumeChanged -= ChangeMusicVolume;
    }

    public void MuteAndPause()
    {
        if (_isPaused) return;

        _audioMixer.GetFloat(MusicVolumeGroup, out _originalMusicVolume);
        _audioMixer.GetFloat(FxVolumeGroup, out _originalSFXVolume);

        _audioMixer.SetFloat(MusicVolumeGroup, -80);
        _audioMixer.SetFloat(FxVolumeGroup, -80);

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
        return Mathf.Pow(10, volume / 20);
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
            _audioMixer.SetFloat(group, -80);
        }
        else
        {
            _audioMixer.SetFloat(group, Mathf.Log10(value) * 20);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Menu":
                PlayClip(_menuClip);
                break;

            case "Game":
                PlayClip(_gameClip);
                break;
        }
    }

    private void PlayClip(AudioClip clip)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
