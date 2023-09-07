using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Gather things by listening to events

    [SerializeField]
    Transform fireExtinguisher;

    bool hasFireExtinguisher = true;

    void Start()
    {
        // Subscribe to events here...

    }

    void OnDestroy()
    {
        // Unsubscribe from events here...

    }

    void Update()
    {
        EquipUnequipItems();
    }

    private void EquipUnequipItems()
    {
        if(hasFireExtinguisher && Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireExtinguisher.gameObject.SetActive(!fireExtinguisher.gameObject.activeSelf);
        }
    }

    private void GatheredFireExtinguisher()
    {
        hasFireExtinguisher = true;
    }
}
