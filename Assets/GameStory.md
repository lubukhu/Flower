# DON'T CLICK - TÀI LIỆU KỊCH BẢN & CẤU TRÚC LOGIC (GDD)

Dự án này là một trải nghiệm **Meta-game Kinh dị Tâm lý**. Mục tiêu của kịch bản này là xóa nhòa ranh giới giữa người chơi thực và nhân vật ảo.

---

## chương 1. Vòng lặp vô tận

- Tình huống: Ngay khi bước chân vào thế giới này, một ô trắng đơn độc xuất hiện. Hệ thống thì thầm hướng dẫn bạn di chuyển bằng cách nhấp chuột vào nó. Bạn chỉ có thể tiến bước hai lần (mỗi lần 1 ô) trước khi đôi chân trĩu nặng, không thể nhích thêm dù chỉ một phân. Ngay lập tức, những lời cảnh báo bắt đầu vang lên dồn dập.
  - *[Logic: Giới hạn `MovementController` thông qua một biến 'stepCount' trong `PlayerController`. Sau 2 bước, set trạng thái 'Locked' cho MovementSystem.]*

- Hiệu ứng hình ảnh: Ô trắng nhấp nháy liên tục như một nhịp tim yếu ớt giữa hư không.
  - *[Logic: Điều khiển thông qua `TileHighlighter.cs` với hiệu ứng Lerp Alpha hoặc Sin Time.]*

- Hiệu ứng âm thanh: Giọng nói cơ khí của hệ thống lặp đi lặp lại: "click here, click here, click here..." một cách ám ảnh để thúc giục bạn bắt đầu trò chơi.
  - *[Logic: Gọi `HorrorAudioManager` phát Loop một `HorrorSoundData` cụ thể.]*

- Cách chết: Nếu bạn đứng bất động quá 10 giây, sự kiên nhẫn của hệ thống sẽ cạn kiệt. Tiếng thét "click here" sẽ vang lên chói tai. Nếu vẫn không di chuyển, linh hồn bạn sẽ tan biến ngay trong lần đầu tiên này.
  - *[Logic: Khởi tạo một `Timer` trong `GameController.cs`. Nếu `isMoving` là false liên tục 10s => Gọi `CharacterStats.Die()`.]*

### Sau khi chết
- Thành tựu: [Dốc hết sức lực]
- Vật phẩm: `[Giày-Cũ] (Old Shoes)` | Icon: `Item_OldShoes`
- Công dụng: Giúp bạn nhẹ bước hơn, có thể di chuyển xa hơn (2 ô) mà không còn cảm thấy mệt mỏi.
  - *[Logic: Cập nhật `CharacterData` (ScriptableObject) tăng chỉ số 'Range' cho `MovementController`.]*

- Khi chưa sở hữu vật phẩm [Trang-Giấy-Trắng] (Blank Paper)
- Hội thoại mô tả: "Bạn đã cố gắng bước tiếp, nhưng đôi chân cứ nặng dần như đang hòa quyện vào mặt đất..."

Mô tả về chương 1.
Chương khởi đầu của một hành trình không lối thoát, nơi người chơi phải học cách bước đi hoặc chấp nhận bị hư vô nuốt chửng khi sự kiên nhẫn của hệ thống cạn kiệt.

---

## chương 2. Giá như...

- Tình huống: Trở về từ cõi chết, bạn thấy mình đứng giữa một căn phòng bao phủ bởi những bụi cỏ thấp khô héo. Hai con đường mở ra trước mắt. Hướng phía trên dẫn đến [Mê-cung-cỏ-thấp]. Hướng phía dưới đưa bạn đến một nơi mà Boss [Mã-Độc] đang đứng. Một khi đã chọn, con đường còn lại sẽ biến mất. Đừng chần chừ quá lâu, thời gian sẽ cạn dần và [Mã-Độc] sẽ tìm đến bạn.
  - *[Logic: `MapManager` sẽ xóa/vô hiệu hóa các `OverlayTile` của hướng còn lại khi người chơi bước vào Trigger vùng đã chọn.]*

- Tương tác: Những bụi cỏ không chỉ là vật trang trí. Khi tiến lại gần (1 ô), chúng sẽ rực đỏ. Một cú nhấp chuột chính xác sẽ khiến bạn đạp nát chúng, mở đường tiến bước.
  - *[Logic: `RangeSystem.cs` kiểm tra khoảng cách. Nếu `OverlayTile.occupant` là Grass => `TileHighlighter` chuyển màu Đỏ (Warning).]*

- Giải đố: Bí mật của [Trang-Giấy-Trắng] (Blank Paper). Đừng vội vàng bước vào mê cung, vì đó là một cái bẫy chết người. Đầu tiên, hãy tiến vào phòng của Boss [Mã-Độc] (Malware) và giữ khoảng cách an toàn. Bạn phải kiên nhẫn **đợi** cho đến khi thực thể này rời khỏi vị trí và di chuyển sang phòng mê cung. Ngay khi Boss biến mất, hãy **đạp mạnh lên những bụi cỏ** tại phòng Boss để tìm thấy một lối đi ẩn dẫn vào [Hậu-Trường] (Behind The Scene). Cuối con đường tằm tối ấy sẽ đưa bạn đến đúng vị trí đặt [Trang-Giấy-Trắng] (Blank Paper) trong mê cung mà không bị Boss phát hiện. Khi lấy được vật phẩm, những bụi cỏ xung quanh sẽ thức tỉnh và kết liễu bạn, nhưng bạn sẽ giữ được bí mật của thế giới này trong lần hồi sinh kế tiếp.
  - *[Logic: Trình tự State của Boss quản lý qua `GameState.cs`. Lối đi ẩn thực chất là các `OverlayTile` có flag `isWalkable = false` cho đến khi Grass bị Destroy.]*

### Các phòng của Chương 2
- Phòng: [Mê-cung-cỏ-thấp] (Low-Grass-Maze): Một mê cung cỏ thấp rối rắm với lối đi hẹp. Ở phía cuối mê cung, bạn sẽ nhìn thấy [Trang-Giấy-Trắng] (Blank Paper) nằm ngay trước mắt. Tuy nhiên, nếu bạn đi con đường này, ngay khi chuẩn bị chạm tay vào vật phẩm, Boss [Mã-Độc] (Malware) sẽ đột ngột xuất hiện, đốt cháy tờ giấy ngay trước mặt bạn và kết liễu bạn trong sự tuyệt vọng.
- Phòng: Boss [Mã-Độc] (Malware): Nơi ngự trị của thực thể [Mã-Độc] (Malware). Bạn phải quan sát hành vi của nó và tìm kiếm lối đi ẩn giấu dưới những lớp cỏ đỏ rực sau khi nó rời đi. Nếu chạm vào nó khi nó còn ở đây, một cú jump scare sẽ gửi bạn về vạch xuất phát.
- Phòng: [Hậu-Trường] (Behind The Scene): Một không gian phi thực thể nằm ngoài ranh giới của trò chơi, chỉ có thể tiếp cận qua lối đi ẩn ở phòng Boss. Nếu không có [Mắt-Của-Sự-Thật], tất cả những gì bạn thấy chỉ là một màn hình đen đặc quánh, khiến việc định vị trở nên cực kỳ khó khăn.

### Sau khi chết
- Vật phẩm: `[Mắt-Của-Sự-Thật] (Eye of Truth)` | Icon: `Item_EyeOfTruth`
- Công dụng: Nhìn thấu những ảo ảnh, dùng nó để lột bỏ lớp vỏ bọc và tìm thấy sự thật ẩn giấu đằng sau mọi vật.
- Vật phẩm: `[Trang-Giấy-Trắng] (Blank Paper)` | Icon: `Item_BlankPaper`
- Công dụng: [Trang-Giấy-Trắng] (Blank Paper) không chỉ là giấy, nó là đôi tai của thế giới này. Đôi lúc, những dòng mật mã hệ thống sẽ hiện lên rồi tan biến như những bóng ma.

Mô tả về chương 2.
Chương này giới thiệu về khả năng tương tác với môi trường (`RangeSystem` & `Interaction`) và tư duy logic để tìm ra những con đường không trải sẵn.

---

## chương 4. Kẻ thao túng thời gian

- Tình huống: Một cánh cửa quỷ đỏ rực hiện ra với dòng chữ khắc bằng máu: "Chỉ mở cho linh hồn lang thang lúc 03:00 sáng".
- Tương tác: Khi chạm tay vào cánh cửa, hơi lạnh toát ra khiến bạn rùng mình, kèm theo thông điệp nhắc nhở về quy luật của thời gian.
- Giải đố: Lời nguyền ghi trên cổng địa ngục: "Ai làm trái luật lệ của thời gian sẽ mãi mãi mắc kẹt tại điểm bắt đầu".
    - Để vượt qua, người chơi phải can thiệp vào hệ thống: mở **Date & Time settings** trên Windows và chỉnh giờ máy tính thành đúng 03:00 AM.
    - Quay lại game, cánh cửa sẽ tự động mở ra, mời gọi bạn vào cõi chết cuối cùng.
  - *[Logic: `GameController.cs` sử dụng `System.DateTime.Now`. Nếu `DateTime.Now.Hour == 3` => Set `Door.isOpen = true`.]*

### Sau khi chết
- Thành tựu: [Ngủ đi!!!]
- Vật phẩm: `[Bút-Chì-Của-Quỷ] (Devil's Pencil)` | Icon: `Item_DevilsPencil`
- Công dụng: Cho phép bạn ghi đè mã lệnh Terminal vào tệp tin gốc của hệ thống, chuẩn bị cho màn kết.

---

## Hội thoại & Những lời thì thầm

- Khi có [Trang-Giấy-Trắng] (Blank Paper): "Có những khoảnh khắc đã trôi qua... vĩnh viễn không thể tìm lại."
- Tiếng cười của Hệ thống: "Ở đây làm gì có thời gian mà mong đợi? Ahahahaha!"
- Ngẫu nhiên: "Ở một nơi lạnh lẽo như thế này, tìm thấy một chút hơi ấm... cũng không quá tệ."
- Ngẫu nhiên: "Đừng tin vào những gì bạn thấy, hãy tin vào những gì bạn cảm nhận được."
  - *[Logic: Dữ liệu này được đổ vào `CharacterInfo.cs` hoặc một `DialogueSystem` để hiển thị UI ngẫu nhiên.]*
