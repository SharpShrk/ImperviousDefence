using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private const string MusicVolumeGroup = "Music";
    private const string FxVolumeGroup = "FX Sound";

    [SerializeField] private AudioMixer _audioMixer;

    public AudioSource AudioSource;
    public AudioClip MenuClip;
    public AudioClip GameClip;

    private float _previousMusicVolume;
    private float _previousSFXVolume;
    private bool _isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ChangeClip(MenuClip);
    }

    public void ChangeMusicVolume(float value)
    {
        if (value <= 0.0001f)
        {
            _audioMixer.SetFloat(MusicVolumeGroup, -80);
        }
        else
        {
            _audioMixer.SetFloat(MusicVolumeGroup, Mathf.Log10(value) * 20);
        }
    }

    public void ChangeSFXVolume(float value)
    {
        if (value <= 0.0001f)
        {
            _audioMixer.SetFloat(MusicVolumeGroup, -80);
        }
        else
        {
            _audioMixer.SetFloat(MusicVolumeGroup, Mathf.Log10(value) * 20);
        }
    }

    public float GetMusicVolume()
    {
        _audioMixer.GetFloat(MusicVolumeGroup, out float volume);
        return Mathf.Pow(10, volume / 20);
    }

    public float GetSFXVolume()
    {
        _audioMixer.GetFloat(FxVolumeGroup, out float volume);
        return Mathf.Pow(10, volume / 20);
    }

    public void ChangeClip(AudioClip newClip)
    {
        AudioSource.clip = newClip;
        AudioSource.Play();
    }

    public void MuteAllAndPause()
    {
        if (_isMuted) return;

        _previousMusicVolume = GetMusicVolume();
        _previousSFXVolume = GetSFXVolume();

        _audioMixer.SetFloat(MusicVolumeGroup, -80);
        _audioMixer.SetFloat(FxVolumeGroup, -80);

        _isMuted = true;
        Time.timeScale = 0;
    }

    public void RestorePreviousVolumeAndUnpause()
    {
        if (!_isMuted) return;

        ChangeMusicVolume(_previousMusicVolume);
        ChangeSFXVolume(_previousSFXVolume);

        _isMuted = false;
        Time.timeScale = 1;
    }
}