using UnityEngine;

namespace finished3
{
    public class CharacterInfo : MonoBehaviour
    {
        public OverlayTile standingOnTile;

        public void SetPositionOnTile(OverlayTile tile)
        {
            // Chuyển dấu cực + qua - để lướt ảo giác Graphic Y-Sort. 
            // -0.0001f sẽ ép tọa độ Screen chồm lên cao hơn mảnh lưới màu Đỏ Tươi 1 mm, giải quyết vụ "chìm đáy bể màu" triệt để
            transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y - 0.0001f,
                tile.transform.position.z + 0.96f // lỗi cứu cánh khi enemy nằm dưới tile
            );

            // Bỏ đoạn "+ 1" ngớ ngẩn gây lỗi múa Tường hỏng không gian 3D.
            GetComponent<SpriteRenderer>().sortingOrder =
                tile.GetComponent<SpriteRenderer>().sortingOrder;

            standingOnTile = tile;
        }
    }
}