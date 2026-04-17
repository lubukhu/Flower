## chương 1. Vòng lặp vô tận

- Tình huống: Vừa vào game sẽ có 1 ô trắng xuất hiện, hệ thống sẽ hướng dẫn di chuyển bằng cách click vào ô trắng. Người chơi di chuyển được 2 lần (mỗi lần 1 ô) rồi không thể di chuyển nữa. Hệ thống tiếp tục cảnh báo.

- Hiệu ứng hình ảnh: ô trắng có hiệu ứng nhấp nháy.

- Hiệu ứng âm thanh: giọng nói hệ thống hướng dẫn :"click here, click here, click here" liên tục để hướng dẫn bắt đầu trò chơi.

- Cách chết: Sau 10s không làm gì người chơi sẽ bị cảnh báo:"click here, click here, click here" liên tục. Nếu người chơi không di chuyển sẽ chết lần đầu tiên.

### Sau khi chết

- Thành tựu: [Dốc hết sức lực]

- Vật phẩm: [Giày-Cũ] (Old Shoes) | Icon: `Item_OldShoes`
- Công dụng: [Giày-Cũ] (Old Shoes) Có thể di xa hơn (2 ô) và không còn bị mất sức.

- Khi chưa sở hữu vật phẩm [Trang-Giấy-Trắng] (Blank Paper)

- Hội thoại mô tả: "Bạn đã cố gắng đi nhưng không thể"

Mô tả về chương 1.

- Chương này sẽ giới thiệu về cơ chế di chuyển của game và cơ chế chết của game.

##########################################################################

## chương 2. Giá như...

- Tình huống: Sau khi hồi sinh, người chơi ở trong một căn phòng xung quanh bao phủ bởi những bụi cỏ thấp, có 2 con đường để đi. Hướng ở trên là đường dẫn đến phòng [Mê-cung-cỏ-thấp] (Low-Grass-Maze). Hướng ở dưới là đường dẫn đến phòng Boss [Mã-Độc] (Malware). Khi bước vào một trong hai hướng thì đường còn lại biến mất. Nếu người chơi ở trong phòng quá lâu và không chọn đi đâu cả thì boss sẽ đi tìm người chơi và kết liễu người chơi.

- Tương tác: Người chơi có thể tương tác với những bụi cỏ thấp. khi ở gần (1 ô), ô của bụi cỏ sẽ có màu đỏ và người chơi có thể ấn vào để đạp lên bụi cỏ, cỏ sẽ biến mất. 

- Giải đố: Cách để lấy vật phẩm [Trang-Giấy-Trắng] (Blank Paper). Người chơi di chuyển tới phòng boss [Mã-Độc] (Malware), người chơi phải đợi boss [Mã-Độc] (Malware) biến mất lúc này boss sẽ di chuyển qua phòng mê cung vật phẩm [Trang-Giấy-Trắng] (Blank Paper), khi đó người chơi tìm được lối đi ẩn trong những bụi cỏ thấp. Khi đi vào sẽ dẫn vào phòng [Hậu-Trường] (Behind The Scene). Đi thẳng tới hết đường sẽ đến được nơi có vật phẩm [Trang-Giấy-Trắng] (Blank Paper). Khi lấy được vật phẩm [Trang-Giấy-Trắng] (Blank Paper) người chơi sẽ bị các bụi cỏ thấp bao quanh và kết liễu người chơi.

### Các phòng của chương

- Phòng: [Mê-cung-cỏ-thấp] (Low-Grass-Maze)
- Mô tả phòng: Mê cung là một nơi bao phủ bằng các lối đi bằng cỏ thấp, ở phía cuối mê cung là vật phẩm [Trang-Giấy-Trắng] (Blank Paper). Khi người chơi hoàn thành mê cung sẽ không lấy được vật phẩm này, boss xuất hiện đốt cháy vật phẩm [Trang-Giấy-Trắng] (Blank Paper) trước mặt người chơi và kết liễu người chơi.

- Phòng: Boss [Mã-Độc] (Malware)
- Mô tả phòng: Khi người chơi chọn đi đường đến boss, người chơi sẽ phải đối mặt với boss [Mã-Độc] (Malware) ngay giữa phòng. Người nếu mà người chơi chạm vào con boss [Mã-Độc] (Malware) thì người chơi bị jump scare và chết.

- Phòng: [Hậu-Trường] (Behind The Scene)
- Mô tả phòng: khi không có vật phẩm [Mắt-Của-Sự-Thật] (Eye of Truth) người chơi chỉ có thể nhìn thấy màn hình đen.

### Sau khi chết

- Thành tựu: [Mắt sáng]
- Vật phẩm: [Mắt-Của-Sự-Thật] (Eye of Truth) | Icon: `Item_EyeOfTruth`
- Công dụng: [Mắt-Của-Sự-Thật] (Eye of Truth) Dùng nó lên bất cứ thứ gì để biết được sự thật đằng sau nó. 

- Vật phẩm: [Trang-Giấy-Trắng] (Blank Paper) | Icon: `Item_BlankPaper`
- Công dụng: [Trang-Giấy-Trắng] (Blank Paper) Nó có thể nghe thấy thế giới này.
- Mô tả: [Trang-Giấy-Trắng] (Blank Paper) Màu trắng của nó không giống với màu trắng của thế giới này, đôi lúc lại có những dòng chữ kỳ lạ xuất hiện rồi biến mất. 

Mô tả về chương 2.

- Chương này sẽ giới thiệu về cơ chế tương tác của game đối với môi trường xung quanh.

##########################################################################

## chương 4. Kẻ thao túng thời gian

- Tình huống: Một cánh cửa quỷ đỏ rực với dòng chữ "Chỉ mở cho linh hồn lang thang lúc 03:00 sáng".

- Tương tác: Người chơi tương tác với cánh cửa sẽ hiện ra dòng chữ.

- Giải đố: Lời nguyền ghi trên một cánh cửa: "Cửa Địa Ngục chỉ mở vào lúc 03:00 sáng. Ai làm trái sẽ quay lại điểm bắt đầu".

- Cách chết: Không thể đợi đến 3 giờ sáng ngoài đời thực. Người chơi phải Alt-Tab ra ngoài Windows, mở phần cài đặt ngày giờ của máy tính (Date & Time settings) và chỉnh đồng hồ hệ thống về 03:00 AM. Quay lại game, cửa địa ngục hiện ra nhân vật bước vào để chết.

### Sau khi chết

- Thành tựu: [Ngủ đi!!!]

- Vật phẩm: [Bút-Chì-Của-Quỷ] (Devil's Pencil) | Icon: `Item_DevilsPencil`
- Cách dùng: Dùng [Bút-Chì-Của-Quỷ] (Devil's Pencil) lên [Trang-Giấy-Trắng] (Blank Paper)
- Công dụng: [Bút-Chì-Của-Quỷ] (Devil's Pencil) Có thể ghi mã lệnh Terminal lên tệp tin hệ thống.

- Khi đã sở hữu vật phẩm [Trang-Giấy-Trắng] (Blank Paper)

- Hội thoại nói ra suy nghĩ của Player: "Có những thời gian qua rồi sẽ không thể lấy lại được"

- Hội thoại hệ thống: "Ở đây làm gì có thời gian ahahaha"

- Khi chưa sở hữu vật phẩm [Trang-Giấy-Trắng] (Blank Paper)

- Hội thoại mô tả: "Bạn đã bị địa ngục nuốt chửng"

##########################################################################


### Những câu nói xuất hiện ngẫu nhiên trong game

- Ngẫu nhiên: Ở một nơi lạnh lẽo nhưng này, tìm được hơi ấm của không quá tệ
- Ngẫu nhiên: Có những thứ trong đời không như ta mong muốn.