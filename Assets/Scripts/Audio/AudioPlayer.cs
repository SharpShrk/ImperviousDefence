using UnityEngine;

namespace Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _menuClip;
        [SerializeField] private AudioClip _gameClip;

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

