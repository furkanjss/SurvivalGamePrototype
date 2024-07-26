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

    void Initialize()
    {
        icon = GetComponent<Image>();
        button = GetComponent<Button>();
        itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        button.onClick.AddListener(ChoiceThis);
    }
    public void SetSlot(ItemSlot slot)
    {
        if (icon==null)
        {
            Initialize();
        }

        Item = slot.item;
        icon.sprite = Item.icon;
        itemName.text = Item.itemName;
        amountText.text = slot.amount.ToString();
    }

    void ChoiceThis()
    {
        if (Item.itemType == BaseItem.ItemType.Tool)
        {
            icon.color=Color.green;
        }
        InventoryManager.Instance.UseSelectedItem(Item);
    }
    
}