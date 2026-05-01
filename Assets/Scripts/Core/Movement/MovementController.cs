using System.Collections.Generic;
using UnityEngine;

namespace finished3
{
    public class MovementController
    {
        private PathFinder pathFinder = new PathFinder();
        private List<OverlayTile> path = new List<OverlayTile>();

        public List<OverlayTile> GetPath(CharacterInfo character, OverlayTile target, List<OverlayTile> rangeTiles)
        {
            return pathFinder.FindPath(character.standingOnTile, target, rangeTiles);
        }

        public void MoveAlongPath(
            CharacterInfo character,
            JumpMover jumpMover,
            ClimbMover climbMover,
            MovementSystem movementSystem,
            List<OverlayTile> path,
            System.Action onComplete)
        {
            if (path.Count == 0) return;

            var currentTile = character.standingOnTile;
            var targetTile = path[0];

            var moveType = movementSystem.GetMovementType(currentTile, targetTile);

            switch (moveType)
            {
                case MovementType.Climb:
                    if (!climbMover.IsClimbing)
                    {
                        // 🎵 [SFX] Tiếng bước chân (Chỉ phát 1 lần khi bắt đầu nhảy)
                        var stats = character.GetComponent<CharacterStats>();
                        // 🎵 [SFX] Bước chân (Random + Pitch/Pan)
                        if (stats != null && stats.characterData != null) stats.characterData.PlayRandomMove();

                        climbMover.StartClimb(targetTile.transform.position, () =>
                        {
                            FinishStep(character, targetTile, path, onComplete);
                        });
                    }
                    break;

                case MovementType.Jump:
                case MovementType.Walk:
                    if (!jumpMover.IsJumping)
                    {
                        // 🎵 [SFX] Tiếng bước chân (Chỉ phát 1 lần khi bắt đầu nhảy)
                        var stats = character.GetComponent<CharacterStats>();
                        // 🎵 [SFX] Bước chân (Random + Pitch/Pan)
                        if (stats != null && stats.characterData != null) stats.characterData.PlayRandomMove();

                        jumpMover.StartJump(targetTile.transform.position, () =>
                        {
                            FinishStep(character, targetTile, path, onComplete);
                        });
                    }
                    break;
            }
        }
        private void FinishStep(
            CharacterInfo character,
            OverlayTile tile,
            List<OverlayTile> path,
            System.Action onComplete)
        {
            PositionCharacter(character, tile);
            path.RemoveAt(0);

            onComplete?.Invoke();
        }

        private void PositionCharacter(CharacterInfo character, OverlayTile tile)
        {
            // clear tile cũ
            if (character.standingOnTile != null)
            {
                character.standingOnTile.unitOnTile = null;
            }

            character.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y - 0.0001f, 
                tile.transform.position.z + 0.96f 
            );

            // Không bao giờ được +1 điểm SortingOrder, sẽ phá hỏng Ảo giác Không Gian Trùng của Lớp isometric Y-Axis
            character.GetComponent<SpriteRenderer>().sortingOrder =
                tile.GetComponent<SpriteRenderer>().sortingOrder;

            character.standingOnTile = tile;
            tile.unitOnTile = character;
        }
    }
}