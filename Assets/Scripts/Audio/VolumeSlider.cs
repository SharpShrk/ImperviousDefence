using UnityEngine;
using UnityEngine.UI;

public enum VolumeType
{
    Music,
    SFX
}

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private VolumeType _volumeType;

    private void Start()
    {
        switch (_volumeType)
        {
            case VolumeType.Music:
                _slider.value = AudioManager.Instance.GetMusicVolume();
                break;
            case VolumeType.SFX:
                _slider.value = AudioManager.Instance.GetSFXVolume();
                break;
        }

        _slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    private void OnValueChanged()
    {
        switch (_volumeType)
        {
            case VolumeType.Music:
                AudioManager.Instance.ChangeMusicVolume(_slider.value);
                break;
            case VolumeType.SFX:
                AudioManager.Instance.ChangeSFXVolume(_slider.value);
                break;
        }
    }
}