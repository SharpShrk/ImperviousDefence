using System;

public class AudioSoundFXSlider : BaseAudioSlider
{
    public static event Action<float> OnSFXVolumeChanged = volume => { };

    private void Start()
    {
        AudioResources audioResources = FindObjectOfType<AudioResources>();
        _slider.value = audioResources.GetVolume(FxVolumeGroup);
    }

    protected override void OnValueChanged(float value)
    {
        OnSFXVolumeChanged.Invoke(value);
    }
}
