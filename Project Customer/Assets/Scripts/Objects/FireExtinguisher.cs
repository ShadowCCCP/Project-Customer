using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
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
    float cooldown = 0.25f;

    void Start()
    {
        lastShot = Time.time;
        Life.onDeath += DeactivateSelf;
    }

    void OnDestroy()
    {
        Life.onDeath -= DeactivateSelf;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - lastShot > cooldown)
        {
            Shoot();
            lastShot = Time.time;
        }

        if(Input.GetMouseButtonUp(0))
        {
            foamParticle.Stop();
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
}
