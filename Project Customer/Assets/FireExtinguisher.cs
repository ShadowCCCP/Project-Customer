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
    float cooldown = 100;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time - lastShot > cooldown)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        currentFoam = Instantiate(foamPrefab, spawnPoint.position, spawnPoint.rotation);
        currentFoam.transform.localPosition = Vector3.zero;
        currentFoam.transform.SetParent(null);
        //currentFoam.GetComponent<Rigidbody>().AddForce(transform.forward * shootPower);
    }
}
