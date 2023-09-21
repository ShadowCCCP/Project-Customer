using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingBarAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool doOnce;
    Image bar;
    [SerializeField]
    float flashingValue=25;
    void Start()
    {
        anim = GetComponent<Animator>();
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
         if(bar.fillAmount <= flashingValue / 100 && !doOnce)
         {
             doOnce = true;
             PlayAnimation();
         }
         if(bar.fillAmount > flashingValue/100 && doOnce)
         {
             doOnce= false;
             PlayAnimation();
         }
    }

    private void PlayAnimation()
    {
        if (anim != null)
        {
            anim.SetTrigger("PlayAnimation");
        }
    }

}
