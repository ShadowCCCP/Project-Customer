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
    UIManager uiManager;

    bool playCutscene;
    bool playedCutscene;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        rotateCamera = FindObjectOfType<RotateCamera>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        cutScene = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (playCutscene)
        {
            uiManager.ToggleHealthOxygenCrosshair();
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
            uiManager.ToggleHealthOxygenCrosshair();
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
