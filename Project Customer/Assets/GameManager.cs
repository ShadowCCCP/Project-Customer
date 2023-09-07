using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager currentManager;
    Animator anim;

    public static GameManager GManager
    {
        get
        {
            return currentManager;
        }
    }

    private void Awake()
    {
        if (currentManager != null) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            currentManager = this;
        }
    }
    /*
    private void Start()
    {
        anim = GetComponent<Animator>();

        Timer.onTimesUp += TriggerFadeSameLevel;
        SpikeTrap.onPlayerSpiked += TriggerFadeSameLevel;
        FinishLine.onPlayerFinish += TriggerFadeNextLevel;
    }

    private void OnDestroy()
    {
        if (currentManager == this)
        {
            Timer.onTimesUp -= TriggerFadeSameLevel;
            SpikeTrap.onPlayerSpiked -= TriggerFadeSameLevel;
            FinishLine.onPlayerFinish -= TriggerFadeNextLevel;
            currentManager = null;
        }
    }
    */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    // Methods used when event is triggered
    private void TriggerFadeSameLevel()
    {
        anim.SetTrigger("FadeSameLevel");
    }

    public void TriggerFadeNextLevel()
    {
        anim.SetTrigger("FadeNextLevel");
    }

    // These methods are used in the fade animations
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        anim.SetTrigger("FadeSameLevel");
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
        anim.SetTrigger("FadeNextLevel");
    }

    // As I needed an integer parameter, I couldn't start the method through the animation...
    public void SpecificLevel(int buildIndex)
    {
        anim.SetTrigger("Fade");
        StartCoroutine(LoadSpecificLevel(buildIndex));
    }

    private IEnumerator LoadSpecificLevel(int buildIndex)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(buildIndex);
        anim.SetTrigger("Fade");
    }

    // For button in menu
    public void QuitGame()
    {
        Application.Quit();
    }
}