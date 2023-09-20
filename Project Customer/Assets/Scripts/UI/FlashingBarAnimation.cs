using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingBarAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    bool doOnce;
    Slider slider;
    [SerializeField]
    float flashingValue=25;
    void Start()
    {
        anim = GetComponent<Animator>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
         if(slider.value <= flashingValue / 100 && !doOnce)
         {
             doOnce = true;
             PlayAnimation();
         }
         if(slider.value > flashingValue/100 && doOnce)
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
