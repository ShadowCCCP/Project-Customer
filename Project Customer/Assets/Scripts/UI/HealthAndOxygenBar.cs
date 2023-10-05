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
    Life player;

    float maxLife;
    float maxOxygen;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Life>();
        maxLife = player.GetLife();
        maxOxygen = player.GetOxygen();
    }

    // Update is called once per frame
    void Update()
    {
        SetSliderValues();
    }
    private void SetSliderValues()
    {
        OxygenBarFill.fillAmount = (float)player.GetOxygen() / maxOxygen;
        HealthBarFill.fillAmount = (float)player.GetLife() / maxLife;
    }
}
