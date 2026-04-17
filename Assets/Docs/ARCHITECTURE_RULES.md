# TÀI LIỆU KIẾN TRÚC & QUY TẮC PHÁT TRIỂN (ARCHITECTURE & CODING RULES)

Tài liệu này thay thế các bản đồ kiến trúc cũ, tập trung vào việc duy trì tính nhất quán của hệ thống và các "Quy tắc vàng" khi mở rộng dự án "DON'T CLICK".

---

## 1. PHÂN LỚP QUẢN LÝ (LAYER MANAGEMENT)

Dự án được chia thành 3 tầng dữ liệu nghiêm ngặt để đảm bảo Game không bị Spaghetti Code:

### A. Layer Dữ Liệu Tĩnh (Static Data - ScriptableObjects)
- **Đại diện:** `CharacterData`, `ItemData`, `HorrorSoundData`.
- **Nhiệm vụ:** Chứa thông số gốc (HP tối đa, Sát thương, Icon). 
- **Quy tắc:** Tuyệt đối không thay đổi biến trong các file này bằng Code khi đang chạy (Runtime). Chỉ dùng để ĐỌC.

### B. Layer Thực Thi (Logic Layer - MonoBehaviours)
- **Đại diện:** `CharacterStats`, `InventoryManager`, `MovementController`.
- **Nhiệm vụ:** Xử lý tính toán (Trừ máu, di chuyển, nhặt đồ). 
- **Quy tắc:** Khi dữ liệu thay đổi, phải bắn ra **Event** (Action) thay vì gọi trực tiếp sang UI.

### C. Layer Không Gian (Spatial Layer - Grid)
- **Đại diện:** `OverlayTile`, `MapManager`.
- **Nhiệm vụ:** Quản lý tọa độ, vật cản và vị trí thực thể.

---

## 2. QUY TẮC VÀNG KHI SỬA ĐỔI DỮ LIỆU (GOLDEN RULES)

Để tránh các lỗi logic chết người trong dự án Tốt nghiệp, bạn **PHẢI** tuân thủ 3 quy tắc sau:

1.  **NGUYÊN TẮC OBSERVER (Mỏ neo Sự kiện):**
    - Không bao giờ cho UI chạy `Update()` để kiểm tra máu.
    - Phải dùng: `characterStats.OnHealthChanged += UpdateHeartUI;`
    - Điều này giúp Game chạy mượt (60 FPS) ngay cả trên máy cấu hình yếu.

2.  **NGUYÊN TẮC SRP (Đơn nhiệm):**
    - `InventoryManager` chỉ quản lý số lượng Item. Nó không bao giờ tự cộng máu.
    - Nó phải gọi `CharacterStats.Heal()` để thực hiện việc đó.

3.  **QUY TẮC HIỂN THỊ 2.5D (Sorting Order):**
    - Mọi nhân vật phải có vị trí Z là `grid.z + 0.96f` và Y là `grid.y - 0.0001f`.
    - Điều này giúp nhân vật luôn đứng TRÊN ô gạch màu đỏ mà không bị lỗi nhấp nháy (Z-Fighting).

---

## 3. LUỒNG XỬ LÝ KHI THÊM TÍNH NĂNG MỚI (WORKFLOW)

Khi bạn muốn thêm một loại quái vật mới hoặc một cách chết mới:
1.  **Bước 1:** Tạo file `CharacterData` (ScriptableObject) trong thư mục `Settings`.
2.  **Bước 2:** Kéo file đó vào Component `CharacterStats` của Prefab nhân vật.
3.  **Bước 3:** Đăng ký sự kiện `OnDied` trong `GameController` để kích hoạt kịch bản chuyển chương (Next Chapter).

---

## 4. DANH SÁCH FILE QUAN TRỌNG (HOT FILES)
- `MouseController.cs`: Nơi tiếp nhận Input đầu tiên.
- `CharacterStats.cs`: Nơi quản lý sự sống và cái chết.
- `MapManager.cs`: Nơi quản lý "xương sống" của thế giới Game.
