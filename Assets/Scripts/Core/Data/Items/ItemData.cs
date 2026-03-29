using UnityEngine;

namespace finished3
{
    public enum ItemType
    {
        Consumable, // Đồ tiêu hao (Bình Máu, Giải độc)
        Equipment,  // Trang bị (Kiếm, Giáp)
        Material,   // Nguyên liệu chế tạo
        KeyItem     // Đồ nhiệm vụ không thể rớt
    }

    [CreateAssetMenu(fileName = "New Item", menuName = "Flower/Item/Basic Item", order = 1)]
    public class ItemData : ScriptableObject
    {
        [Header("Thông tin Cơ Bản (Basic Info)")]
        [Tooltip("Mã định danh duy nhất của vật phẩm (VD: potion_01) để quản lý Save/Load")]
        public string id;
        public string itemName;
        
        [TextArea(2, 4)]
        [Tooltip("Đoạn mô tả ngắn gọn lúc di chuột qua vật phẩm")]
        public string description;
        
        [Tooltip("Hình ảnh hiển thị trong UI Túi đồ")]
        public Sprite icon;

        [Header("Chỉ số Hành Trang (Inventory Stats)")]
        public ItemType itemType;
        
        [Tooltip("Số lượng tối đa có thể xếp chồng trong 1 vách ngăn (Slot). VD: Kiếm = 1, Bình Máu = 99")]
        public int maxStack = 99;
        
        [Tooltip("Bật nếu cho vứt đồ ra bãi cỏ, Tắt nếu là Cốt Truyện buộc phải giữ")]
        public bool isDroppable = true;
    }
}
