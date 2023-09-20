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
