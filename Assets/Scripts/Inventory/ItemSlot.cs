using Unity.VisualScripting;

[System.Serializable]
public class ItemSlot
{
    public BaseItem item;
    public int amount; 
    public ItemSlot(BaseItem item, int _amount)
    {
        this.item = item;
        this.amount = _amount;
    }
}