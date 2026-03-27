using UnityEngine;

namespace finished3
{
    public class CharacterInfo : MonoBehaviour
    {
        public OverlayTile standingOnTile;

        public void SetPositionOnTile(OverlayTile tile)
        {
            transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y + 0.0001f,
                tile.transform.position.z
            );

            GetComponent<SpriteRenderer>().sortingOrder =
            tile.GetComponent<SpriteRenderer>().sortingOrder + 1;

            standingOnTile = tile;
        }
    }
}