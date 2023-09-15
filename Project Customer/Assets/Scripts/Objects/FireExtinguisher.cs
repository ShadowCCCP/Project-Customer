using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [SerializeField]
    AudioClip oneShot;

    [SerializeField]
    AudioClip loopSound;

    [SerializeField]
    ParticleSystem foamParticle;

    [SerializeField]
    FoamBullet foamPrefab;

    [SerializeField]
    Transform spawnPoint;

    [SerializeField]
    float shootPower;

    FoamBullet currentFoam;
    float lastShot;
    float cooldownFoamSpawn = 0.25f;

    [SerializeField]
    float cooldownSpam = 1;
    float lastPress;

    InventoryManager inventoryManager;
    AudioSource audioSource;
    SoundTransition sTrans;
    bool doOnce;
    bool startPlayingSounds;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sTrans = GetComponent<SoundTransition>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        lastShot = Time.time;
        Life.onDeath += DeactivateSelf;
    }

    void OnDestroy()
    {
        Life.onDeath -= DeactivateSelf;
    }

    void Update()
    {
        if(inventoryManager.hasFireExtinguisher && Time.time - lastPress > cooldownSpam)
        {
            PlaySoundEffects();

            if (Input.GetMouseButton(0) && Time.time - lastShot > cooldownFoamSpawn)
            {
                Shoot();
                lastShot = Time.time;
                startPlayingSounds = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                foamParticle.Stop();
                StopSoundEffects();
                lastPress = Time.time;
            }
        }
    }

    private void Shoot()
    {
        foamParticle.Play();
        currentFoam = Instantiate(foamPrefab, spawnPoint.position, spawnPoint.rotation);
        currentFoam.transform.SetParent(null);
        currentFoam.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }

    private void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

    private void PlaySoundEffects()
    {
        if(startPlayingSounds && !audioSource.isPlaying)
        {
            if (!doOnce)
            {
                audioSource.volume = 1;
                audioSource.clip = oneShot;
                audioSource.Play();
                doOnce = true;
            }

            if (!audioSource.isPlaying && doOnce)
            {
                audioSource.loop = true;
                audioSource.clip = loopSound;
                audioSource.Play();
            }
        }
    }

    private void StopSoundEffects()
    {
        audioSource.loop = false;
        audioSource.clip = oneShot;
        sTrans.TransitionToZeroVolume();
        audioSource.Play();
        doOnce = false;
        startPlayingSounds = false;
    }
}
