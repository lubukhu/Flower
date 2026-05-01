using UnityEngine;

namespace finished3
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }

        public GameObject enemyPrefab;

        // vị trí grid (tile) test ban đầu
        public Vector2Int spawnPosition;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            UnityEngine.Assertions.Assert.IsNotNull(enemyPrefab, "FATAL ERROR: EnemySpawner đang thiếu enemyPrefab để tạo lính!");
        }

        void Start()
        {
            Spawn();
        }

        void Spawn()
        {
            SpawnEnemyAndGet(spawnPosition);
        }

        public CharacterStats SpawnEnemyAndGet(Vector2Int pos)
        {
            // lấy tile từ map
            if (!MapManager.Instance.map.ContainsKey(pos)) return null;
            var tile = MapManager.Instance.map[pos];

            // tạo enemy
            var enemy = Instantiate(enemyPrefab);
            
            // Thiết lập vị trí trực tiếp để hiển thị (tương tự như cách player đứng)
            enemy.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y - 0.0001f,
                tile.transform.position.z + 0.96f
            );
            
            // Xử lý SortingOrder để đứng chung ô vẫn hiển thị đúng
            enemy.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;

            // Gán thông tin ô cho Quái vật để hệ thống nhận diện
            var characterInfo = enemy.GetComponent<CharacterInfo>();
            if (characterInfo != null)
            {
                characterInfo.standingOnTile = tile;
                tile.unitOnTile = characterInfo; // Đăng ký sở hữu ô gạch này
            }

            return enemy.GetComponent<CharacterStats>();
        }
    }
}