using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransition : MonoBehaviour
{
    [SerializeField]
    float transitionDuration = 2; 
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TransitionToFullVolume()
    {
        StartCoroutine(FadeVolume(0, 1, transitionDuration));
    }

    public void TransitionToZeroVolume()
    {
        StartCoroutine(FadeVolume(1, 0, transitionDuration));
    }

    private IEnumerator FadeVolume(float startVolume, float targetVolume, float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            float t = currentTime / duration;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, t);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume reaches the target exactly
        audioSource.volume = targetVolume;
    }
}
