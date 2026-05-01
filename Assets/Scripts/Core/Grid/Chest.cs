using UnityEngine;

namespace finished3
{
    public class Chest : MonoBehaviour
    {
        public Animator animator;
        public bool isOpen = false;

        [Header("Danh Sách Phần Thưởng (Loot Table)")]
        [Tooltip("Kéo thẻ bài vật phẩm (Bình Máu, Sách Phép) vào đây để rương ngẫu nhiên rớt ra.")]
        public System.Collections.Generic.List<ItemData> possibleLoot = new System.Collections.Generic.List<ItemData>();

        private void Awake()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }

        public void OpenChest()
        {
            if (isOpen) return;

            isOpen = true;
            GameLogger.Log("Rương đã được mở!");

            if (animator != null)
            {
                // Gọi trigger "Open" trong Animator
                animator.SetTrigger("Open");
            }
            else
            {
                GameLogger.LogWarning("Chest không có Animator!");
            }

            // Ở đây sau này có thể gọi thêm logic sinh ra vật phẩm, cộng tiền, v.v.
            GiveLootToPlayer();
        }

        private void GiveLootToPlayer()
        {
            if (possibleLoot == null || possibleLoot.Count == 0)
            {
                GameLogger.LogWarning("Rương này là rương trống (Chưa cài đặt possibleLoot)!");
                return;
            }

            // Quay xổ số ngẫu nhiên 1 vật phẩm trong danh sách
            int randomIndex = Random.Range(0, possibleLoot.Count);
            ItemData droppedItem = possibleLoot[randomIndex];

            if (droppedItem != null)
            {
                if (InventoryManager.Instance != null)
                {
                    bool success = InventoryManager.Instance.AddItem(droppedItem, 1);
                    if (success)
                    {
                        GameLogger.Log($"Tuyệt vời! Bạn nhận được 1 [{droppedItem.itemName}] (Đã tự động cất vào túi đồ)");
                    }
                }
                else
                {
                    GameLogger.LogError("FATAL: Không tìm thấy InventoryManager trong Scene! Hãy kéo nó vào.");
                }
            }
        }
    }
}
