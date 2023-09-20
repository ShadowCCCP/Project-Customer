using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    GameObject[] fires;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }   

    public void FiresOn()
    {
        foreach (GameObject fires in fires) 
        {
            fires.SetActive(true);
        }
    }
    public void FiresOff()
    {
        foreach (GameObject fires in fires)
        {
            fires.SetActive(false);
        }
    }
}
