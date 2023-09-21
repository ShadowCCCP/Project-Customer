using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField]
    Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.SetFloat("volume", 1);
        }
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        
    }

    // Called when the slider value changes
    void Update()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        PlayerPrefs.SetFloat("volume", volumeSlider.value);


    }

}
