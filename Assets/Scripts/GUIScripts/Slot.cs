using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    private Equipment item;
    public EquipmentSlot equipmentSlot;

    public void AddItem(Equipment newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnUnequipButton()
    {
        EquipmentManager.instance.Unequip((int)equipmentSlot);
        ClearSlot();
    }

}
