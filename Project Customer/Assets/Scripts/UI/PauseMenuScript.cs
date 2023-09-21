using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gamePaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameUI;

    private PhysicsPickup playerModel;
    private RotateCamera playerRotateCamera;
    void Start()
    {
        playerRotateCamera = FindObjectOfType<RotateCamera>();
        playerModel = FindObjectOfType<PhysicsPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerRotateCamera)
        {
            if (!gamePaused)
            {
                pause();
            }
            else
            {
                resume();
            }

        }
    }

    void pause()
    {
        playerRotateCamera.UnlockCursor();
        playerModel.gameObject.SetActive(false);
        gamePaused = true; 
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void resume()
    {
        playerRotateCamera.LockCursor();
        playerModel.gameObject.SetActive(true);
        gamePaused = false;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }


    public void LoadStart()
    {
        SceneManager.LoadScene("StartScreen");
    }

}
