using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    // How much it moves...
    [SerializeField, Range(0, 0.1f)]
    float amplitude = 0.015f;

    // How fast it moves...
    [SerializeField, Range(0, 30)]
    float frequency = 10;

    [SerializeField]
    Transform _camera;

    [SerializeField]
    Transform cameraHolder;
    MoveCamera moveCameraScript;

    AudioManager audioManager;

    string[] normalStepSounds = { "NormalStep1", "NormalStep2", "NormalStep3", "NormalStep4" };
    string[] crouchStepSounds = { "CrouchStep1", "CrouchStep2", "CrouchStep3", "CrouchStep4" };

    bool stepWasUp = true;
    float toggleSpeed = 2;
    Vector3 startPos;
    Rigidbody rb;
    PlayerMovement pMoveScript;

    void Start()
    {
        moveCameraScript = cameraHolder.GetComponent<MoveCamera>();
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
        pMoveScript = GetComponent<PlayerMovement>();
        startPos = _camera.localPosition;
    }

    void Update()
    {
        CheckMotion();
        ResetPosition();
    }

    private void CheckMotion()
    {
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        if (speed >= toggleSpeed && pMoveScript.IsGrounded())
        {
            PlayMotion(StepMotion());
        }
    }

    private void ResetPosition()
    {
        if(_camera.localPosition != startPos)
        {
            _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPos, frequency * Time.deltaTime);
        }
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
        PlayStepSound();
    }

    private Vector3 StepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        return pos;
    }

    private void PlayStepSound()
    {
        if (audioManager != null)
        {
            if (_camera.localPosition.y <= Mathf.Sin((-Mathf.PI / 2) / frequency) * amplitude && stepWasUp)
            {
                int randomNumber = Random.Range(0, 4);
                if (!moveCameraScript.IsCrouching())
                {
                    audioManager.Play(normalStepSounds[randomNumber]);
                }
                else audioManager.Play(crouchStepSounds[randomNumber]);
                stepWasUp = false;
            }

            if (_camera.localPosition.y >= 0)
            {
                stepWasUp = true;
            }
        }
    }
}
