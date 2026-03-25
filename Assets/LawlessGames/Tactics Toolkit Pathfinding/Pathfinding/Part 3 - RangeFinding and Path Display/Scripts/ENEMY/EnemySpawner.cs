using UnityEngine;

namespace finished3
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;

        // vị trí grid (tile)
        public Vector2Int spawnPosition;

        void Start()
        {
            Invoke(nameof(Spawn), 0.1f);
        }

        void Spawn()
        {
            SpawnEnemy(spawnPosition);
        }

        void SpawnEnemy(Vector2Int pos)
        {
            // lấy tile từ map
            var tile = MapManager.Instance.map[pos];

            // tạo enemy
            var enemy = Instantiate(enemyPrefab);

            var characterInfo = enemy.GetComponent<CharacterInfo>();

            enemy.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y + 0.0001f,
                tile.transform.position.z
            );

            enemy.GetComponent<SpriteRenderer>().sortingOrder =
                tile.GetComponent<SpriteRenderer>().sortingOrder;

            // 🔥 LINK 2 CHIỀU
            characterInfo.SetPositionOnTile(tile);
            tile.unitOnTile = characterInfo;
        }
    }
}