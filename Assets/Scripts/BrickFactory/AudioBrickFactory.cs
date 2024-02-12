using UnityEngine;

namespace BrickFactories
{
    public class AudioBrickFactory : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void PlayAudio()
        {
            _audioSource.Play();
        }

        public void StopAudio()
        {
            _audioSource.Stop();
        }
    }
}