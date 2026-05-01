using System;
using UnityEngine;

namespace finished3
{
    /// <summary>
    /// Đóng vai trò là Game Manager cốt lõi.
    /// Quản lý hệ thống Chuyển Tầng, tăng độ khó và lưu trữ các biến toàn cục của màn chơi.
    /// </summary>
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance { get; private set; }

        [Header("Trạng Thái Hầm Ngục")]
        public int currentFloor = 1;

        // Kênh sự kiện báo cho UI biết đã sang tầng mới
        public event Action<int> OnFloorChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            // Cập nhật UI lần đầu
            OnFloorChanged?.Invoke(currentFloor);
        }

        /// <summary>
        /// Gọi hàm này khi người chơi xác nhận bấm xuống cầu thang
        /// </summary>
        public void NextFloor()
        {
            currentFloor++;
            GameLogger.Log($"=== BƯỚC XUỐNG TẦNG {currentFloor} ===");

            // Thưởng EXP khi qua tầng
            if (PlayerController.Instance != null && PlayerController.Instance.character != null)
            {
                var stats = PlayerController.Instance.character.GetComponent<CharacterStats>();
                if (stats != null) stats.AddExp(50);
            }

            // 1. Tăng độ khó (Mỗi tầng thêm 3 quái, 1 rương)
            if (MapManager.Instance != null)
            {
                MapManager.Instance.monsterCount += 3;
                MapManager.Instance.chestCount += 1;
                // Cầu thang vẫn giữ nguyên 1 cái
            }

            // 2. Dọn dẹp bản đồ cũ: Xóa toàn bộ quái vật và rương đang có trên sân
            CleanupOldFloor();

            // 3. Tái tạo bản đồ mới xung quanh vị trí nhân vật hiện tại
            if (PlayerController.Instance != null && PlayerController.Instance.character != null)
            {
                OverlayTile startTile = PlayerController.Instance.character.standingOnTile;
                
                // Mẹo: Đưa nhân vật ra vị trí chính xác của Cầu Thang (hoặc giữ nguyên nếu đã ở đó)
                // Sau đó bắt đầu rải mìn mới
                if (MapManager.Instance != null && startTile != null)
                {
                    MapManager.Instance.GenerateDungeon(startTile.grid2DLocation);
                    
                    // Lật ô đầu tiên cho an toàn
                    startTile.RevealTile();
                    
                    // Force cập nhật UI HUD 
                    // (Sử dụng dummy call qua PlayerController hoặc phát Event thủ công nếu cần)
                    // Vì onJumpComplete không chạy, ta cần gọi update HUD
                }
            }

            // 4. Báo cho UI Cập nhật số tầng
            OnFloorChanged?.Invoke(currentFloor);
        }

        /// <summary>
        /// Hủy toàn bộ Quái Vật và Rương của tầng cũ để nhường chỗ cho tầng mới.
        /// </summary>
        private void CleanupOldFloor()
        {
            // Trong Prototype, Quái và Rương có thể đang là GameObject nằm rải rác.
            // Ta có thể tìm và xóa (Hoặc trả về Object Pool)
            
            // Xóa Quái vật (CharacterInfo mà không phải Player)
            CharacterInfo[] allCharacters = FindObjectsByType<CharacterInfo>(FindObjectsSortMode.None);
            foreach (var character in allCharacters)
            {
                if (PlayerController.Instance == null || character != PlayerController.Instance.character)
                {
                    Destroy(character.gameObject);
                }
            }

            // Xóa Rương
            Chest[] allChests = FindObjectsByType<Chest>(FindObjectsSortMode.None);
            foreach (var chest in allChests)
            {
                Destroy(chest.gameObject);
            }

            // Xóa sạch bộ nhớ unitOnTile trên các Grid
            if (MapManager.Instance != null && MapManager.Instance.map != null)
            {
                foreach (var kvp in MapManager.Instance.map)
                {
                    if (PlayerController.Instance != null && kvp.Value == PlayerController.Instance.character.standingOnTile)
                    {
                        // Giữ lại player
                    }
                    else
                    {
                        kvp.Value.unitOnTile = null;
                        kvp.Value.chestOnTile = null;
                    }

                    // Ẩn Cầu Thang Cũ: Thay thế hình ảnh cầu thang bằng sương mù
                    if (kvp.Value.isRevealed)
                    {
                        // Reset hình ảnh về ô sương mù bình thường
                        kvp.Value.SetFogColor();
                    }
                }
            }
        }
    }
}
