# TÀI LIỆU THIẾT KẾ GAME (GDD)
## Tên dự án dự kiến: Isometric Dungeon Sweeper

---

### 1. Ý TƯỞNG CỐT LÕI (CORE CONCEPT)
- **Thể loại (Genre)**: Puzzle, Roguelike, Dungeon Crawler, Minesweeper.
- **Phong cách hình ảnh (Art Style)**: 2D Isometric. Các ô bản đồ (Tiles), nhân vật và kiến trúc đều được thiết kế theo góc nhìn chéo từ trên xuống.
- **Cơ chế chính**: Sự kết hợp giữa cơ chế dò mìn (Minesweeper) quen thuộc và yếu tố sinh tồn, khám phá hầm ngục (Roguelike). Khác với Minesweeper kinh điển là dùng chuột click để mở ô, trò chơi này yêu cầu người chơi **trực tiếp điều khiển nhân vật di chuyển** từng bước trên mặt phẳng Isometric để lật mở bản đồ.

### 2. CƠ CHẾ GAMEPLAY (GAMEPLAY MECHANICS)

#### 2.1 Di chuyển và Tương tác (Movement & Interaction)
- Nhân vật đứng trên 1 ô Tile. Khi bước sang 1 ô chưa lật (Unknown Tile), ô đó sẽ được mở ra.
- **Tiêu hao năng lượng/lượt đi**: Mỗi bước di chuyển (hoặc mỗi hành động) sẽ tiêu hao chỉ số "Lượt đi" (Đồng hồ cát). Nếu hết Lượt đi, người chơi sẽ bắt đầu bị trừ phần trăm HP theo mỗi bước, hoặc nhận hiệu ứng bất lợi.

#### 2.2 Cơ chế ô lân cận (Minesweeper Logic)
Thay vì chỉ báo số lượng "Mìn", các ô an toàn sau khi được lật mở sẽ hiển thị đồng thời 3 thông tin (dựa vào cấu trúc ảnh tham khảo):
1. **Số lượng Quái vật (Monster)** ở 8 ô xung quanh.
2. **Số lượng Rương (Chest)** ở 8 ô xung quanh.
3. **Số lượng Lối thoát/Bậc thang xuống tầng (Stairs)** ở 8 ô xung quanh.
*Ví dụ*: Một ô hiện lên (Quái: 1, Rương: 1, Thang: 1) nghĩa là ở 8 ô bao quanh nó có chính xác 1 quái, 1 rương, 1 thang. Người chơi phải suy luận để né hoặc tiến tới đúng ô.

#### 2.3 Cấu tạo một màn chơi (Dungeon Floor)
Mỗi tầng sẽ được sinh ngẫu nhiên (Procedural Generation) bao gồm:
- **Vùng xuất phát an toàn**: Khu vực để người chơi bắt đầu đi.
- **Quái vật ẩn**: Giẫm phải ô chứa quái sẽ bị trừ HP ngay lập tức (không có combat, chạm là mất máu).
- **Rương ẩn**: Giẫm lên để nhận Vật phẩm (Bình máu, Năng lượng, Sách phép...).
- **Cầu thang ẩn**: Đích đến của màn chơi. Giẫm lên để đi xuống tầng ngục tiếp theo.

### 3. GIAO DIỆN & CHỈ SỐ NHÂN VẬT (HUD & RESOURCES)

#### Hệ thống Chỉ số (Stats):
- **Trái tim (HP)**: Máu của người chơi. Tối đa phụ thuộc nâng cấp. Bị trừ khi dẫm trúng Quái. Về 0 => Chết / Game Over.
- **Mặt trăng khuyết / Năng lượng (MP / Mana)**: Năng lượng dùng để sử dụng kĩ năng hoặc vật phẩm phép thuật.
- **Lượt đi (Đồng hồ cát - Steps/Turns)**: *Ví dụ: 0 / 30*. Thể hiện chuỗi số bước chân an toàn trước khi kiệt sức. Lượt đi có thể được phục hồi bằng cách qua màn hoặc sử dụng item. 

#### Giao diện Thông tin Màn chơi (Level HUD):
- Góc trên hiển thị: Tên khu vực đang đứng (Vd: Địa hạ tầng 1 - Underground Floor 1).
- Bên cạnh có **Bảng Thống kê Thực thể (Entity Counter)**: Cho biết tổng số lượng Quái, Rương, Thang hiện CÒN LẠI trên tầng này. Giúp người chơi loại trừ trong quá trình giải đố.

#### Kho đồ (Inventory):
- Nơi lưu trữ vật phẩm người chơi tìm được từ Rương hoặc Cửa hàng (nếu có).
- **Các vật phẩm mẫu**: 
  - *Bình Sinh lực / Thể lực (Potions)*: Phục hồi HP hoặc Lượt đi.
  - *Cuốn sách lửa (Spellbook)*: Kỹ năng đặc hữu, có số lượng sử dụng (hoặc ngốn Năng lượng trăng khuyết), ví dụ: Phóng hỏa đốt cháy 1 ô bất kì để tiêu diệt quái vật mà không cần bước lên đó, lật mở 1 vùng rộng lớn, dịch chuyển tức thời, v.v.

### 4. VÒNG LẶP RÚT GỌN (GAME LOOP & ROGUELIKE ELEMENTS)
1. Bắt đầu ở hầm ngục Tầng 1. Số liệu quái thấp, map nhỏ bé.
2. Di chuyển từng bước trên lưới Isometric. Đọc các con số báo hiệu xung quanh để dò đường.
3. Nếu không may giẫm trúng quái vật => Trừ HP.
4. Tìm và Mở rương => Nhập Item giải quyết góc kẹt báo động (VD: Bí lượt đi, xài item soi đường).
5. Phân tích ra vị trí Cầu thang qua màn => Tới bước xuống màn tiếp theo, quái sẽ đông hơn, giới hạn lượt đi ngặt nghèo hơn.
6. Chết (hết HP) => Trở lại điểm khởi đầu (Roguelike), mất hết vật phẩm của run đó. Có thể tích lũy điểm thưởng để mua nâng cấp vĩnh viễn (Rogue-lite) cho lần thám hiểm sau.
