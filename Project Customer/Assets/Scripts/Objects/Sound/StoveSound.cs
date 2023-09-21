using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    bool stoveActive;
    bool transitionSound;
    AudioSource audioSource;


    [SerializeField]
    AudioClip oneShot;
    [SerializeField]
    AudioClip loop;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OnClickItems.onStoveClicked += PlayStoveSound;
    }

    void OnDestroy()
    {
        OnClickItems.onStoveClicked -= PlayStoveSound;
    }

    void Update()
    {
        if(transitionSound && !audioSource.isPlaying)
        {
            audioSource.clip = loop;
            audioSource.loop = true;
            audioSource.Play();
            transitionSound = false;
        }
    }

    private void PlayStoveSound()
    {
        if(stoveActive)
        {
            audioSource.loop = false;
            transitionSound = false;
            audioSource.Stop();
            stoveActive = false;
        }
        else
        {
            audioSource.clip = oneShot;
            audioSource.Play();
            transitionSound = true;
            stoveActive = true;
        }
    }
}
