using UnityEngine;
using System.Collections.Generic;

namespace finished3
{
    public class AttackController
    {
        private List<OverlayTile> attackTiles = new List<OverlayTile>();

        public List<OverlayTile> GetAttackTiles(CharacterInfo character, int range)
        {
            attackTiles.Clear();

            var pos = new Vector2Int(
                character.standingOnTile.gridLocation.x,
                character.standingOnTile.gridLocation.y
            );

            foreach (var kvp in MapManager.Instance.map)
            {
                var tile = kvp.Value;

                int dx = Mathf.Abs(tile.gridLocation.x - pos.x);
                int dy = Mathf.Abs(tile.gridLocation.y - pos.y);

                int distance = Mathf.Max(dx, dy); // hoặc dx + dy

                if (distance > 0 && distance <= range)
                {
                    attackTiles.Add(tile);
                }
            }

            return attackTiles;
        }

        public void ShowAttackRange(List<OverlayTile> tiles)
        {
            foreach (var tile in tiles)
            {
                if (tile.unitOnTile != null)
                {
                    tile.SetAttackColor();
                }
            }
        }

        public bool TryAttack(OverlayTile tile, CharacterStats attacker)
        {
            if (tile.unitOnTile == null) return false;

            var targetInfo = tile.unitOnTile;
            var targetStats = targetInfo.GetComponent<CharacterStats>();

            if (targetStats == null) return false;

            var attackerGO = attacker.gameObject;

            var attackMover = attackerGO.GetComponent<AttackMover>();
            var hitEffect = targetInfo.GetComponent<HitEffect>();

            // 🔥 nếu có animation thì dùng
            if (attackMover != null)
            {
                attackMover.StartAttack(targetInfo.transform.position, () =>
                {
                    // 🔥 hiệu ứng trúng đòn
                    hitEffect?.PlayHit();

                    // 🔥 gây damage
                    CombatManager.Instance.Attack(attacker, targetStats);
                });
            }
            else
            {
                // fallback nếu không có animation
                hitEffect?.PlayHit();
                CombatManager.Instance.Attack(attacker, targetStats);
            }

            return true;
        }
    }
}