using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
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
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - lastShot > cooldown)
        {
            Shoot();
            lastShot = Time.time;
        }
    }

    private void Shoot()
    {
        currentFoam = Instantiate(foamPrefab, spawnPoint.position, spawnPoint.rotation);
        currentFoam.transform.SetParent(null);
        currentFoam.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }
}