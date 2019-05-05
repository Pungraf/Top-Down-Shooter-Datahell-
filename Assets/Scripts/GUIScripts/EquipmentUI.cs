using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform EquipmentParent;
    public GameObject EquipmentGO;
    private EquipmentManager equipmentManager;
    public Slot[] slots;
    
    public delegate void OnEquiped();

    
    
    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        EquipmentManager.instance.onEquipmentEquiped += UpdateUI;
        slots = EquipmentParent.GetComponentsInChildren<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            EquipmentGO.SetActive(!EquipmentGO.activeSelf);
        }
    }

    void UpdateUI()
    {
        foreach (Equipment equipment in equipmentManager.currentEquipment)
        {
            foreach (Slot slot in slots)
            {
                if (slot != null && equipment != null)
                {
                    if (slot.equipmentSlot == equipment.equipSlot)
                    {
                        slot.AddItem(equipment);
                    }
                }
            }
        }
    }
}
