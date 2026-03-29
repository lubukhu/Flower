namespace finished3
{
    /// <summary>
    /// Bản thiết kế cho MỘT ô rương trong hành trang.
    /// Mang thẻ [Serializable] để Hiện hiển thị được số lượng ở ngoài thanh Inspector Unity.
    /// </summary>
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData item;
        public int amount;

        public InventorySlot(ItemData item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }

        public void RemoveAmount(int value)
        {
            amount -= value;
            if (amount < 0) amount = 0;
        }

        public bool IsEmpty => amount <= 0 || item == null;
    }
}
