using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<ItemSlot> slots = new List<ItemSlot>();
    
    public static Action<BaseItem> OnPickUpItem;
    public static Action<List<ItemSlot>> OnInventoryChanged;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        OnPickUpItem += AddItem;
    }  private void OnDisable()
    {
        OnPickUpItem -= AddItem;
    }

    public void AddItem(BaseItem item)
    {
        ItemSlot slot = slots.Find(s => s.item == item);

        if (slot != null && item.isStackable)
        {
            slot.amount += 1;
        }
        else
        {
            slots.Add(new ItemSlot(item, 1));
        }
        OnInventoryChanged?.Invoke(slots);
    }

    public void RemoveItem(BaseItem item, int amount)
    {
        ItemSlot slot = slots.Find(s => s.item == item);

        if (slot != null)
        {
            slot.amount -= amount;
            if (slot.amount <= 0)
            {
                slots.Remove(slot);
            }
        }
        OnInventoryChanged?.Invoke(slots);
    }

  
    public void UseSelectedItem(BaseItem selectedItem)
    {
        if (selectedItem == null)
        {
            Debug.Log("No item selected.");
            return;
        }

        switch (selectedItem.itemType)
        {
            case BaseItem.ItemType.Tool:
                ToolItem tool= selectedItem as ToolItem;
                PlayerInventory.OnSetNewTool?.Invoke(tool.toolPrefab);
                break;
            case BaseItem.ItemType.Food:
         
                FoodItem food = selectedItem as FoodItem;
                if (food != null)
                {
                    EatFood(food);
                }
                else
                {
                    Debug.Log("Selected item is not a FoodItem.");
                }
                break;
         
        }
    }

    private void EatFood(FoodItem food)
    {
        SurvivalManager.OnFoodEaten?.Invoke(food.effect);
        RemoveItem(food, 1);
        OnInventoryChanged?.Invoke(slots);
    }
}
