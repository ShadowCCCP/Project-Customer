using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathFade : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Life.onDeath += TriggerFadeSameLevel;
    }

    private void OnDestroy()
    {
        Life.onDeath -= TriggerFadeSameLevel;
    }

    // Method triggered by event
    private void TriggerFadeSameLevel()
    {
        anim.SetTrigger("FadeSameLevel");
    }
}