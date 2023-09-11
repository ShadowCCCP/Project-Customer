using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeathMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject deathMenu;
    [SerializeField]
    private GameObject gameUI;
    // Start is called before the first frame update
    void Start()
    {
       // Life.onDeath += death;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnDestroy()
    {
       // Life.onDeath -= death;
    }
    void death()
    {
        //strart timer
        Thread.Sleep(2000);
        gameUI.SetActive(false);
        deathMenu.SetActive(true);
    }
}
