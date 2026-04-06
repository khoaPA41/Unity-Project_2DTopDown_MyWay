using System;

[Serializable]
public class InventorySlot
{
    public ValueItem itemData;
    public int stack;
    public InventorySlot(ValueItem itemData, int stack)
    {
        this.itemData = itemData;
        this.stack = stack;
    }

    public InventorySlot()
    {
    }
}
