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
            Spawn();
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
            characterInfo.SetPositionOnTile(tile);
            tile.unitOnTile = characterInfo;
        }
    }
}