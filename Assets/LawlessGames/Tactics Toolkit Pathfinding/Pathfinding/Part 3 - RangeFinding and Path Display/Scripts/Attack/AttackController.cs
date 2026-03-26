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

            var target = tile.unitOnTile.GetComponent<CharacterStats>();

            if (target == null) return false;

            CombatManager.Instance.Attack(attacker, target);
            return true;
        }
    }
}