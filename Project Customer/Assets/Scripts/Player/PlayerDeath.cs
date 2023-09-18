using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    Transform cameraHolder;

    [SerializeField]
    Transform UI;

    Animator animCam;
    Animator animModel;

    RotateCamera rotateCameraScript;
    PlayerMovement playerMovementScript;
    PhysicsPickup physicsPickupScript;
    PauseMenuScript pauseMenuScript;
    

    [SerializeField]
    private GameObject deathMenu;
    [SerializeField]
    private GameObject gameUI;

    private bool startDeathTimer;
    private float deathScreenDelay = 1;

    void Start()
    {
        physicsPickupScript = FindObjectOfType<PhysicsPickup>();

        physicsPickupScript.gameObject.SetActive(true);
        animCam = cameraHolder.GetComponent<Animator>();
        animModel = physicsPickupScript.gameObject.GetComponent<Animator>();
        rotateCameraScript = FindObjectOfType<RotateCamera>();
        playerMovementScript = physicsPickupScript.gameObject.GetComponent<PlayerMovement>();
        pauseMenuScript = UI.GetComponent<PauseMenuScript>();
        Life.onDeath += Die;
    }
    private void Update()
    {
        timer();
    }

    void OnDestroy()
    {
        Life.onDeath -= Die;
    }

    private void Die()
    {
        rotateCameraScript.UnlockCursor();
        animCam.SetTrigger("die");
        animModel.SetTrigger("die");
        rotateCameraScript.enabled = false;
        playerMovementScript.enabled = false;
        physicsPickupScript.enabled = false;
        pauseMenuScript.enabled = false;

        startDeathTimer = true;


    }

    void timer()
    {
        if (startDeathTimer)
        {
            deathScreenDelay -= Time.deltaTime;
            if (deathScreenDelay < 0)
            {
                gameUI.SetActive(false);
                deathMenu.SetActive(true);
            }
        }
    }
}
