using UnityEngine;
using System.Collections.Generic;

namespace finished3
{
    /// <summary>
    /// Các hình thức tấn công cơ bản có thể gán cho đòn đánh.
    /// </summary>
    public enum AttackType
    {
        Normal,
        Heavy
    }

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
        /// <summary>
        /// Xử lý logic tấn công, kiểm tra mục tiêu và kích hoạt Animation cũng như hiệu ứng sát thương.
        /// Thêm sự kiện onComplete để quét lại UI sau khi đòn đánh đáp trúng mục tiêu.
        /// </summary>
        public bool TryAttack(OverlayTile tile, CharacterStats attacker, System.Action onComplete = null)
        {
            if (tile.unitOnTile == null) return false;

            // 🎵 [SFX] Tiếng chém/đâm của người tấn công (Văng ra ngay lập tức bù đắp nhịp trễ Animation)
            // 🎵 [SFX] Tấn công (Random + Pitch/Pan)
            if (attacker.characterData != null) attacker.characterData.PlayRandomAttack();

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
                    onComplete?.Invoke();
                });
            }
            else
            {
                hitEffect?.PlayHit();
                CombatManager.Instance.Attack(attacker, targetStats);
                onComplete?.Invoke();
            }

            return true;
        }
    }
}