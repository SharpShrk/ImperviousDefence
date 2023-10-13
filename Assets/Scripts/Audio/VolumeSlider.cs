using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        //_slider.value = AudioManager.Instance.AudioSource.volume;
        //_slider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    private void OnValueChanged()
    {
        //AudioManager.Instance.ChangeVolume(_slider.value);
    }
}
