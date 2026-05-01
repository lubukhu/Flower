using System;
using System.Collections.Generic;
using UnityEngine;

namespace finished3
{
    /// <summary>
    /// PlayerController quản lý trạng thái, Spawn và Di Chuyển của Seeker.
    /// Chuẩn mô hình Single Responsibility.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        #region Events
        // Event bắn ra khi người chơi hoàn thành 1 bước đi (Truyền OverlayTile đích đến)
        // MainHUD sẽ đăng ký Event này để cập nhật UI góc dưới bên trái
        public static event Action<OverlayTile> OnPlayerStep;
        #endregion

        #region Inspector Settings
        [Header("Spawn Settings")]
        [Tooltip("Prefab nhân vật sẽ được sinh ra")]
        public GameObject characterPrefab;
        #endregion

        #region Core References
        public CharacterInfo character { get; private set; }
        private CharacterStats playerStats;
        private JumpMover jumpMover;
        private ClimbMover climbMover;
        #endregion

        #region Internal State
        private bool isMoving;
        public bool isLevelingUp = false; // Biến chặn người chơi khi bảng Level Up hiện lên
        public bool IsPlayerSpawned => character != null;

        private MovementController movementController;
        private MovementSystem movementSystem;
        private List<OverlayTile> currentPath = new List<OverlayTile>();
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            UnityEngine.Assertions.Assert.IsNotNull(characterPrefab, "FATAL ERROR: Cần gán characterPrefab!");

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            isMoving = false;
            movementController = new MovementController();
            movementSystem = new MovementSystem();
        }
        #endregion

        #region Public API (Được gọi từ MouseController)
        public void TapOnTile(OverlayTile tile)
        {
            if (isMoving || isLevelingUp) return; // Chặn input nếu đang di chuyển hoặc đang Lên cấp

            // Spawn nhân vật nếu chưa có
            if (!IsPlayerSpawned)
            {
                HandleSpawn(tile);
                return;
            }
            // Nếu có đường đi hợp lệ đến ô này (dù là ô đã lật hay chưa lật)
            if (currentPath != null && currentPath.Count > 0)
            {
                isMoving = true;
                MovePathStep();
                return;
            }

            // Fallback: Nếu không có đường (hoặc click linh tinh nhưng sát bên cạnh)
            HandleMovement(tile);
        }

        // ---- BACKWARD COMPATIBILITY CHO CODE CŨ ----
        public void CancelAction()
        {
            // Dummy method để các script cũ không báo lỗi.
        }
        // --------------------------------------------

        public void HoverTile(OverlayTile tile)
        {
            if (isMoving || !IsPlayerSpawned) return;

            // Xóa đường mũi tên cũ
            if (currentPath != null && currentPath.Count > 0)
            {
                foreach (var item in currentPath)
                {
                    item.SetSprite(ArrowTranslator.ArrowDirection.None);
                }
                currentPath.Clear();
            }

            // Nếu cắm cờ hoặc trỏ vào chính mình thì bỏ qua
            if (tile.isFlagged) return;
            if (tile == character.standingOnTile) return;

            // Lấy danh sách toàn bộ các ô an toàn (đã lật, không cờ) để làm bản đồ tìm đường
            List<OverlayTile> safeTiles = new List<OverlayTile>();
            foreach (var kvp in MapManager.Instance.map)
            {
                if (kvp.Value.isRevealed && !kvp.Value.isFlagged)
                {
                    safeTiles.Add(kvp.Value);
                }
            }

            // Nếu ô đang trỏ CHƯA LẬT, chúng ta vẫn cho phép thuật toán tìm đường tới nó
            // bằng cách thêm tạm thời nó vào danh sách an toàn
            if (!tile.isRevealed)
            {
                safeTiles.Add(tile);
            }

            // Tìm đường đi ngắn nhất
            currentPath = movementController.GetPath(character, tile, safeTiles);

            // Vẽ mũi tên dọc theo đường đi
            if (currentPath.Count > 0)
            {
                for (int i = 0; i < currentPath.Count; i++)
                {
                    var previousTile = i > 0 ? currentPath[i - 1] : character.standingOnTile;
                    var futureTile = i < currentPath.Count - 1 ? currentPath[i + 1] : null;

                    var arrow = new ArrowTranslator().TranslateDirection(previousTile, currentPath[i], futureTile);
                    currentPath[i].SetSprite(arrow);
                }
            }
        }
        #endregion

        #region Core Gameplay Logic
        private void HandleMovement(OverlayTile targetTile)
        {
            // Nếu đang đứng trên cầu thang và click lại chính nó -> Vẫn cho hiện bảng xác nhận
            if (targetTile == character.standingOnTile)
            {
                if (targetTile.isStair && targetTile.isRevealed)
                {
                    ShowStairPopup();
                }
                return;
            }

            // Kiểm tra xem có kề cạnh không (Chỉ cho phép đi 1 bước: Trên, dưới, trái, phải)
            // Không cho đi chéo để sát với thiết kế Dò mìn trên Isometric
            int xDiff = Mathf.Abs(targetTile.gridLocation.x - character.standingOnTile.gridLocation.x);
            int yDiff = Mathf.Abs(targetTile.gridLocation.y - character.standingOnTile.gridLocation.y);
            
            if (xDiff + yDiff > 1) 
            {
                GameLogger.Log("Chỉ được đi từng bước kề cạnh!");
                isMoving = false; // Đảm bảo mở khóa UI
                return;
            }

            // Kiểm tra bị cắm cờ không
            if (targetTile.isFlagged)
            {
                GameLogger.Log("Ô này đã bị cắm cờ, không thể bước lên!");
                isMoving = false; // Đảm bảo mở khóa UI
                return;
            }

            // Kiểm tra xem ô đích có rương không (đã lộ diện)
            if (targetTile.chestOnTile != null && !targetTile.chestOnTile.isOpen)
            {
                // Mở rương từ xa
                targetTile.chestOnTile.OpenChest();
                isMoving = false;
                return; // KHÔNG NHẢY VÀO Ô ĐÃ CÓ RƯƠNG
            }

            // Kiểm tra xem ô đích có phải Cầu Thang (đã lộ diện) không
            if (targetTile.isStair && targetTile.isRevealed)
            {
                ShowStairPopup();
                isMoving = false;
                return; // KHÔNG NHẢY VÀO CHỖ CẦU THANG KHI CLICK
            }

            // Kiểm tra xem ô đích đã có ai đứng chưa (ví dụ: Quái vật đã lộ diện)
            if (targetTile.unitOnTile != null && targetTile.unitOnTile != character)
            {
                // Kích hoạt Combat: Người chơi chủ động tấn công quái vật
                var enemyStats = targetTile.unitOnTile.GetComponent<CharacterStats>();
                if (enemyStats != null && CombatManager.Instance != null)
                {
                    CombatManager.Instance.Attack(playerStats, enemyStats);
                }
                
                isMoving = false;
                return; // KHÔNG NHẢY VÀO Ô ĐÃ CÓ NGƯỜI
            }

            isMoving = true;

            bool wasRevealed = targetTile.isRevealed;

            // 1. LẬT Ô TRƯỚC ĐỂ KIỂM TRA (Tránh tình trạng nhảy vào rồi mới biết có mìn)
            targetTile.RevealTile();

            // Nếu lật trúng Quái Vật -> Sinh quái, Bị đánh, và KHÔNG nhảy vào ô đó
            if (targetTile.isMonster)
            {
                GameLogger.Log("BÙM! Đạp trúng quái vật!");
                targetTile.isMonster = false; // Quái đã xuất hiện, xóa cờ mìn

                if (EnemySpawner.Instance != null && CombatManager.Instance != null)
                {
                    // Sinh quái vật ngay tại vị trí ô vừa lật
                    var enemyStats = EnemySpawner.Instance.SpawnEnemyAndGet(targetTile.grid2DLocation);
                    
                    if (enemyStats != null)
                    {
                        // Quái vật cắn người chơi trước
                        CombatManager.Instance.Attack(enemyStats, playerStats);
                    }
                }
                else
                {
                    GameLogger.LogWarning("⚠️ KHÔNG TÌM THẤY EnemySpawner HOẶC CombatManager TRONG SCENE! Hãy kéo Script vào 1 GameObject trống. Tạm thời trừ 3 máu thay vì sinh quái.");
                    playerStats.TakeDamage(3);
                }

                // Giữ nguyên vị trí, không nhảy, nhả cờ isMoving
                OnPlayerStep?.Invoke(character.standingOnTile); 
                isMoving = false;
                return;
            }

            // Nếu lật trúng Rương -> Sinh rương và KHÔNG nhảy vào ô đó
            if (targetTile.isChest)
            {
                GameLogger.Log("Wow! Bạn đã tìm thấy một Rương Báu!");
                targetTile.isChest = false; // Rương đã xuất hiện, xóa cờ mìn

                // Lật trúng rương an toàn (không có quái), thưởng 10 EXP nếu là lần đầu lật
                if (!wasRevealed && playerStats != null)
                {
                    playerStats.AddExp(10);
                }

                if (ChestSpawner.Instance != null)
                {
                    GameObject chestObj = ChestSpawner.Instance.SpawnChest(targetTile);
                    if (chestObj != null)
                    {
                        Chest chestComp = chestObj.GetComponent<Chest>();
                        if (chestComp != null) chestComp.OpenChest();
                    }
                }
                else
                {
                    GameLogger.LogWarning("⚠️ KHÔNG TÌM THẤY ChestSpawner TRONG SCENE! Hãy kéo Script ChestSpawner vào GameManagers.");
                }

                // Giữ nguyên vị trí, không nhảy vào ô đó
                OnPlayerStep?.Invoke(character.standingOnTile); 
                isMoving = false;
                return;
            }

            // Nếu lật trúng Cầu Thang -> Hiện cầu thang và KHÔNG nhảy vào ô đó
            if (targetTile.isStair)
            {
                GameLogger.Log("Cầu thang dẫn xuống tầng sâu hơn đã lộ diện!");
                
                // Thưởng 10 EXP cho việc tìm thấy cầu thang
                if (!wasRevealed && playerStats != null) playerStats.AddExp(10);

                // Hiện bảng hỏi luôn
                ShowStairPopup();

                OnPlayerStep?.Invoke(character.standingOnTile); 
                isMoving = false;
                return;
            }

            // --- Lật được ô trống an toàn (Không quái, không rương) ---
            if (!wasRevealed && playerStats != null)
            {
                playerStats.AddExp(10);
            }

            // 2. NẾU Ô AN TOÀN -> THỰC HIỆN BƯỚC NHẢY VÀO Ô ĐÓ
            // Xử lý SortingOrder ngay lập tức để không bị lỗi đè hình khi đang bay trên không
            character.GetComponent<SpriteRenderer>().sortingOrder = targetTile.GetComponent<SpriteRenderer>().sortingOrder;

            // Tọa độ đích thực sự cần đáp xuống
            Vector3 targetLandingPos = new Vector3(
                targetTile.transform.position.x,
                targetTile.transform.position.y - 0.0001f,
                targetTile.transform.position.z + 0.96f
            );

            // Hàm callback chạy SAU KHI NHẢY XONG
            System.Action onJumpComplete = () =>
            {
                // Cập nhật Logic Grid
                character.standingOnTile.unitOnTile = null; // Rời ô cũ
                character.standingOnTile = targetTile;
                targetTile.unitOnTile = character; // Vào ô mới

                // Trừ Lượt Đi (Steps)
                if (playerStats != null)
                {
                    playerStats.UseStep();
                }

                // Gọi Event báo cho MainHUD biết mình vừa đổi ô
                OnPlayerStep?.Invoke(targetTile);

                isMoving = false;
            };

            // Kích hoạt hoạt ảnh nhảy (Play SFX và gọi Jump)
            var stats = character.GetComponent<CharacterStats>();
            if (stats != null && stats.characterData != null) stats.characterData.PlayRandomMove();

            var moveType = movementSystem.GetMovementType(character.standingOnTile, targetTile);
            if (moveType == MovementType.Climb && climbMover != null)
            {
                climbMover.StartClimb(targetLandingPos, onJumpComplete);
            }
            else if (jumpMover != null)
            {
                jumpMover.StartJump(targetLandingPos, onJumpComplete);
            }
            else
            {
                // Fallback nếu không có Script JumpMover thì dịch chuyển tức thì
                character.transform.position = targetLandingPos;
                onJumpComplete.Invoke();
            }
        }

        private void MovePathStep()
        {
            if (currentPath.Count > 0)
            {
                OverlayTile nextTile = currentPath[0];

                // Nếu ô chuẩn bị bước lên là ô CHƯA MỞ (tức là bước cuối cùng để lật ô)
                if (!nextTile.isRevealed)
                {
                    // Ẩn mũi tên
                    nextTile.SetSprite(ArrowTranslator.ArrowDirection.None);
                    currentPath.RemoveAt(0);

                    // Chuyển giao quyền di chuyển bước cuối cho HandleMovement (cơ chế Dò mìn)
                    HandleMovement(nextTile);
                    return;
                }

                // Nếu ô an toàn -> Di chuyển bằng Jump/Walk như cũ
                nextTile.SetSprite(ArrowTranslator.ArrowDirection.None);

                movementController.MoveAlongPath(character, jumpMover, climbMover, movementSystem, currentPath, () =>
                {
                    // Cập nhật UI HUD sau mỗi bước đi
                    OnPlayerStep?.Invoke(character.standingOnTile);
                    
                    // Đi tiếp bước tiếp theo
                    MovePathStep();
                });
            }
            else
            {
                isMoving = false;
            }
        }

        private void HandleSpawn(OverlayTile tile)
        {
            character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
            playerStats = character.GetComponent<CharacterStats>();
            jumpMover = character.GetComponent<JumpMover>();
            climbMover = character.GetComponent<ClimbMover>();

            character.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y - 0.0001f,
                tile.transform.position.z + 0.96f
            );

            character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            character.standingOnTile = tile;
            tile.unitOnTile = character;

            // Xóa vùng an toàn và Rải mìn (Procedural Generation)
            MapManager.Instance.GenerateDungeon(new Vector2Int(tile.gridLocation.x, tile.gridLocation.y));

            // Lật ô đầu tiên
            tile.RevealTile();
            
            // Cập nhật UI
            OnPlayerStep?.Invoke(tile);
        }
        private void ShowStairPopup()
        {
            if (StairConfirmUI.Instance != null)
            {
                StairConfirmUI.Instance.ShowConfirmPanel();
            }
            else
            {
                GameLogger.LogWarning("Chưa có StairConfirmUI! Tự động chuyển tầng qua DungeonManager.");
                if (DungeonManager.Instance != null) DungeonManager.Instance.NextFloor();
            }
        }
        #endregion
    }
}
