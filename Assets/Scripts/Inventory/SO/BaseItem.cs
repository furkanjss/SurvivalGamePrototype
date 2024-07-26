using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryManager ItemSlot", menuName = "InventoryManager/InventoryManager ItemSlot")]
public class BaseItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public ItemType itemType;
    public bool isStackable;
   
    public enum ItemType
    {
        Tool,
        Food,
        Resource,
        Other,Drink
    }
}