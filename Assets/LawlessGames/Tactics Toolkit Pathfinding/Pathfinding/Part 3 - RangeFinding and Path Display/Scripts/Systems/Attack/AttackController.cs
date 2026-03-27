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
        public enum AttackType
        {
            Normal,
            Heavy
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

            // 🔥 QUAN TRỌNG: chặn spam
            if (attackMover != null && attackMover.IsAttacking)
                return false;

            if (attackMover != null)
            {
                attackMover.StartAttack(targetInfo.transform.position, () =>
                {
                    if (targetInfo != null)
                    {
                        var hitEffect = targetInfo.GetComponent<HitEffect>();
                        hitEffect?.PlayHit();
                    }

                    if (targetStats != null)
                    {
                        CombatManager.Instance.Attack(attacker, targetStats);
                    }
                });
            }
            else
            {
                hitEffect?.PlayHit();
                CombatManager.Instance.Attack(attacker, targetStats);
            }

            return true;
        }
    }
}