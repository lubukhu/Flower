using System.Collections.Generic;
using UnityEngine;

namespace finished3
{
    public class TileHighlighter
    {
        public void ShowMoveRange(List<OverlayTile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.ShowTile();
            }
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

        public void ClearTiles(List<OverlayTile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.HideTile();
            }
        }

        public void ClearArrows(List<OverlayTile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.SetSprite(ArrowTranslator.ArrowDirection.None);
            }
        }
        public void ShowPath(List<OverlayTile> path, ArrowTranslator arrowTranslator, OverlayTile startTile)
        {
            for (int i = 0; i < path.Count; i++)
            {
                var previousTile = i > 0 ? path[i - 1] : startTile;
                var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                var arrow = arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                path[i].SetSprite(arrow);
            }
        }
    }
}