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

            // 🔧 BUG FIX: Buộc nhân vật phải có điểm vẽ đồ họa (SortingOrder) lớn hơn mảnh Đất
            // Để lớp lưới màu Trắng (Move) và Đỏ (Attack) không bao giờ "nhuộm lấp" mất hình ảnh quái vật
            GetComponent<SpriteRenderer>().sortingOrder =
                tile.GetComponent<SpriteRenderer>().sortingOrder + 1;

            standingOnTile = tile;
        }
    }
}