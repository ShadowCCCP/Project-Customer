using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField]
    Slider volumeSlider;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.SetFloat("volume", 100);
        }
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeVolume(PlayerPrefs.GetFloat("volume"));
        volumeSlider.value = PlayerPrefs.GetFloat("volume");

    }

    // Called when the slider value changes
    void Update()
    {
        audioManager.ChangeVolume(volumeSlider.value);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);


    }

}
