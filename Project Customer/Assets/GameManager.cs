using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager currentManager;
    Animator anim;

    private void Awake()
    {
        if (currentManager != null) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            currentManager = this;
        }
    }
    private void Start()
    {
        anim = GetComponent<Animator>();

        Life.onDeath += TriggerFadeSameLevel;
    }

    private void OnDestroy()
    {
        if (currentManager == this)
        {
            Life.onDeath -= TriggerFadeSameLevel;
            currentManager = null;
        }
    }

    // Method triggered by event
    private void TriggerFadeSameLevel()
    {
        anim.SetTrigger("FadeSameLevel");
    }

    // Method used in the fade animation as event
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        anim.SetTrigger("FadeSameLevel");
    }

    // For button in menu
    public void QuitGame()
    {
        Application.Quit();
    }
}