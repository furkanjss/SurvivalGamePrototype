using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
[System.Serializable]
public class InventorySlotUI : MonoBehaviour
{
    public BaseItem Item;
    private Image icon;
    private Button button;
    private TextMeshProUGUI itemName;
    private TextMeshProUGUI amountText;
    private ItemSlot holdItemSlot;
    void Initialize()
    {
        icon = GetComponent<Image>();
        button = GetComponent<Button>();
        itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
       

        button.onClick.AddListener(ChooseThisItem);
    }
    public void SetSlot(ItemSlot slot)
    {
        if (icon==null)
        {
            Initialize();
        }

        holdItemSlot = slot;
        Item = slot.item;
        icon.sprite = Item.icon;
        itemName.text = Item.itemName;
        amountText.text = slot.amount.ToString();
    }

    void ChooseThisItem()
    {
        if (Item.itemType == BaseItem.ItemType.Tool)
        {
            icon.color=Color.green;
        }
        InventoryManager.Instance.UseSelectedItem(Item,holdItemSlot);
    }

    public void IsHotkeySlot()
    {
        transform.localScale = new Vector3(2, 2, 2);
    }
    
}