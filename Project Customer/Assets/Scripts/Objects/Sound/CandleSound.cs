using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSound : MonoBehaviour
{
    AudioSource audioSource;
    bool active;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OnClickItems.onCandleClicked += PlaySound;
    }

    void OnDestroy()
    {
        OnClickItems.onCandleClicked -= PlaySound;
    }

    private void PlaySound()
    {
        if (!active) audioSource.Play();

        active = !active;
    }
}
