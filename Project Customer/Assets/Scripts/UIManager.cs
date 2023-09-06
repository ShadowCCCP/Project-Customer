using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI OxygenLeftText;

    private PhysicsPickup player;

    public float Oxygen = 100;
    public float OxygenRundownSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PhysicsPickup>();
        ItemName.text = player.objectName;

        OxygenLeftText.text = "Oxygen: " + Oxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemName.text != player.objectName)
        {
            ItemName.text = player.objectName;
        }
        oxygenRundown();
    }
    void oxygenRundown()
    {
        Oxygen -= Time.deltaTime * OxygenRundownSpeed;
        OxygenLeftText.text = "Oxygen: " + (int)Oxygen;
        if (Oxygen < 0)
        {
            //death
        }
    }
}
