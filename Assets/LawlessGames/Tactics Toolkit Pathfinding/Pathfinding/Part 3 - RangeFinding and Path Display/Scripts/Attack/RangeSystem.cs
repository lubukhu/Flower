using System.Collections.Generic;
using UnityEngine;

namespace finished3
{
    public class RangeSystem
    {
        private RangeFinder rangeFinder = new RangeFinder();

        // 🎯 MOVE RANGE (giữ nguyên logic cũ)
        public List<OverlayTile> GetMoveRange(CharacterInfo character, int range)
        {
            return rangeFinder.GetTilesInRange(
                new Vector2Int(
                    character.standingOnTile.gridLocation.x,
                    character.standingOnTile.gridLocation.y
                ),
                range
            );
        }

        // 🎯 ATTACK RANGE (chuyển từ AttackController sang đây)
        public List<OverlayTile> GetAttackRange(CharacterInfo character, int range)
        {
            List<OverlayTile> result = new List<OverlayTile>();

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
                    result.Add(tile);
                }
            }

            return result;
        }
    }
}