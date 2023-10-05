using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    [SerializeField]
    int lockNumber = 3;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int locksUnlocked = 0;
    public void lockUnlocked()
    {
        locksUnlocked++;
        if(locksUnlocked == lockNumber)
        {
            Unlocked();
        }
    }
    void Unlocked()
    {
        if (anim != null)
        {
           anim.SetTrigger("PlayAnimation");
        }
    }
}
