using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneByOne : MonoBehaviour
{

    [SerializeField] GameObject solidFire;
    [SerializeField] GameObject electricalFire;
    [SerializeField] GameObject cookingFire;

    [SerializeField] float firstWait;
    [SerializeField] float secondWait;
    
    void Start()
    {
        StartCoroutine(SpiritSpawn());
    }

    IEnumerator SpiritSpawn()
    {
        solidFire.SetActive(true);

        yield return new WaitForSeconds(firstWait);

        electricalFire.SetActive(true);

        yield return new WaitForSeconds(secondWait);

        cookingFire.SetActive(true);
    }
}
