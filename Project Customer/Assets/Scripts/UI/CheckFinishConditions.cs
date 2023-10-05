using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CheckFinishConditions : MonoBehaviour
{
    RotateCamera rotateCameraScript;
    PlayerMovement playerMovementScript;
    PhysicsPickup physicsPickupScript;
    PauseMenuScript pauseMenuScript;

    [SerializeField]
    GameObject GameUI;

    [SerializeField]
    GameObject FinishedMenuUI;

    [SerializeField]
    bool ableToFinishGame = false;

    float finishTimer = 10;
    float finishTimeSet;

    void Start()
    {
        rotateCameraScript = FindObjectOfType<RotateCamera>(); 
        physicsPickupScript = FindObjectOfType<PhysicsPickup>();
        playerMovementScript =FindObjectOfType<PlayerMovement>();
        pauseMenuScript = FindObjectOfType<PauseMenuScript>();

        CutsceneManager.allCutscenesPlayed += SetAbilityToFinishGameTrue;
    }

    void Update()
    {
        if (Fire.flameCount <= 0 && ableToFinishGame && Time.time - finishTimeSet >= finishTimer)
        {
            OnGameFinished();
        }
    }

    void OnDestroy()
    {
        CutsceneManager.allCutscenesPlayed -= SetAbilityToFinishGameTrue;
    }

    public void SetAbilityToFinishGameTrue()
    {
        ableToFinishGame = true;
        finishTimeSet = Time.time;
    }
    void OnGameFinished()
    {
        GameUI.SetActive(false);
        FinishedMenuUI.SetActive(true);
        rotateCameraScript.UnlockCursor();
     
        rotateCameraScript.enabled = false;
        playerMovementScript.enabled = false;
        physicsPickupScript.enabled = false;
        pauseMenuScript.enabled = false;

    }
}
