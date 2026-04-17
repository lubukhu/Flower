using System.Collections.Generic;
using UnityEngine;
using static finished3.ArrowTranslator;

namespace finished3
{
    public class OverlayTile : MonoBehaviour
    {
        public int G;
        public int H;
        public int F { get { return G + H; } }

        public bool isBlocked = false;

        public OverlayTile Previous;
        public Vector3Int gridLocation;
        public Vector2Int grid2DLocation {get { return new Vector2Int(gridLocation.x, gridLocation.y); } }
        public CharacterInfo unitOnTile;
        public List<Sprite> arrows;



        public bool isShowing = false;

        // ✨ [PHỤC HỒI CODE CŨ] Trả lại hàm Update nguyên bản của hệ thống cũ.
        // Tuy nhiên có thêm chốt chặn an toàn để không làm hỏng giao diện Hướng dẫn của Chapter 1.
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Nếu đang ở Chapter 1 và người chơi chưa Spawn, không được tự ý ẩn gạch (Gạch đang nháy)
                if (PlayerController.Instance != null && !PlayerController.Instance.IsPlayerSpawned)
                {
                    return; 
                }

                HideTile();
            }
        }

        public void HideTile()
        {
            isShowing = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }

        public void ShowTile()
        {
            isShowing = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        public void SetSprite(ArrowDirection d)
        {
            if (d == ArrowDirection.None)
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 0);
            else
            {
                GetComponentsInChildren<SpriteRenderer>()[1].color = new Color(1, 1, 1, 1);
                GetComponentsInChildren<SpriteRenderer>()[1].sprite = arrows[(int)d];
                GetComponentsInChildren<SpriteRenderer>()[1].sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            }
        }
        public void SetAttackColor()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0.3f, 1); // đỏ
        }
    }
}
