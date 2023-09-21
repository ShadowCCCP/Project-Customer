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

    bool playCutscene;
    bool playedCutscene;

    void Start()
    {
        rotateCamera = FindObjectOfType<RotateCamera>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        cutScene = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (playCutscene)
        {
            cutsceneCam.gameObject.SetActive(true);
            rotateCamera.transform.gameObject.SetActive(false);
            playerMovement.enabled = false;
            rotateCamera.enabled = false;
            cutScene.Play();
            playCutscene = false;
            playedCutscene = true;
        }

        if(!rotateCamera.enabled && cutScene.state == PlayState.Paused && playedCutscene)
        {
            cutsceneCam.gameObject.SetActive(false);
            rotateCamera.transform.gameObject.SetActive(true);
            playerMovement.enabled = true;
            rotateCamera.enabled = true;
            playedCutscene = false;
        }
    }

    public void PlayCutscene()
    {
        playCutscene = true;
    }
}
