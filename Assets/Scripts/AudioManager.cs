using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip dragSound;
    public AudioClip dropSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private void Awake()
    {
        instance = this;
    }

    public void PlayDragSound()
    {
        audioSource.PlayOneShot(dragSound);
    }

    public void PlayDropSound()
    {
        audioSource.PlayOneShot(dropSound);
    }

    public void PlayCorrectSound()
    {
        audioSource.PlayOneShot(correctSound);
    }

    public void PlayWrongSound()
    {
        audioSource.PlayOneShot(wrongSound);
    }
}
