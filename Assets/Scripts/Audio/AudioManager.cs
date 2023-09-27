using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource AudioSource;
    public AudioClip MenuClip;
    public AudioClip GameClip;


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
        }

        ChangeClip(MenuClip);
    }

    public void ChangeVolume(float volume)
    {
        AudioSource.volume = volume;
    }

    public void ChangeClip(AudioClip newClip)
    {
        AudioSource.clip = newClip;
        AudioSource.Play();
    }
}
