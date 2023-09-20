using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : MonoBehaviour
{
    [SerializeField]
    AudioClip fireSpiritDeath;
    AudioSource audioSource;

    [SerializeField]
    Fire[] connectedFires;
    int fireConnected;

    bool doOnce;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        fireConnected = connectedFires.Length;
    }

    void Update()
    {
        int fireCount = fireConnected;

        for (int i = 0; i < fireConnected; i++)
        {
            if (!connectedFires[i].enabled)
            {
                fireCount--;
            }
        }

        if(fireCount <= 0)
        {
            if(!doOnce)
            {
                audioSource.clip = fireSpiritDeath;
                audioSource.loop = false;
                audioSource.Play();
                doOnce = false;
            }

            if(!audioSource.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
