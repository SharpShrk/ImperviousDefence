using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void PlayClip(AudioClip clip)
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}