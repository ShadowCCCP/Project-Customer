using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : MonoBehaviour
{
    [SerializeField]
    AudioClip[] fireSpiritDeath;
    AudioSource audioSource;

    [SerializeField]
    Fire[] connectedFires;
    int fireConnected;

    [SerializeField]
    float floatSpeed = 1;

    [SerializeField]
    float height = 0.2f;

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
                int randomNumber = Random.Range(0, fireSpiritDeath.Length);

                audioSource.clip = fireSpiritDeath[randomNumber];
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

    void FixedUpdate()
    {
        float bouncy = Mathf.Sin(Time.time * floatSpeed) * height * Time.deltaTime;
        transform.Translate(Vector3.up * bouncy);
    }
}
