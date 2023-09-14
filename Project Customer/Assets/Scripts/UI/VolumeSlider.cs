using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField]
    Slider volumeSlider;

    //AudioSource audioSource; // Reference to the AudioSource you want to control
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Called when the slider value changes
    void Update()
    {
        audioManager.ChangeVolume(volumeSlider.value);
    }

}
