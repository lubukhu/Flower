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
    }
}