using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gamePaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject gameUI;
    private RotateCamera player;
    void Start()
    {
        player = FindObjectOfType<RotateCamera>();
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
        player.UnlockCursor();
        gamePaused = true; 
        pauseMenu.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0;
    }

    void resume()
    {
        player.LockCursor();
        gamePaused = false;
        pauseMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
    }

}
