# Khối Kiến Trúc Dòng Chảy Dự Án (Architecture Map)

Đây là tài liệu Bản Đồ Thiết Kế Hệ Thống. Khi gặp lỗi, bạn nhìn vào bản đồ này để biết ngay file Code nào đang nắm quyền kiểm soát và gọi đến file nào. Rút ngắn thời gian truy vết lỗi xuống còn vài giây.

## 1. Sơ Đồ Cây Liên Kết Các Luồng Mã Lệnh (Mermaid Graph)

```mermaid
graph TD
    %% Tầng Nhập Liệu (Input Layer)
    Input[("🖱️ Chuột Người Chơi")] -->|Click/Hover| MC(MouseController)

    %% Tầng Điều Phối Lõi (Brain)
    MC -->|Truyền Tọa đô Tile| PC(PlayerController)

    %% Tầng Hành Trang (Tách biệt khỏi Điều phối đi lại)
    subgraph Inv ["🎒 Hệ thống Hành Trang (Inventory)"]
        UI_Inv[Giao diện InventoryUI] -->|Hiển thị/Nút bấm| IM(InventoryManager)
        IM -->|Đếm Đồ & Dùng Bình| IS[InventorySlot]
        IS -->|Chứa Dữ liệu Item| ID[[ItemData/ConsumableData]]
    end

    %% Tầng Hệ Thống Tính Toán (Logic Systems)
    subgraph S ["⚙️ Các Hệ Thống Tính Toán (Systems)"]
        PC -->|1. Yêu cầu Đo Khoảng Cách| RS(RangeSystem)
        RS -->|Quét A* Bán Kính| RF(RangeFinder)

        PC -->|2. Yêu cầu Vạch Đường| MCtrl(MovementController)
        MCtrl -->|Quét A* Đường đi| PF(PathFinder)
        
        PC -->|3. Yêu cầu Đánh Nhau| AC(AttackController)
    end

    %% Tầng Lõi Dữ Liệu và Bản Đồ (Data / Grid)
    subgraph Core ["🗺️ Lõi Dữ Liệu & Bản Đồ"]
        RF --> Map(MapManager)
        PF --> Map
        Map -->|Chứa các Tile| OT(OverlayTile)
    end

    %% Tầng Xử lý Sát thương và Nhân vật (Entities)
    subgraph Entities ["💂 Nhân Vật & Thông Số"]
        AC -.->|Kích hoạt Hiệu ứng| AM(AttackMover)
        AC -->|Gửi Data Sát thương| CM(CombatManager)
        CM -->|Trừ Máu / Nhận Buff| CS(CharacterStats)
        CS -->|Lấy Máu gốc từ Set| CD[[CharacterData (SO)]]
        CS -.->|Tắt Xác (Pool)| CI(CharacterInfo)
        
        IM -.->|Nếu Dùng Bình (Consume)| CS
    end

    %% Tầng Hiển thị / Đồ Họa (Visuals)
    subgraph UI ["✨ Hiển thị Môi Trường"]
        PC -.->|Ra lệnh Vẽ Màu (Gọi liên tục)| TH(TileHighlighter)
        TH -.->|Phủ 1 miếng Gạch 2D màu Trắng/Đỏ| OT
    end

    %% Styles
    classDef brain fill:#f9f,stroke:#333,stroke-width:2px;
    classDef sys fill:#bbf,stroke:#333,stroke-width:2px;
    classDef data fill:#dfd,stroke:#333,stroke-width:2px;
    classDef inv fill:#ffd700,stroke:#333,stroke-width:2px;

    class PC brain;
    class MCtrl,AC,RS sys;
    class Map,CD,CS,ID data;
    class IM,IS,UI_Inv inv;
```

## 2. Hướng dẫn Truy Vết Lỗi Nhanh (Cheatsheet)

Nếu Game của bạn gặp lỗi ở một khía cạnh cụ thể, **ĐỪNG mở bừa các file lên đọc!** Chỉ mở đúng các file trong nhóm đó:

**A. Lỗi Bấm Chuột (Input không nhận, Rê chuột không sáng, Kêu lách cách):**
📌 *File chịu trách nhiệm:* `MouseController.cs` và `TileHighlighter.cs`

**B. Lỗi Thuật Toán Đi Bộ (Đi xuyên tường, Rớt đài, Kẻ đường lố khoảng cách):**
📌 *File chịu trách nhiệm:*
1. Sinh vùng màu giới hạn: `RangeFinder.cs`, `RangeSystem.cs`
2. Thực hiện Quét đường đi thực để kéo Anim Mover: `PathFinder.cs`, `MovementController.cs`

**C. Lỗi Đồ Đạc / Bơm Máu Hành Trang:**
📌 *File chịu trách nhiệm:*
1. Thay đổi tính toán Slot, số lượng: `InventoryManager.cs`
2. Bình Máu không bơm, báo đầy Máu sai: `ConsumableData.cs` và `CharacterStats.cs`

**D. Lỗi Hiển thị Đồ họa Không Gian 3D / Y-Sort / Đè Hình Đỏ Trắng:**
📌 *File chịu trách nhiệm:* `CharacterInfo.cs`, `MovementController.cs`, `PlayerController.cs` (HandleSpawn)
- **Quy tắc Vàng Số 1:** Mọi nhân vật đều phải thiết lập vị trí gốc là `Y - 0.0001f` và `Z + 0.96f`. 
- **Quy tắc Vàng Số 2:** TUYỆT ĐỐI không được cộng trừ thủ công `SortingOrder`. Hệ thống dùng Y để phát hiện chân tường 3D, và dùng Z để lôi Cạnh Nhân Vật lên đè lấp hoàn toàn Miếng Tile Đỏ Trắng nằm ngang!

## 3. Quy chuẩn Khi Viết Code Mới (Nghiêm Cấm)
- Khối Bơm Máu/Sát thương: Tuyệt đối không để `MouseController` dính líu đến trừ Máu/Đánh nhau. Phải ném Event về `CombatManager` / Phím Căng.
- Khối Túi Đồ: Hệ thống `Inventory` Không tự ôm việc cộng Sinh Lực, nó chỉ gọi hàm Giao cắm bên trong API `CharacterStats.Heal()`.
- Khối Đồ Hoạ Hiển Thị Nền: Tuyệt đối không dùng `Destroy()` đối với Nhân Vật/Quái. Hãy dùng `gameObject.SetActive(false)` tại `CharacterStats.Die()` để chờ Hệ thống Object Pool nhặt lại xác (Tránh quá tải RAM Mobile).
- Khối UI (Thanh Máu, Số Lượng Item): Phải móc vào sự kiện Mỏ Neo (`OnHealthChanged` / `OnInventoryChanged`). Khối Giao diện không bao giờ được phép trực tiếp sửa Đồ Của Người Chơi.
