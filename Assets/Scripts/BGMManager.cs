using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource audioSource;
    public AudioClip bgmClip;
    public Slider volumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource.clip = bgmClip;
        audioSource.loop = true;

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("BGMVolume");
            audioSource.volume = savedVolume;
            if (volumeSlider != null) volumeSlider.value = savedVolume;
        }
        else
        {
            audioSource.volume = 1f;
        }

        audioSource.Play();

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void RegisterSlider(Slider newSlider)
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(SetVolume);
        }

        volumeSlider = newSlider;

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            volumeSlider.value = audioSource.volume;
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }
}
