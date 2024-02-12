using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public abstract class BaseAudioSlider : MonoBehaviour
    {
        protected const string MusicVolumeGroup = "Music";
        protected const string FxVolumeGroup = "FX_Sound";

        [SerializeField] protected Slider _slider;
        [SerializeField] protected VolumeHandler _volumeHandler;

        protected abstract void OnValueChanged(float value);
    }
}