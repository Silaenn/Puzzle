using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public Slider sfxSlider;
    public AudioClip dragSound;
    public AudioClip dropSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip clickSound;
    public AudioClip spawnSound;
    public AudioClip gameoverSound;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        if (sfxSlider != null)
        {
            sfxSlider.value = savedVolume;
            sfxSlider.onValueChanged.AddListener(SetVolume);
        }
        SetVolume(savedVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
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

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlaySpawnSound()
    {
        audioSource.PlayOneShot(spawnSound);
    }
    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameoverSound);
    }
}
