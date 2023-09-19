using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Transform fireExtinguisher;

    public bool hasFireExtinguisher = false;

    void Start()
    {
        PhysicsPickup.onPickupFireExtinguisher += GatheredFireExtinguisher;
    }

    void OnDestroy()
    {
        PhysicsPickup.onPickupFireExtinguisher -= GatheredFireExtinguisher;

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
        fireExtinguisher.gameObject.SetActive(!fireExtinguisher.gameObject.activeSelf);
    }

    public bool GetFireExtinguisherHoldState()
    {
        return hasFireExtinguisher;
    }
}
