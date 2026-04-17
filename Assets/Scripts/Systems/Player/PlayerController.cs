using System.Collections.Generic;
using UnityEngine;
using static finished3.ArrowTranslator;

namespace finished3
{
    /// <summary>
    /// PlayerController quản lý trạng thái, Spawn, Di Chuyển và Tấn Công của người chơi.
    /// Nhận tọa độ từ MouseController. Chuẩn mô hình Single Responsibility.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        #region Inspector Settings
        [Header("Spawn Settings")]
        [Tooltip("Prefab nhân vật sẽ được sinh ra (Kéo thả từ bên ngoài Scene)")]
        public GameObject characterPrefab;
        #endregion

        #region Core References
        public CharacterInfo character { get; private set; }
        private CharacterStats playerStats;
        private JumpMover jumpMover;
        private ClimbMover climbMover;
        
        private List<OverlayTile> rangeFinderTiles = new List<OverlayTile>();
        private List<OverlayTile> attackTiles = new List<OverlayTile>();
        private List<OverlayTile> path = new List<OverlayTile>();
        #endregion

        #region Internal State
        private bool isMoving;
        private bool isRangeVisible = true;
        public bool IsPlayerSpawned => character != null;
        #endregion

        #region Systems
        private MovementController movementController;
        private AttackController attackController;
        private RangeSystem rangeSystem;
        private TileHighlighter tileHighlighter;
        private ArrowTranslator arrowTranslator;
        private MovementSystem movementSystem;
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            // Báo động đỏ nếu Quên kéo thẻ Prefab vào Inspector!
            UnityEngine.Assertions.Assert.IsNotNull(characterPrefab, "FATAL ERROR: Bạn chưa kéo `characterPrefab` vào PlayerController ở màn hình scene!");

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            movementController = new MovementController();
            attackController = new AttackController();
            rangeSystem = new RangeSystem();
            movementSystem = new MovementSystem();
            tileHighlighter = new TileHighlighter();
            arrowTranslator = new ArrowTranslator();

            isMoving = false;
        }

        private void Update()
        {
            // Thực thi quỹ đạo di chuyển
            if (path.Count > 0 && isMoving && isRangeVisible)
            {
                movementController.MoveAlongPath(
                    character,
                    jumpMover,
                    climbMover,        
                    movementSystem,    
                    path,
                    () =>
                    {
                        GetInRangeTiles();
                        isMoving = false;
                        
                        // ✨ [CHAPTER 1 HOOK] Thông báo đã hoàn thành 1 bước đi
                        if (Chapter1Controller.Instance != null)
                        {
                            Chapter1Controller.Instance.OnPlayerMove();
                        }
                    }
                );
            }
        }
        #endregion

        #region Public API (Được gọi từ MouseController)
        /// <summary>
        /// Xử lý click trái lên một Tile bất kỳ.
        /// </summary>
        public void TapOnTile(OverlayTile tile)
        {
            // ✨ [CHAPTER 1 HOOK] Kiểm tra xem ô gạch này có được phép Tap không (ví dụ: chỉ cho phép ô (2,2) ở đầu game)
            if (Chapter1Controller.Instance != null && !Chapter1Controller.Instance.CanTapTile(tile)) return;

            if (HandleAttack(tile)) return;
            if (HandleSpawn(tile)) return;
            if (HandleMovement(tile)) return;
        }

        /// <summary>
        /// Dọn dẹp/Ẩn toàn bộ hiệu ứng Overlay khi nhấp chuột ra ngoài hay hủy lệnh.
        /// </summary>
        public void CancelAction()
        {
            ClearArrows();
            HideRange();
            isRangeVisible = false;
            path.Clear();
            isMoving = false;
        }

        /// <summary>
        /// Xử lý hiển thị đường đi (Path Preview) khi Hover chuột qua Tile.
        /// </summary>
        public void HoverTile(OverlayTile tile)
        {
            if (isRangeVisible && rangeFinderTiles.Contains(tile) && !isMoving)
            {
                path = movementController.GetPath(character, tile, rangeFinderTiles);
                tileHighlighter.ClearArrows(rangeFinderTiles);
                tileHighlighter.ShowPath(path, arrowTranslator, character.standingOnTile);
            }
            else
            {
                ClearArrows();
            }
        }
        #endregion

        #region Core Gameplay Logic
        private bool HandleAttack(OverlayTile tile)
        {
            if (character == null) return false;

            if (tile.unitOnTile != null && tile.unitOnTile != character)
            {
                if (attackTiles.Contains(tile))
                {
                    attackController.TryAttack(tile, playerStats, () => 
                    {
                        // Callback chạy SAU KHI đòn đánh đã tính toán (Enemy mất máu hoặc chết vỡ tung)
                        HideRange();
                        ClearArrows();
                        GetInRangeTiles(); // Quét gạch lại từ đầu: Quái sống = Ô đỏ, Quái chết gỡ Tile = Ô trắng walkable
                        isRangeVisible = true;
                    });
                    
                    // Xóa CancelAction() ở đây để màn hình không bị tối mù đột ngột khi nhân vật đang múa đòn
                    return true;
                }
                return true;
            }
            return false;
        }

        private bool HandleMovement(OverlayTile tile)
        {
            if (!isRangeVisible)
            {
                if (tile == character.standingOnTile)
                {
                    ShowRange();
                    isRangeVisible = true;
                }
                return false;
            }
    
            if (!rangeFinderTiles.Contains(tile))
            {
                CancelAction();
                return true;
            }

            if (tile == character.standingOnTile)
            {
                CancelAction();
                return true;
            }

            // ✨ [CHAPTER 1 HOOK] Kiểm tra khóa di chuyển của Bước 3
            // Thay vì chặn ngay từ đầu, ta cho hiện Range nhưng rung lắc khi định đi
            if (Chapter1Controller.Instance != null && Chapter1Controller.Instance.IsMovementLocked())
            {
                // 🎵 [SFX] Báo không đi được (Random + Pitch/Pan chuyên nghiệp)
                var stats = character.GetComponent<CharacterStats>();
                if (stats != null && stats.characterData != null)
                    stats.characterData.PlayRandomCannotMove();

                var hitEffect = character.GetComponent<HitEffect>();
                if (hitEffect != null) hitEffect.PlayHit();
                
                // Ép hiện lại Range vì nếu không hệ thống Update trong OverlayTile sẽ tự ẩn mất
                ShowRange();

                GameLogger.Log("Chapter 1: Di chuyển bị chặn. Nhân vật rung lắc báo hiệu.");
                return true;
            }

            path = movementController.GetPath(character, tile, rangeFinderTiles);

            isMoving = true;
            tile.HideTile();
            return true;
        }

        private bool HandleSpawn(OverlayTile tile)
        {
            if (character != null) return false;

            character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
            playerStats = character.GetComponent<CharacterStats>();
            jumpMover = character.GetComponent<JumpMover>();
            climbMover = character.GetComponent<ClimbMover>();

            character.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y - 0.0001f,
                tile.transform.position.z + 0.96f
            );

            character.GetComponent<SpriteRenderer>().sortingOrder =
                tile.GetComponent<SpriteRenderer>().sortingOrder;

            character.standingOnTile = tile;
            tile.unitOnTile = character;

            // ✨ [CHAPTER 1 HOOK] Thông báo đã Spawn TRƯỚC khi tính toán tầm di chuyển
            if (Chapter1Controller.Instance != null)
            {
                Chapter1Controller.Instance.SetPlayerSpawned(true);
            }

            GetInRangeTiles();

            return true;
        }
        #endregion

        #region UI & Helpers
        private void ClearArrows()
        {
            if (rangeFinderTiles == null) return;
            tileHighlighter.ClearArrows(rangeFinderTiles);
        }

        private void HideRange()
        {
            tileHighlighter.ClearTiles(rangeFinderTiles);
            tileHighlighter.ClearTiles(attackTiles);
        }

        private void ShowRange()
        {
            tileHighlighter.ShowMoveRange(rangeFinderTiles);
            tileHighlighter.ShowAttackRange(attackTiles); // 🔧 BUG FIX: Gọi lại vòng Đỏ bị mất hồi nãy
        }

        private void GetInRangeTiles()
        {
            rangeFinderTiles = rangeSystem.GetMoveRange(character, playerStats.moveRange);
            tileHighlighter.ShowMoveRange(rangeFinderTiles);
            attackTiles = rangeSystem.GetAttackRange(character, playerStats.attackRange);
            tileHighlighter.ShowAttackRange(attackTiles);
        }
        #endregion
    }
}
