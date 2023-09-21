using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        rotateCameraScript = FindObjectOfType<RotateCamera>(); 
        physicsPickupScript = FindObjectOfType<PhysicsPickup>();
        playerMovementScript =FindObjectOfType<PlayerMovement>();
        pauseMenuScript = FindObjectOfType<PauseMenuScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Fire.flameCount <= 0 && ableToFinishGame)
        {
            OnGameFinished();
        }
    }

    public void SetAbilityToFinishGameTrue()
    {
        ableToFinishGame = true;
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
