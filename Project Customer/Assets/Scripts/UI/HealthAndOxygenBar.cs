using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndOxygenBar : MonoBehaviour
{
    [SerializeField]
    Image HealthBarFill;
    [SerializeField]
    Image OxygenBarFill;
    private Life player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSliderValues();
    }
    private void SetSliderValues()
    {
        OxygenBarFill.fillAmount = (float)player.GetOxygen() /100;
        HealthBarFill.fillAmount = (float)player.GetLife() / 100;
    }
}
