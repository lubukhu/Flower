using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace finished3
{
    public class RangeFinder
    {
        public List<OverlayTile> GetTilesInRange(Vector2Int location, int range)
        {
            var startingTile = MapManager.Instance.map[location];
            var inRangeTiles = new List<OverlayTile>();
            int stepCount = 0;

            inRangeTiles.Add(startingTile);

            //Should contain the surroundingTiles of the previous step. 
            var tilesForPreviousStep = new List<OverlayTile>();
            tilesForPreviousStep.Add(startingTile);
            while (stepCount < range)
            {
                var surroundingTiles = new List<OverlayTile>();

                foreach (var item in tilesForPreviousStep)
                {
                    var neighbors = MapManager.Instance.GetSurroundingTiles(
                        new Vector2Int(item.gridLocation.x, item.gridLocation.y)
                    );

                    foreach (var neighbor in neighbors)   // ✅ đổi tên
                    {
                        if (neighbor.unitOnTile != null)
                            continue;

                        surroundingTiles.Add(neighbor);
                    }
                }
                inRangeTiles.AddRange(surroundingTiles);
                tilesForPreviousStep = surroundingTiles.Distinct().ToList();
                stepCount++;
            }

            return inRangeTiles.Distinct().ToList();
        }
    }
}
