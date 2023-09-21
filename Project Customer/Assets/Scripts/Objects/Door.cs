using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Transform doorSide1;

    [SerializeField]
    Transform doorSide2;

    [SerializeField]
    AudioClip[] doorOpenSound;

    [SerializeField]
    AudioClip[] doorCloseSound;

    bool doorOpen;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(doorSide1.gameObject.tag == "BedDoor")   OnClickItems.onBedDoorClicked += PlaySound;
        else    OnClickItems.onBathDoorClicked += PlaySound;
    }

    void OnDestroy()
    {
        if (doorSide1.gameObject.tag == "BedDoor") OnClickItems.onBedDoorClicked -= PlaySound;
        else OnClickItems.onBathDoorClicked -= PlaySound;
    }

    private void PlaySound()
    {
        int randomNumber = UnityEngine.Random.Range(0, 2);

        if (doorOpen)   audioSource.clip = doorCloseSound[randomNumber];
        else            audioSource.clip = doorOpenSound[randomNumber];

        audioSource.Play();
        doorOpen = !doorOpen;
    }
}
