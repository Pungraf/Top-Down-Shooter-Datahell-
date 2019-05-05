using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Armor")]
public class Armor : Equipment
{
    
    public SkinnedMeshRenderer mesh;
    
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.RenderArmor(this);
    }
    
}
