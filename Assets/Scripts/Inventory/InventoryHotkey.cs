using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHotkey : InventoryUI
{
    public override void UpdateUI(List<ItemSlot> slots)
    {
        base.UpdateUI(slots);
        SetHotkeyFeaturesToSlots(slots);
    }

     void SetHotkeyFeaturesToSlots(List<ItemSlot> slots)
    {
       
        foreach (InventorySlotUI slot in slotsUI)
        {
            slot.IsHotkeySlot();
        }
    }
}
