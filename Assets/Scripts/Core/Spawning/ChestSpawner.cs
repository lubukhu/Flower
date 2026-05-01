using UnityEngine;

namespace finished3
{
    public class ChestSpawner : MonoBehaviour
    {
        public static ChestSpawner Instance { get; private set; }

        [Tooltip("Kéo Prefab Rương (Chest) của bạn vào đây")]
        public GameObject chestPrefab;

        [Tooltip("Độ lệch vị trí khi rương xuất hiện so với tâm của ô gạch")]
        public Vector3 spawnOffset = new Vector3(0f, -0.0001f, 0.96f);

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        /// <summary>
        /// Sinh ra Rương tại vị trí ô lật trúng
        /// </summary>
        public GameObject SpawnChest(OverlayTile tile)
        {
            if (chestPrefab == null)
            {
                GameLogger.LogWarning("ChestSpawner chưa được gán chestPrefab!");
                return null;
            }

            // Tạo Chest
            var chest = Instantiate(chestPrefab);
            
            // Thiết lập vị trí với độ lệch tùy chỉnh (spawnOffset)
            chest.transform.position = tile.transform.position + spawnOffset;
            
            // Xử lý SortingOrder để rương hiển thị đúng trên nền gạch
            var spriteRenderer = chest.GetComponent<SpriteRenderer>();
            var tileRenderer = tile.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && tileRenderer != null)
            {
                spriteRenderer.sortingOrder = tileRenderer.sortingOrder;
            }

            // Gắn rương vào ô Grid để PlayerController nhận diện được
            var chestComponent = chest.GetComponent<Chest>();
            if (chestComponent != null)
            {
                tile.chestOnTile = chestComponent;
            }
            else
            {
                GameLogger.LogWarning("Prefab Rương đang thiếu Script Chest.cs!");
            }

            return chest;
        }
    }
}
