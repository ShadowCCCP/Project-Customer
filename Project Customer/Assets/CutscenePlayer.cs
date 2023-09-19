using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField]
    Transform cutsceneCam;

    RotateCamera rotateCamera;
    PlayerMovement playerMovement;
    PlayableDirector cutScene;

    void Start()
    {
        rotateCamera = FindObjectOfType<RotateCamera>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        cutScene = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            cutsceneCam.gameObject.SetActive(true);
            rotateCamera.transform.gameObject.SetActive(false);
            playerMovement.enabled = false;
            rotateCamera.enabled = false;
            cutScene.Play();
        }

        if(!rotateCamera.enabled && cutScene.state == PlayState.Paused)
        {
            cutsceneCam.gameObject.SetActive(false);
            rotateCamera.transform.gameObject.SetActive(true);
            playerMovement.enabled = true;
            rotateCamera.enabled = true;
        }
    }
}
