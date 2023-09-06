using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI itemName;

    private PhysicsPickup player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PhysicsPickup>();
        itemName.text = player.GetItemName();
    }

    // Update is called once per frame
    void Update()
    {
        if(itemName.text != player.GetItemName())
        {
            itemName.text = player.GetItemName();
        }
    }
}
