using System;

public class AudioMusicSlider : BaseAudioSlider
{
    public static event Action<float> OnMusicVolumeChanged = delegate { };

    private void Start()
    {
        AudioResources audioResources = FindObjectOfType<AudioResources>();
        _slider.value = audioResources.GetVolume(MusicVolumeGroup);
    }

    protected override void OnValueChanged(float value)
    {
        OnMusicVolumeChanged.Invoke(value);
    }
}
