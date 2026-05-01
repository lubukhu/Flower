# HƯỚNG DẪN THIẾT LẬP UNITY DÀNH CHO BẠN
*(Cẩm nang cần làm ngay khi bạn mở lại được Unity Editor)*

Từ lúc bạn không mở được Unity tới giờ, chúng ta đã code xong rất nhiều hệ thống quan trọng. Dưới đây là các bước thao tác (kéo thả) bạn cần làm trên giao diện Unity để game chạy mượt mà. Đừng lo, cứ làm từ từ từng phần nhé!

---

## PHẦN 1: HỆ THỐNG TÀI NGUYÊN (LƯỢT ĐI & MANA)

**Mục tiêu:** Gắn giao diện hiển thị số Lượt đi (Steps) và Năng lượng (Mana) lên màn hình.

1. **Chuẩn bị UI:**
   - Mở màn hình chứa Canvas của bạn.
   - Ở góc trên cùng bên trái (dưới thanh Máu chẳng hạn), tạo 1 TextMeshPro đặt tên là `StepsText` (hiển thị số `30/30`).
   - Tạo thêm 1 TextMeshPro đặt tên là `ManaText` (hiển thị số `10/10`).
2. **Gắn Script:**
   - Tạo một GameObject trống đặt tên là `ResourceHUD`.
   - Kéo thả file `Assets/Scripts/UI/ResourceHUD.cs` vào GameObject này.
   - Nhìn sang bảng Inspector: Kéo `StepsText` vào ô *Steps Text*, kéo `ManaText` vào ô *Mana Text*.

---

## PHẦN 2: HỆ THỐNG KHO ĐỒ (INVENTORY) & RÚT THƯỞNG

**Mục tiêu:** Tạo vật phẩm, nhét vào Rương, và làm túi đồ chứa vật phẩm rơi ra.

1. **Tạo Vật Phẩm (ScriptableObject):**
   - Click chuột phải Project -> `Create` -> `Flower` -> `Item` -> `Consumable Item`.
   - Tạo 2 file: `HP_Potion` và `MP_Potion`. Điền chỉ số hồi phục vào Inspector.
2. **Cấu hình Rương (Chest):**
   - Mở Prefab rương. Trong script `Chest`, bấm dấu `+` ở `Possible Loot` và kéo 2 lọ thuốc vừa tạo vào.
3. **Làm Giao diện Túi Đồ:**
   - Tạo một Panel đặt tên là `InventoryPanel`. Gắn Component `GridLayoutGroup`.
   - Kéo file `Assets/Scripts/UI/Inventory/InventoryUI.cs` vào Panel này.
   - Tạo các `Slot` con, gắn script `InventorySlotUI.cs` và kéo các thành phần Image/Text tương ứng vào.

---

## PHẦN 3: CƠ CHẾ CHUYỂN TẦNG (CẦU THANG)

**Mục tiêu:** Bảng hỏi xác nhận và quản lý số tầng.

1. **Dungeon Manager:** Tạo GameObject trống tên `DungeonManager`, kéo script cùng tên vào.
2. **Floor HUD:** Tạo TextMeshPro tên `FloorText`, kéo script `FloorHUD.cs` vào.
3. **Confirm Popup:** Tạo Panel `StairConfirmPanel` có 2 nút "Đồng Ý" / "Hủy".
   - Kéo script `StairConfirmUI.cs` vào Panel.
   - Gán Event OnClick cho nút Đồng ý -> `OnConfirmYes`, nút Hủy -> `OnConfirmNo`.

---

## PHẦN 4: HỆ THỐNG KINH NGHIỆM & LEVEL UP (MỚI)

**Mục tiêu:** Hiển thị thanh EXP và bảng chọn nâng cấp khi lên cấp.

1. **Thanh EXP (HUD):**
   - Tạo một **Slider** (Thanh trượt) trong Canvas, đặt tên là `ExpBar`.
   - Tạo một GameObject trống tên là `ExpHUDManager`. Kéo file `Assets/Scripts/UI/ExpHUD.cs` vào.
   - Gán `ExpBar` vào ô *Exp Slider* trong Inspector. Tạo thêm Text để gán vào *Level Text* và *Exp Number Text*.

2. **Bảng Level Up (Popup):**
   - Tạo một Panel lớn nằm giữa màn hình, đặt tên là `LevelUpPanel`. **Quan trọng:** Hãy tắt (Deactivate) nó đi sau khi thiết kế xong.
   - Kéo script `Assets/Scripts/UI/LevelUpUI.cs` vào Panel này.
   - Gán chính cái Panel này vào ô *Level Up Panel* trong Inspector.
   - Tạo **3 Button** (Hồi Máu, Hồi Mana, Tăng Bước Đi). Gán Event OnClick cho từng nút:
     - Nút HP -> `LevelUpUI.OnClickUpgradeHP` (Nhãn nút: **+1 Tim**)
     - Nút Mana -> `LevelUpUI.OnClickUpgradeMana` (Nhãn nút: **+1 Mana**)
     - Nút Bước Đi -> `LevelUpUI.OnClickUpgradeSteps` (Nhãn nút: **+10 Steps**)

---

## PHẦN 5: MÀN HÌNH MENU & CHỌN NHÂN VẬT

**Mục tiêu:** Tạo luồng vào game từ Menu chính.

1. **Game Manager:**
   - Tạo một GameObject tên `GameManager` ở Scene Menu chính.
   - Kéo file `Assets/Scripts/Core/GameManager.cs` vào. Script này sẽ tự giữ lại khi chuyển cảnh.
2. **Menu Chính (Scene_MainMenu):**
   - Tạo Canvas với 2 Button: **Bắt đầu** và **Thoát**.
   - Kéo file `Assets/Scripts/UI/MenuSystems.cs` (Class `MainMenuUI`) vào Canvas.
   - Gán Event OnClick: Nút Bắt đầu -> `MainMenuUI.OnClickPlay`, Nút Thoát -> `MainMenuUI.OnClickQuit`.
3. **Chọn Nhân Vật (Scene_Selection):**
   - Tạo 3 Button cho 3 Class: **Hộ Vệ**, **Trinh Sát**, **Học Giả**.
   - Tạo 1 Button **Vào Game**.
   - Kéo file `Assets/Scripts/UI/MenuSystems.cs` (Class `CharacterSelectionUI`) vào Canvas.
   - Gán Event cho 3 nút Class tương ứng với các hàm `SelectGuardian`, `SelectScout`, `SelectScholar`.
   - Gán Event cho nút Vào Game -> `OnClickStartGame`.

---

## PHẦN 6: THIẾT LẬP TIM & MANA (ZELDA STYLE)

**Mục tiêu:** Hiển thị máu/mana dưới dạng icon (Hỗ trợ nửa icon).

1. **Heart HUD:**
   - Trong Canvas Playing, tìm GameObject chứa các trái tim. Kéo `HeartHUD.cs` vào.
   - Bạn cần chuẩn bị **3 Sprite**: Tim Đầy, Nửa Tim, Tim Rỗng. Kéo vào Inspector.
   - Gán `heartPrefab` (một Image trống) và `heartContainer` (Panel chứa tim).
2. **Mana Icon HUD:**
   - Tương tự như Tim, tạo Panel cho Mana. Kéo `ManaIconHUD.cs` vào.
   - Chuẩn bị **3 Sprite**: Trăng Đầy, Nửa Trăng, Trăng Rỗng.
   - Gán vào Inspector tương ứng.

---

## GHI CHÚ QUAN TRỌNG:
- **Chỉ số khởi đầu:** Game sẽ tự lấy Class bạn đã chọn ở màn hình Selection. Hộ vệ sẽ bắt đầu với 3 tim, các class khác 2 tim.
- **Quy đổi:** 1 Tim = 2 HP. Khi bạn bị quái cắn 1 HP, tim sẽ tự vỡ một nửa.
- **Tăng Tim:** Khi lên cấp và chọn "Tăng Tim", bạn sẽ được cộng thêm **1 Tim tối đa** (tức là 2 HP).

*Chúc bạn thiết lập thành công và sớm mở lại được Unity!*
