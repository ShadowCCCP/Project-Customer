using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public static event Action onPlayerTeleport;
    static event Action onPlayTeleportSound;

    AudioSource audioSource;
    bool playingOneShot;

    [SerializeField]
    AudioClip oneShot;

    [SerializeField]
    AudioClip loop;

    [SerializeField]
    Transform placeToTeleport;

    PhysicsPickup playerModel;
    SoundTransition sTrans;

    float enterTime;
    float portalCooldown = 1;

    void Start()
    {
        sTrans = GetComponent<SoundTransition>();
        audioSource = GetComponent<AudioSource>();
        playerModel = FindObjectOfType<PhysicsPickup>();

        onPlayTeleportSound += PlayOneShotSound;
    }

    void OnDestroy()
    {
        onPlayTeleportSound -= PlayOneShotSound;
    }

    void Update()
    {
        if(playingOneShot && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.clip = loop;
            audioSource.Play();
            sTrans.TransitionToFullVolume();
            playingOneShot = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pickup"))
        {
            playerModel.DropObject();
            other.transform.position = placeToTeleport.transform.position;
            if (onPlayTeleportSound != null) onPlayTeleportSound();
            
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && Time.time - enterTime > portalCooldown)
        {
            playerModel.DropObject();
            playerModel.gameObject.transform.position = placeToTeleport.transform.position;

            enterTime = Time.time;

            if (onPlayTeleportSound != null) onPlayTeleportSound();
            if (onPlayerTeleport != null) onPlayerTeleport();
        }
    }

    private void PlayOneShotSound()
    {
        audioSource.loop = false;
        audioSource.clip = oneShot;
        audioSource.Play();
        playingOneShot = true;
    }
}
