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
    [SerializeField]
    private GameObject playerModel;
    private RotateCamera playerRotateCamera;
    void Start()
    {
        playerRotateCamera = FindObjectOfType<RotateCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        playerModel.SetActive(false);
        gamePaused = true; 
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void resume()
    {
        playerRotateCamera.LockCursor();
        playerModel.SetActive(true);
        gamePaused = false;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("StartScreen");
    }



}
