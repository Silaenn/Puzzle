using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponent<Slider>();
        if (volumeSlider == null)
        {
            Debug.LogError("Slider component tidak ditemukan di GameObject lain");
            return;
        }

        if (BGMManager.instance != null)
        {
            BGMManager.instance.RegisterSlider(volumeSlider);
        }
        else
        {
            Debug.LogError("BGMManager.instance tidak ditemukan");
        }
    }
}
