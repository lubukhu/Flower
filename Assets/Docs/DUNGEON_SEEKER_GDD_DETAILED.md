
MỤC LỤC
1. PHẦN 1 – GAME DESIGN DOCUMENT	8
1.1. Giới thiệu chung	8
1.1.1. Giới thiệu	8
1.1.2. Xác định phạm vi tài liệu	8
1.1.3. Nội dung chính của trò chơi (Elevator Pitch)	9
1.1.4. Đối tượng người chơi hướng đến (Target Audience)	9
1.2. Tổng quan về game	10
1.2.1. Nội dung và mục tiêu của game (Game concept)	10
1.2.2. Thể loại của game (Genre)	11
1.2.3. Giải đố chiến thuật (Puzzle - Strategy RPG)	12
1.2.4. Roguelike	13
1.2.5. Bối cảnh của game	15
1.2.6. Cấu trúc, cách chơi của game	16
1.2.7. Nhân vật chính, số lượng người chơi	18
1.3. Cách chơi, mục tiêu và sự tiến triển trong game	19
1.3.1. Mục tiêu chính và mục tiêu phụ trong game	19
1.3.2. Sự tiến triển trong game	20
1.3.3. Sự tiến triển của câu chuyện trong game	22
1.3.4. Cân bằng độ khó của game	23
1.4. Quy tắc và cơ chế vận hành của game	25
1.4.1. Quy tắc Khám phá (Discovery Rules):	25
1.4.2. Quy tắc Di chuyển và Tài nguyên (Movement & Resource Rules):	25
1.4.3. Quy tắc Tương tác và Trừng phạt (Interaction & Penalty Rules):	26
1.4.4. Điều kiện Kết thúc (End-game Conditions):	26
1.5. Hệ thống đồ họa và âm thanh	27
1.5.1. Hệ thống đồ họa (Graphics)	27
1.5.2. Hệ thống âm thanh (Audio)	28
1.6. Cốt truyện và nhân vật	29
1.6.1. Cốt truyện (The Story of the Seeker)	29
1.6.2. Hệ thống nhân vật (Characters)	30
1.7. Chi tiết về thế giới game và cấp độ	31
1.7.1. Cấu trúc cấp độ (Level Structure)	31
1.7.2. Phân khu môi trường (Environment Themes)	32
1.7.3. Các thực thể tương tác trong cấp độ	33
1.7.4. Sự tiến triển độ khó (Difficulty Progression)	33
1.8. Tối ưu hóa gameplay, Kế hoạch phát triển và Phát hành	34
1.8.1. Tối ưu hóa gameplay và Quản lý độ khó	34
1.8.2. Kế hoạch phát triển (Development Roadmap)	34
1.8.3. Kiểm thử và Bảo đảm chất lượng (QA & Testing)	35
1.8.4. Kế hoạch phát hành và Tiếp cận thị trường	35
2. PHẦN 2 – TÀI LIỆU KỸ THUẬT	36
2.1. Phân tích ý tưởng game (SWOT Analysis)	36
2.2. Thiết kế Game (Sơ đồ Game Loop)	38
2.2.1. Mô tả chi tiết Luồng vận hành (Game Loop Description)	39
2.2.2. Hệ thống Chức năng (Core Features)	39
2.2.3. Sơ đồ lớp (Class Diagram - Sơ lược)	40
2.2.4. Phân tách trạng thái và Cấu trúc màn chơi (System Architecture)	40
2.2.5. Đặc trưng Kỹ thuật (Technical Summary)	41
2.2.6. Chi tiết Tài nguyên Dự án (Project Assets)	42
2.2.7. Chi tiết Thiết kế Gameplay & Âm thanh	43
2.3. Công nghệ & hệ thống	45


LỜI CẢM ƠN
Dự án game DUNGEON SEEKER là thành quả của sự nỗ lực, cố gắng và kiên trì không ngừng nghỉ của em trong suốt thời gian qua. Tuy nhiên, dự án này sẽ khó có thể hoàn thiện nếu thiếu đi sự hướng dẫn, chỉ bảo và hỗ trợ nhiệt tình từ giảng viên - Thầy Nguyễn Ngọc Chấn, trường Cao đẳng FPT Polytechnic. Em xin được gửi lời cảm ơn chân thành và sâu sắc nhất đến Thầy Nguyễn Ngọc Chấn. Nhờ những chia sẻ, kinh nghiệm quý báu và sự sát sao của thầy từ những ngày đầu tiên khi em vừa lên ý tưởng triển khai. Dù quá trình thực hiện gặp nhiều khó khăn, thầy luôn là người định hướng, sẵn sàng giải đáp các vướng mắc và đưa ra các hướng xử lý kịp thời, giúp em vững bước và hoàn thành trọn vẹn trò chơi DUNGEON SEEKER. Cuối cùng, em xin gửi lời tri ân đến Ban lãnh đạo trường Cao đẳng FPT Polytechnic cùng toàn thể Quý thầy cô ngành Lập trình Game.
Cảm ơn nhà trường và các thầy cô đã luôn dìu dắt, truyền đạt kiến thức và tạo mọi điều kiện thuận lợi nhất để em được học tập, phát triển và hoàn thành tốt đồ án tốt nghiệp này. Em xin chân thành cảm ơn!

Chúng em xin chân thành cảm ơn quý thầy cô!
Thành phố Hồ Chí Minh, ngàyㅤthángㅤnăm 2026
Sinh viên
	
	
	
	
	
	
	
	
LỜI MỞ ĐẦU
Hiện nay, ngành công nghiệp game đang phát triển mạnh mẽ và trở thành một trong những lĩnh vực giải trí có sức ảnh hưởng sâu rộng trên toàn cầu. Với sự tiến bộ nhanh chóng của công nghệ và các công cụ hỗ trợ, việc phát triển một trò chơi điện tử không còn là điều xa vời đối với sinh viên hay những người đam mê sáng tạo nội dung số. Trong bối cảnh đó, nhu cầu trải nghiệm các tựa game đòi hỏi tư duy logic kết hợp với yếu tố sinh tồn ngẫu nhiên (roguelike) đang ngày càng được cộng đồng người chơi đón nhận nồng nhiệt vì tính thử thách và giá trị chơi lại cao. Xuất phát từ niềm đam mê với thể loại giải đố (puzzle) và mong muốn mang đến một góc nhìn hoàn toàn mới cho lối chơi "Dò mìn" (Minesweeper) kinh điển, em đã quyết định phát triển dự án "DUNGEON SEEKER" – một tựa game giải đố thám hiểm hầm ngục được thiết kế với đồ họa 2D góc nhìn Isometric. Trò chơi đưa người chơi vào hành trình khám phá những tầng hầm ngục tăm tối, nơi họ phải từng bước lật mở bản đồ, sử dụng các con số gợi ý để suy luận vị trí quái vật ẩn, tìm kiếm rương vật phẩm cứu viện và lối thoát hiểm. Để sống sót qua các tầng ngục được tạo ngẫu nhiên, người chơi phải thật sự cẩn trọng rèn luyện tư duy logic, đồng thời tính toán chi li từng bước đi và lượng máu ít ỏi của mình. Dự án được phát triển bằng nền tảng Unity, nhằm giúp em tiếp cận sâu hơn với quy trình thiết kế và lập trình game 2D Isometric. Đây cũng là cơ hội quý báu để em áp dụng các bài học vào thực tế, đồng thời nghiên cứu sâu hơn về các kỹ thuật nâng cao như: thuật toán tạo hình bản đồ ngẫu nhiên (Procedural Generation), logic lưới (Grid System), và tư duy quản lý tài nguyên hệ thống phức tạp trong game. Trong quá trình phát triển dự án với tư cách cá nhân đảm nhận toàn bộ các khâu, em đã gặp không ít thách thức, từ việc tự mình giải quyết các vấn đề logic code phức tạp cho đến việc dồn toàn lực phân bổ thời gian để hoàn thiện sản phẩm. Tuy nhiên, với tinh thần cầu tiến và sự hỗ trợ, định hướng tận tình từ thầy cô, em đã từng bước hoàn thiện dự án. Em hi vọng đây không chỉ dừng lại ở một sản phẩm học thuật kiểm tra năng lực, mà còn là bước đệm vững chắc cho những sáng tạo chuyên nghiệp hơn trong mảng phát triển game sau này. Em xin chân thành cảm ơn sự quan tâm, hướng dẫn và những lời góp ý quý báu từ quý thầy cô để dự án DUNGEON SEEKER có thể tiếp tục hoàn thiện và phát triển hơn nữa.
GIỚI THIỆU DỰ ÁN
Trong thời đại mà công nghệ giải trí ngày càng phát triển, nhu cầu tìm kiếm những trò chơi mang tính giải đố kích thích tư duy, kết hợp với yếu tố sinh tồn ngẫu nhiên (roguelike) đang trở nên phổ biến đối với cộng đồng game thủ. Tuy nhiên, phần lớn các trò chơi hiện nay thường đi theo lối mòn chiến đấu hành động hoặc giải đố đơn thuần mà thiếu đi sự giao thoa sáng tạo giữa hai trải nghiệm này. Điều đó đặt ra nhu cầu về một tựa game vừa nhẹ nhàng, thân thuộc dễ tiếp cận, nhưng lại sở hữu chiều sâu về sự tính toán logic và khả năng chơi lại (replayability) vô tận.
Từ thực tế đó, dự án DUNGEON SEEKER được xây dựng với mục tiêu mang đến một làn gió mới cho dòng game kinh điển dò mìn (Minesweeper) bằng cách lồng ghép khéo léo các yếu tố sinh tồn (Roguelike) và thám hiểm hầm ngục (Dungeon Crawler) trên đồ họa 2D góc nhìn Isometric. Trò chơi đưa người chơi hóa thân thành một nhà thám hiểm kiên cường, dấn thân vào một hầm ngục sâu thẳm đầy rẫy bí ẩn. Tại đây, những cuộc chiến không được giải quyết bằng gươm giáo hay lòng dũng cảm mù quáng; thay vào đó, chìa khóa sinh tồn duy nhất nằm ở tư duy logic. Người chơi phải thu thập các dữ kiện số học nằm trên mặt đất để suy luận ra vị trí quái vật ẩn, định vị rương vật phẩm và tìm lối thoát xuống những tầng ngục tối tăm hơn.
DUNGEON SEEKER hướng đến nhóm người chơi yêu thích thử thách trí tuệ, say mê thể loại giải đố chiến lược và có tính kiên nhẫn cao. Với cấu trúc lối chơi không yêu cầu nhịp độ phản xạ nhanh mà đề cao sự tỉ mỉ trong từng bước đi, kết hợp với phong cách Isometric gọn gàng, trò chơi mang đến một trải nghiệm vừa trắc trở, kịch tính nhưng lại vô cùng lôi cuốn, khơi dậy tinh thần chinh phục mạnh mẽ. Bên cạnh cơ sở suy luận của thể loại giải đố, game còn tăng cường chiều sâu với các hệ thống:
Tiến trình sinh tồn ngẫu nhiên (Roguelike): Mỗi lần bước vào game là một địa hình lưới hoàn toàn mới được sinh ngẫu nhiên, nếu tử nạn người chơi sẽ mất trắng và phải quay lại từ đầu.
Hệ thống quản lý tài nguyên chiến lược: Việc di chuyển bị giới hạn bởi chỉ số "Lượt đi" (Steps), cùng với lượng sinh lực (HP) và năng lượng (Mana) eo hẹp, ép buộc người chơi phải dùng đầu óc để tối ưu hóa từng nước cờ.
Đa dạng hóa lối chơi thông qua vật phẩm: Sự xuất hiện thiết yếu của các sách phép thuật hoặc bình sinh lực để cứu nguy vào lúc góc suy luận rơi vào bế tắc.
Dự án được xây dựng hoàn toàn trên nền tảng Unity, giúp em không chỉ trau dồi mạnh mẽ tư duy lập trình C# mà còn có cơ hội áp dụng thực tế các thuật toán nâng cao như cấu trúc lưới (Grid System) và tạo bản đồ ngẫu nhiên (Procedural Generation). Đặc biệt hơn, thông qua việc đảm nhiệm độc lập toàn bộ các khâu thiết kế, mã hóa và tổ chức hình ảnh, dự án đã giúp em rèn luyện tính kỷ luật cá nhân, khả năng tự nghiên cứu, giải quyết vấn đề và quản lý dự án hiệu quả. Với tất cả tâm huyết đó, em mong muốn DUNGEON SEEKER sẽ trở thành một minh chứng tích cực cho năng lực tổng hợp kiến thức đã học, đồng thời là một sản phẩm game có chất lượng và tính sáng tạo cao. Em rất mong nhận được những đánh giá, đóng góp quý báu từ quý thầy cô để định hướng hoàn thiện trò chơi hơn nữa trong tương lai.
Thành viên dự án:

Lê Bá Khôi
- Phân tích và Thiết kế Game
- Thiết kế Giao diện (UI/UX)
- Lập trình Gameplay
- Gmail: khoilbts00564@fpt.edu.vn
- MSSV: TS00564





PHẦN 1 – GAME DESIGN DOCUMENT
Giới thiệu chung
Giới thiệu
Tài liệu Thiết kế Trò chơi (Game Design Document - GDD) này được biên soạn với mục đích cung cấp cho người đọc một cái nhìn tổng quan và chi tiết nhất về dự án trò chơi DUNGEON SEEKER. Đặc biệt, đối với quy trình phát triển với tư cách là một nhà phát triển độc lập, GDD đóng vai trò như một xương sống mạch lạc. Nó là một bản đồ hướng dẫn giúp tác giả, giảng viên hướng dẫn cũng như hội đồng đánh giá dự án có thể nắm bắt rõ ràng trò chơi từ ý tưởng khởi nguồn, cơ chế lối chơi (gameplay), thiết kế tính năng cho đến các yếu tố kỹ thuật.
Mục tiêu cuối cùng mong muốn đạt được của tài liệu là cụ thể hóa định hướng tầm nhìn của dự án. Bằng cách đi sâu vào chi tiết của từng thành phần cấu tạo nên game, tài liệu không chỉ giúp người phát triển bám sát vào cốt lõi ban đầu, tránh việc bị lệch hướng trong quá trình hình thành sản phẩm, mà qua đó còn tạo ra một nền tảng vững chắc để phân bổ quỹ thời gian, lên kế hoạch triển khai mã nguồn và quản lý tài nguyên dự án một cách tối ưu nhất.
Xác định phạm vi tài liệu
Tài liệu này là công cụ giám sát và chỉ đường đắc lực xuyên suốt quá trình thực hiện dự án. Nó giúp cá nhân người phát triển có định hướng rõ ràng, bám sát các lộ trình đã được đề ra cấu trúc từ trước; liền mạch từ khâu lên ý tưởng thiết kế, tiếp cận lập trình cho đến khi hoàn thiện sản phẩm.
Bên cạnh đó, trong bối cảnh phát triển độc lập, tài liệu GDD còn đóng vai trò như một hệ thống "quản lý dự án thu nhỏ", giúp người viết tự theo dõi tiến độ, đánh giá khối lượng công việc và linh hoạt điều chỉnh khi xảy ra lỗi phát sinh. Đồng thời, đây cũng là nguồn tài liệu hữu ích dành cho Giảng viên hướng dẫn, Hội đồng đánh giá, hay bất kỳ ai quan tâm đến quy trình phát triển trò chơi và mong muốn hiểu sâu hơn về kiến trúc của dự án DUNGEON SEEKER.
Nội dung chính của trò chơi (Elevator Pitch)
Trong DUNGEON SEEKER, người chơi sẽ hóa thân thành một nhà thám hiểm đơn độc mang trong mình nhiệm vụ chinh phục hệ thống hầm ngục tăm tối bất tận. Trò chơi kết hợp cơ chế suy luận logic kinh điển của "Dò mìn" (Minesweeper) vào địa hình lưới Isometric ngẫu nhiên (Roguelike). Tại đây, không có sự xuất hiện của những cuộc chiến chặt chém đẫm máu; thay vào đó, "vũ khí" duy nhất của người chơi là trí não. Họ phải liên tục thu thập dữ kiện từ mặt đất để phán đoán chính xác vị trí quái vật ẩn, định vị rương cứu viện và tìm ra lối thoát hiểm. Càng dấn thân sâu xuống các tầng ngục, sự sống còn càng bị đe dọa bởi giới hạn khắt khe của chỉ số thể lực (Lượt đi) và lượng máu ít ỏi, thúc đẩy người chơi bứt phá tư duy chiến thuật để sống sót và vén màn những bí mật bị chôn vùi dưới lòng đất.
Đối tượng người chơi hướng đến (Target Audience)
Tựa game hướng đến độ tuổi từ 12-16+ trở lên, những người yêu thích sự vận động của trí tuệ và tư duy chiến thuật. Thay vì những màn chiến đấu tốc độ cao, DUNGEON SEEKER tập trung vào trải nghiệm cân não, nơi mỗi bước đi đều mang tính quyết định đến sự sống còn. Dự án đặc biệt thu hút các nhóm đối tượng sau:
Người chơi yêu thích thể loại giải đố (Puzzle): Những người hâm mộ lối chơi suy luận logic kinh điển như Minesweeper nhưng mong muốn một lớp vỏ bọc thám hiểm hấp dẫn hơn.
Người chơi yêu thích Roguelike: Những người tìm kiếm trải nghiệm mới mẻ, không lặp lại sau mỗi lần chơi và chấp nhận sự trừng phạt khắt khe (chết là hết) để đổi lấy cảm giác chinh phục.
Người chơi yêu thích dòng game Indie chiến thuật: Những người đánh giá cao sự kết hợp độc đáo giữa các cơ chế game khác nhau.
Tổng quan về game
Nội dung và mục tiêu của game (Game concept)
Bối cảnh câu chuyện: Từ xa xưa, thế giới từng tồn tại những "Mê cung mê hoặc" nơi chứa đựng nguồn sức mạnh nguyên thủy vô tận. Tuy nhiên, một thảm họa ma pháp đã khiến những hầm ngục này bị biến đổi, trở thành những cái bẫy chết chóc đầy rẫy thực thể tà ác ẩn mình dưới những lớp sương mù dày đặc. Con người không thể nhìn thấy quái vật bằng mắt thường, và mọi sự dấn thân liều lĩnh đều kết thúc bằng cái chết. Suốt hàng thế kỷ, các hầm ngục được phong ấn, trôi vào quên lãng như một vùng đất cấm.
Đến kỷ nguyên hiện tại, các phong ấn bắt đầu suy yếu, ma lực từ hầm ngục rò rỉ và đe dọa sự yên bình của thế giới bên trên. Nhân vật chính là một hậu duệ của dòng tộc "Seekers" (Người tầm đạo) những người duy nhất sở hữu khả năng cảm nhận được sự hiện diện của ma lực thông qua các tần số năng lượng xung quanh (được thể hiện qua các con số chỉ số).
Mang trên vai trọng trách của một Seeker, cậu phải một mình dấn thân vào hầm ngục DUNGEON SEEKER để thanh tẩy những tầng sâu tối tăm nhất và ngăn chặn sự trỗi dậy của cõi u minh.
Mục tiêu chính: Mục tiêu cốt lõi là thám hiểm và chinh phục từng tầng của hầm ngục. Khác với những chiến binh thông thường, người chơi phải sử dụng tư duy logic để "giải mã" mê cung: dựa trên các con số gợi ý để phán đoán vị trí quái vật ẩn giấu, né tránh các cạm bẫy, thu thập rương vật phẩm cứu trợ và quan trọng nhất là tìm ra cầu thang để tiến xuống tầng tiếp theo. Nhiệm vụ cuối cùng là đánh bại những thực thể canh giữ ở tầng đáy để phong ấn vĩnh viễn nguồn cơn của thảm họa.
Trải nghiệm người chơi: Cảm xúc mà game mang lại là sự giao thoa giữa cảm giác thỏa mãn khi giải được một bài toán hóc búa và sự kịch tính, hồi hộp trong mỗi bước đi sinh tồn. Với yếu tố Roguelike, mỗi lần bước vào hầm ngục là một thử thách hoàn toàn mới với cấu trúc lưới thay đổi ngẫu nhiên, yêu cầu người chơi phải liên tục thích nghi. Hệ thống vật phẩm hỗ trợ như sách phép và bình thuốc không chỉ để hồi phục mà còn là công cụ để thay đổi chiến thuật, giúp người chơi lật ngược thế cờ khi đối mặt với những "điểm mù" của logic.

Thể loại của game (Genre)
Thể loại chính: Giải đố chiến thuật
Thể loại phụ: Roguelike, Dungeon Crawler
Game là sự kết hợp độc đáo giữa logic giải đố và yếu tố sinh tồn thám hiểm. Dưới đây là phân tích chi tiết về sự ảnh hưởng của các thể loại này đến gameplay:
Giải đố chiến thuật (Puzzle Strategy): Đây là linh hồn của trò chơi. Dựa trên cơ chế logic của Minesweeper, người chơi phải thu thập các con số chỉ dẫn tại các ô an toàn để suy luận ra vị trí của quái vật hoặc vật phẩm lân cận. Yếu tố này đòi hỏi người chơi phải có sự tính toán kỹ lưỡng, óc quan sát và khả năng loại trừ trước mỗi bước đi.
Roguelike: Mang lại giá trị chơi lại vô hạn cho dự án. Sau mỗi lần thất bại hoặc bắt đầu một vòng chơi mới (New Run), hầm ngục sẽ được tái cấu trúc hoàn toàn ngẫu nhiên (Procedural Generation). Yếu tố "Perma-death" (chết là hết) tạo ra áp lực tâm lý thực sự, khiến mỗi nước đi trong phần giải đố trở nên kịch tính và có sức nặng hơn.
Dungeon Crawler (Thám hiểm hầm ngục): Cấu trúc game được chia thành nhiều tầng (Floors) với góc nhìn Isometric truyền thống. Người chơi không chỉ giải đố mà còn phải quản lý tài nguyên (máu, năng lượng, lượt đi), khám phá các rương báu và tìm đường xuống tầng sâu hơn. Sự kết hợp này biến một bàn cờ giải đố tĩnh lặng thành một cuộc phiêu lưu sinh động.
Giải đố chiến thuật (Puzzle - Strategy RPG)
Vai trò trong game: Người chơi vào vai một "Seeker" (Người thám mã) với các hướng phát triển khác nhau (như tập trung vào sinh lực, năng lượng hoặc khả năng dò tìm). Nhiệm vụ là thám hiểm hầm ngục, thu thập vật phẩm và sử dụng trí tuệ để giải mã các tầng ngục đầy rẫy hiểm nguy.
Điểm mạnh:
Lối chơi đề cao tư duy logic và sự tính toán, tạo cảm giác thỏa mãn cực lớn khi lật mở được những ô an toàn trong tình thế hiểm nghèo.
Hệ thống phát triển nhân vật rõ ràng thông qua việc nâng cấp các chỉ số sinh tồn: Máu (HP), Năng lượng (Mana) và giới hạn Lượt đi (Steps).
Tận dụng vật phẩm và sách phép thuật một cách chiến lược để giải quyết các "nút thắt" trong logic (ví dụ: dùng phép để lật ô khi không thể suy luận).
Cốt truyện gắn liền với sự tiến hóa của nhân vật, mỗi tầng ngục chinh phục được là một bước tiến trong hành trình giải mã bí mật hầm ngục.
Điểm yếu:
Độ khó: Rất khó để cân bằng giữa sự ngẫu nhiên của bản đồ và khả năng giải đố của người chơi (dễ xảy ra tình huống "đoán mò" nếu bản đồ sinh ra quá hóc búa).
Tính kén người chơi: Lối chơi chậm và tính toán có thể không phù hợp với những người chơi ưa thích hành động tốc độ cao.
Logistics lập trình: Việc xây dựng thuật toán đảm bảo mọi bản đồ đều có thể giải được bằng logic là một thách thức lớn về mặt lập trình.
Cân bằng tài nguyên: Việc thiết kế sự tiêu hao Lượt đi sao cho vừa đủ thử thách mà không gây ức chế đòi hỏi quá trình thử nghiệm (Playtest) kỹ lưỡng.
Roguelike
Vai trò trong game: Mỗi tầng hầm ngục là một thử thách hoàn toàn mới với cấu trúc lưới và vị trí quái vật/vật phẩm được sinh ngẫu nhiên. Người chơi sẽ nhận được các vật phẩm hỗ trợ, sách phép hoặc các chỉ số bổ trợ (buff) ngẫu nhiên trong suốt hành trình, khiến không có hai lần chơi nào giống hệt nhau.
Điểm mạnh:
Giá trị chơi lại vô hạn: Việc thay đổi cấu trúc mê cung và vị trí các con số logic mỗi lần chơi giúp game luôn giữ được sự tươi mới, không gây nhàm chán.
Kích thích tư duy linh hoạt: Thay vì học thuộc lòng bản đồ, người chơi buộc phải rèn luyện kỹ năng suy luận logic để thích nghi với mọi tình huống ngẫu nhiên.
Khả năng mở rộng: Dễ dàng bổ sung thêm các loại quái vật mới, các hiệu ứng ô đặc biệt (ô bẫy, ô ẩn) hoặc các loại sách phép bổ trợ để làm phong phú nội dung.
Điểm yếu:
Áp lực tâm lý: Cơ chế mất hết tiến trình khi nhân vật tử nạn có thể gây ức chế cho người chơi nếu họ chưa quen với dòng game Roguelike.
Rủi ro từ sự ngẫu nhiên: Đôi khi sự ngẫu nhiên quá mức (RNG) có thể tạo ra những tầng ngục "không thể giải được", đòi hỏi thuật toán sinh map phải được tối ưu rất kỹ.
Khó kiểm soát độ khó: Việc cân bằng giữa vật phẩm hữu ích nhận được và độ dày đặc của quái vật trên bản đồ là một thử thách lớn trong thiết kế.

Tổng hợp thể loại và vai trò "DUNGEON SEEKER" là sự giao thoa tinh tế giữa thể loại Giải đố chiến thuật (Puzzle Strategy) và Thống trị hầm ngục (Roguelike). Điểm nhấn của trò chơi là hành trình đơn độc chinh phục những tầng ngục bí ẩn, nơi "trí tuệ là vũ khí duy nhất" để ngăn chặn sự trỗi dậy của các thế lực hắc ám.
Sự kết hợp này mang đến một trải nghiệm thám hiểm đầy trí tuệ, đòi hỏi sự kiên nhẫn và khả năng quản lý tài nguyên (máu, năng lượng, lượt đi) cực kỳ chặt chẽ.
Điểm mạnh tổng thể:
Trải nghiệm độc lạ: Sự kết hợp giữa logic "Dò mìn" và bối cảnh hầm ngục mang đến cảm giác sáng tạo, khác biệt hoàn toàn với các game hành động RPG thông thường.
Sự tự do trong chiến thuật: Tuy là game giải đố, người chơi vẫn có quyền tự do phát triển nhân vật theo hướng mình muốn (ưu tiên hồi phục, ưu tiên năng lượng phép thuật hay ưu tiên khả năng soi đường).
Tính thử thách và thỏa mãn: Mỗi ô gạch được lật mở an toàn là một bước thắng lợi của trí tuệ, mang lại cảm giác thành tựu lớn cho người chơi khi vượt qua được một tầng ngục khó.
Điểm yếu tổng thể:
Yêu cầu sự tập trung cao: Đây không phải là tựa game để chơi giải trí hời hợt; người chơi cần đầu tư thời gian, sự quan sát và khả năng ghi nhớ để có thể tiến sâu vào hầm ngục.
Áp lực khi trải nghiệm đơn độc: Do đặc thù là một mình thám hiểm (solo seeker), mọi sai lầm đều phải trả giá đắt mà không có sự trợ giúp, điểu này có thể tạo cảm giác căng thẳng liên tục cho một số đối tượng người chơi.
Bối cảnh của game
Dự án được xây dựng trong một thế giới Dark Fantasy u tối, là sự kết hợp nhuần nhuyễn giữa phong cách kiến trúc Trung cổ và các hầm ngục ma thuật kỳ ảo. Người chơi sẽ dấn thân vào hành trình vượt qua các tầng sâu của "Mê cung Vĩnh hằng" (The Eternal Grid), nơi mỗi tầng hầm ngục là một hệ thống lưới ma thuật phức tạp với phong cách bài trí riêng biệt, từ những tàn tích gạch đá rêu phong cho đến các căn phòng chứa đầy kho báu và cạm bẫy.
Thế giới trong game đặt trong bối cảnh vương quốc cổ đại bị lãng quên. Đây từng là trung tâm của nền văn minh ma pháp – nơi con người chế ngự quái vật bằng các "Phong ấn Logic" (Logic Seals) nằm sâu dưới lòng đất. Tuy nhiên, theo thời gian, các phong ấn này dần bị xói mòn, khiến các thực thể hắc ám bắt đầu thức tỉnh và lây lan ma lực ra khắp vương quốc. Tòa tháp hầm ngục – nơi từng là nhà tù giam giữ các thế lực này – giờ đây trỗi dậy và trở thành nguồn cơn của mọi hiểm họa.
Phong cách nghệ thuật của game mang đậm màu sắc Isometric Dark Fantasy, tạo nên một thế giới vừa kỳ ảo vừa đầy đe dọa. Màu sắc chủ đạo là các tông màu xanh trầm, xám và tím của đá đêm, kết hợp với các hiệu ứng ánh sáng động từ năng lượng của nhân vật và lớp sương mù che phủ các ô gạch chưa lật. Sự đối lập giữa vùng sáng (ô đã mở) và vùng tối (ô chưa mở) giúp nhấn mạnh cảm giác bất an và kích thích sự tò mò của người chơi trong hành trình thám hiểm. Người chơi sẽ thám hiểm qua những khu vực đặc trưng của hầm ngục: từ các Phòng Gác đầy quái vật ẩn mình, các Hành lang Ảo giác nơi các con số logic bị xáo trộn, cho đến những Mật thất chứa đựng các rương báu và sách phép cổ xưa. Tất cả đều là những "bài toán sinh tử" đòi hỏi sự tập trung tuyệt đối. Trong thế giới của DUNGEON SEEKER, chỉ những ai nắm vững quy luật của các con số và can đảm bước vào bóng tối mới có thể hy vọng tìm thấy lối thoát và mang lại ánh sáng cho vương quốc.
Cấu trúc, cách chơi của game
Game sử dụng cấu trúc phân tầng (Level-based Dungeon) người chơi sẽ chinh phục hầm ngục theo từng tầng từ trên xuống dưới. Mỗi tầng là một mê cung dạng lưới được sinh ngẫu nhiên, yêu cầu người chơi phải tìm ra ô "Cầu thang" để mở khóa tầng tiếp theo. Sự kết hợp giữa yếu tố ngẫu nhiên của mỗi tầng và độ khó tăng dần theo chiều sâu tạo nên một cảm giác thám hiểm bền bỉ, nơi mỗi bước đi đều tiến gần hơn đến bí mật cuối cùng của hầm ngục.
Góc nhìn Top-down Isometric: Trò chơi sử dụng góc nhìn nghiêng từ trên xuống, giúp người chơi có cái nhìn toàn cảnh về lưới bản đồ. Góc nhìn này đặc biệt quan trọng trong DUNGEON SEEKER, hỗ trợ người chơi quan sát các con số chỉ dẫn trên các ô gạch đã lật, từ đó lập chiến thuật di chuyển tối ưu, né tránh các ô có quái vật và xác định vị trí rương báu trong môi trường đầy rủi ro.
Lối chơi chính (Core Gameplay): Tập trung vào 3 yếu tố trụ cột.
Suy luận: Đây là cơ chế "Dò mìn" cải tiến. Người chơi mở các ô gạch để nhận dữ liệu số, từ đó phán đoán 8 ô xung quanh chứa bao nhiêu Quái vật, Rương báu hay Cầu thang.
Khám phá: Di chuyển nhân vật để lật mở từng vùng sương mù, tìm kiếm các lối đi ẩn hoặc các rương chứa trang bị quý giá giúp gia tăng khả năng sinh tồn.
Quản lý Tài nguyên: Người chơi phải cân đối giữa lượng Máu (HP) khi lỡ giẫm trúng quái, Năng lượng (Mana) để triển khai phép thuật tìm đường và đặc biệt là giới hạn Lượt đi (Steps) – thứ sẽ cạn kiệt nếu người chơi di chuyển quá dài mà không có tính toán.
Hệ thống Nhân vật (Classes): Người chơi có thể lựa chọn và phát triển các hướng đi khác nhau cho "Seeker" của mình.
Hộ vệ (The Guardian): Tập trung vào chỉ số HP cao, có khả năng chống chịu khi vô tình giẫm phải quái vật.
Trinh sát (The Scout): Ưu tiên độ linh hoạt và giới hạn Lượt đi lớn, giúp thám hiểm được những bản đồ rộng mà không sợ kiệt sức.
Học giả (The Scholar): Sử dụng Năng lượng ma thuật để triển khai các kỹ năng đặc biệt như: soi sáng một vùng bản đồ, tiêu diệt quái vật từ xa hoặc phát hiện cầu thang sớm.
Tương tác và Môi trường: Bên cạnh việc giải đố, người chơi có thể tương tác với các cổ vật hoặc NPC lãng du xuất hiện ngẫu nhiên trong hầm ngục để nhận nhiệm vụ, mua vật phẩm hồi phục hoặc lắng nghe những mảnh ghép về cốt truyện của vương quốc cổ đại. Các bản đồ được thiết kế với nhiều chủ đề đa dạng: từ sảnh đá lạnh lẽo, hầm mộ đầy rêu phong cho đến những khu vực bị ảnh hưởng bởi ma lực tím hắc ám, mỗi nơi đều đưa ra những thử thách logic và địa hình đòi hỏi người chơi phải vận dụng trí tuệ để vượt qua.
Nhân vật chính, số lượng người chơi
Nhân vật chính: Trong DUNGEON SEEKER, người chơi sẽ hóa thân vào vai một "Seeker" (Kẻ tầm đạo) một người trẻ tuổi mang trong mình dòng máu cổ xưa có khả năng tương tác với các phong ấn ma thuật.
Đặc điểm: Nhân vật chính không được xây dựng như một chiến binh dũng mãnh với sức mạnh cơ bắp, mà là một người sở hữu trí tuệ sắc bén và khả năng quan sát tinh tường.
Trình độ và Kỹ năng: Ban đầu, Seeker chỉ có những kiến thức cơ bản về cách đọc các chỉ số năng lượng trong hầm ngục. Thông qua quá trình thám hiểm, nhân vật sẽ dần học hỏi các phép thuật mới (thông qua sách phép), nâng cao khả năng chịu đựng và tối ưu hóa các bước di chuyển của mình.
Sứ mệnh: Mục tiêu duy nhất của nhân vật là giải mã toàn bộ các tầng ngục để tìm ra nguồn gốc của sự hỗn loạn và đóng lại cánh cổng dẫn đến cõi u minh, bảo vệ sự bình yên cho vương quốc.
Số lượng người chơi:
Chế độ chơi: Đơn người chơi (Single-player).
Lý do thiết kế: Trò chơi được thiết kế tập trung hoàn toàn vào trải nghiệm cá nhân. Việc thám hiểm đơn độc giúp nhấn mạnh cảm giác hồi hộp, kịch tính và yêu cầu sự tập trung tuyệt đối vào các bài toán logic. Người chơi phải tự mình đưa ra mọi quyết định chiến thuật, quản lý tài nguyên và chịu trách nhiệm cho mỗi bước đi của mình, từ đó tạo nên sự gắn kết sâu sắc giữa người chơi và hành trình trưởng thành của nhân vật chính.
Cách chơi, mục tiêu và sự tiến triển trong game
Mục tiêu chính và mục tiêu phụ trong game
Mục tiêu chính:
Chinh phục hầm ngục: Người chơi cần giải mã thành công các tầng lưới ma thuật để tìm ra lối xuống tầng sâu hơn.
Phát triển khả năng suy luận: Nâng cấp các chỉ số sinh tồn và kĩ năng hỗ trợ để có đủ khả năng đối phó với những tầng ngục có mật độ quái vật dày đặc và độ khó cao.
Mục tiêu cuối cùng: Khám phá bí ẩn tại tầng đáy của hầm ngục, đánh bại thực thể canh giữ cõi u minh để ngăn chặn nguồn ma lực tà ác đang rò rỉ, bảo vệ vương quốc khỏi sự sụp đổ.
Mục tiêu phụ:
Truy tìm kho báu: Thu thập tất cả các rương báu ẩn giấu trong mỗi tầng để sở hữu các vật phẩm hiếm, trang bị hỗ trợ và tài nguyên.
Tối ưu hóa hành trình: Cố gắng hoàn thành mỗi tầng ngục với số lượng "Lượt đi" (Steps) ít nhất có thể để nhận được các đánh giá xếp hạng cao hoặc phần thưởng bổ sung.
Khám phá cổ vật: Tìm kiếm các mảnh vỡ cốt truyện hoặc các nhiệm vụ ẩn từ NPC lãng du để hiểu rõ hơn về lịch sử của vương quốc và tòa tháp.
Hệ thống Phần thưởng:
Tài nguyên & Kinh nghiệm: Tiền tệ (dùng để mua vật phẩm/nâng cấp) và điểm kinh nghiệm (EXP) để thăng cấp nhân vật.
Vật phẩm cứu trợ: Bình phục máu (HP), bình hồi năng lượng (Mana) và các vật phẩm tăng lượt đi.
Trang bị & Sách phép: Các trang bị giúp gia tăng chỉ số vĩnh viễn hoặc các cuốn sách phép cho phép thực thi kĩ năng đặc biệt (soi đường, diệt quái từ xa).
Sự tiến triển trong game
Sự tiến triển trong DUNGEON SEEKER được thiết kế theo mô hình vòng lặp Roguelike kết hợp với sự tăng trưởng chỉ số của nhân vật, đảm bảo người chơi luôn cảm thấy bản thân mạnh mẽ và nhạy bén hơn sau mỗi lần thám hiểm.
Hành trình theo chiều sâu (Floor Progression):
Độ khó tăng tiến: Càng đi sâu vào hầm ngục, kích thước của lưới (Grid) sẽ càng mở rộng. Mật độ quái vật dày đặc hơn và các con số gợi ý sẽ trở nên phức tạp hơn, đòi hỏi khả năng tư duy logic và loại trừ cao độ.
Môi trường biến đổi: Mỗi mốc tầng (ví dụ: sau mỗi 5 tầng) sẽ có sự thay đổi về phong cách kiến trúc, màu sắc và các loại cạm bẫy mới, tránh cảm giác nhàm chán và tạo động lực khám phá.
Phát triển Nhân vật (Character Growth):
Phát triển Nhân vật (Character Growth): Hệ thống Cấp độ: Thông qua việc lật mở các ô an toàn và tiêu diệt quái vật (bằng phép thuật), nhân vật tích lũy kinh nghiệm để lên cấp. Mỗi khi thăng cấp, người chơi được lựa chọn nâng cấp các chỉ số cơ bản như: Tăng HP tối đa (để sống sót lâu hơn), Tăng Mana tối đa (để dùng nhiều kĩ năng hơn), hoặc Tăng giới hạn Lượt đi (để thám hiểm rộng hơn).
Trang bị và Kỹ năng: Người chơi sẽ tìm thấy các trang bị mới trong rương báu giúp gia tăng chỉ số vĩnh viễn trong lượt chơi đó. Các sách phép thuật tìm được sẽ mở khóa những kĩ năng mới, giúp người chơi có thêm nhiều công cụ để giải quyết các "nút thắt" logic khó.
Vòng lặp Học tập và Tích lũy (Roguelite elements):
Kinh nghiệm người chơi: Đây là yếu tố quan trọng nhất. Sau mỗi lần tử nạn, người chơi không chỉ mất đi tiến trình mà còn nhận lại sự am hiểu về quy luật của hầm ngục và cách sử dụng các con số gợi ý hiệu quả hơn.
Nâng cấp vĩnh viễn (nếu có): Sau mỗi lượt thám hiểm (Run), một phần tài nguyên thu thập được có thể được dùng để nâng cấp các chỉ số nền tảng tại "Sảnh chờ", giúp nhân vật khởi đầu những lượt chơi tiếp theo thuận lợi hơn.
Khám phá Cốt truyện (Narrative Progression):
Câu chuyện về vương quốc và tòa tháp không được kể ngay lập tức mà được hé lộ dần qua các mảnh tàn tích gặp được ở các tầng sâu. Việc muốn tìm hiểu "điều gì thực sự đang diễn ra ở tầng cuối cùng" là động lực chính thúc đẩy sự tiến triển của người chơi.

Sự tiến triển của câu chuyện trong game
Mạch truyện trong DUNGEON SEEKER được dẫn dắt theo phong cách phi tuyến tính, cho phép người chơi làm chủ hoàn toàn tốc độ và cách thức khám phá của mình:
Sự tự do trong thám hiểm: Ngay khi bắt đầu, người chơi được đưa vào lưới hầm ngục và có quyền tự do di chuyển đến bất kỳ ô gạch nào trong tầm mắt. Bạn có thể chọn cách tiếp cận thận trọng bằng việc lật mở từng ô an toàn để tích lũy vật phẩm, hoặc mạo hiểm bứt phá để tìm ra con đường ngắn nhất. Mọi hành động đều do người chơi quyết định dựa trên khả năng quan sát và suy luận của chính mình.
Chinh phục và Mở khóa: Mỗi tầng ngục được thiết kế như một chướng ngại vật lớn. Mục tiêu cốt lõi là tìm ra lối thoát và đánh bại thực thể canh giữ (Boss) của tầng đó để mở lời nguyền, kích hoạt cổng dịch chuyển dẫn xuống tầng tiếp theo. Tuy nhiên, thay vì vội vàng bước qua, người chơi có thể lựa chọn nán lại để khai phá toàn bộ các ô gạch, tìm kiếm lối đi ẩn hoặc thu thập đủ trang bị nhằm đảm bảo sự chuẩn bị tốt nhất trước khi đối đầu với những thử thách khắc nghiệt hơn ở phía dưới.
Lối chơi không gò bó: Tất cả các hoạt động như thu thập cổ vật, giải đố phụ hay thực hiện nhiệm vụ từ NPC lãng du đều có thể thực hiện bất kỳ lúc nào. Game không đặt ra áp lực về thời gian thực, cho phép người chơi dừng lại suy nghĩ, phân tích các con số gợi ý và đưa ra phương án tối ưu nhất. Sự tiến triển của câu chuyện phụ thuộc hoàn toàn vào chiến lược và sự kiên nhẫn của người chơi.
Đích đến cuối cùng: Hành trình chính yếu tập trung vào việc vượt qua Tầng 1 (Hành lang tăm tối) và hạ gục thủ lĩnh hắc ám tại Tầng 2 (Sảnh đường linh hồn). Từ những người thích "tốc chiến tốc thắng" đến những người muốn trở thành bậc thầy "vét sạch" mọi bí mật trong hầm ngục.
Cân bằng độ khó của game
Việc cân bằng độ khó trong DUNGEON SEEKER được thực hiện thông qua sự giao thoa giữa thuật toán sinh bản đồ ngẫu nhiên và hệ thống quản lý tài nguyên của người chơi, nhằm đảm bảo game luôn có tính thử thách nhưng không gây ức chế:
Tiến trình độ khó theo tầng (Scaling Difficulty):
Mật độ thực thể: Tại các tầng đầu, tỉ lệ quái vật ẩn sẽ thấp và các con số gợi ý đơn giản. Càng xuống sâu, tỉ lệ quái vật sẽ tăng dần, đồng thời các ô gạch có địa hình đặc biệt (ô chặn tầm nhìn, ô bẫy làm mất nhiều lượt đi) sẽ xuất hiện thường xuyên hơn.
Kích thước bản đồ: Lưới Isometric sẽ mở rộng dần, yêu cầu người chơi phải quản lý "Lượt đi" (Steps) khắt khe hơn để có thể khám phá hết bản đồ mà không bị kiệt sức.
Thuật toán kiểm soát tính logic (Fairness Logic):
Đảm bảo khả năng giải được: Hệ thống sinh map được thiết kế để hạn chế tối đa các tình huống "50/50" (nơi người chơi buộc phải đoán mò mà không có dữ kiện logic).
Vùng an toàn khởi đầu: Mỗi màn chơi luôn bắt đầu tại một cụm ô an toàn đã lật sẵn, cung cấp đủ dữ kiện ban đầu để người chơi triển khai các bước suy luận tiếp theo.
Cơ chế cứu viện thông qua Vật phẩm (Item-based Balancing):
Khi người chơi gặp những "điểm mù" logic (các ô không thể suy luận chắc chắn bằng số), các vật phẩm như Sách phép (soi sáng vùng lân cận) hoặc Bình thuốc (hồi máu sau khi giẫm phải quái) đóng vai trò là công cụ cân bằng, giúp người chơi vượt qua những tình huống ngặt nghèo nhờ vào sự chuẩn bị vật phẩm từ trước.

Sự trừng phạt và Thưởng (Risk vs. Reward):
Quản lý HP và Steps: Việc giẫm trúng quái vật chỉ trừ một lượng HP nhất định thay vì kết thúc game ngay lập tức. Điều này cho phép người chơi có sai số nhất định. Tuy nhiên, nếu di chuyển không tính toán dẫn đến hết "Lượt đi", hình phạt sẽ nặng nề hơn (trừ HP theo mỗi bước), buộc người chơi luôn phải cân nhắc giữa việc "khám phá thêm lấy đồ" hay "tiến thẳng xuống tầng tiếp theo để an toàn".
Vòng lặp Roguelite:
Sau mỗi lượt chơi (Run), dù thắng hay thua, người chơi đều tích lũy được một lượng tài nguyên nhất định. Tài nguyên này có thể dùng để nâng cấp vĩnh viễn các chỉ số cơ bản cho nhân vật, giúp những lần thám hiểm sau trở nên "dễ thở" hơn, tạo ra tiến trình phát triển liên tục cho người chơi.
Quy tắc và cơ chế vận hành của game
Quy tắc Khám phá (Discovery Rules):
Màn sương vô định: Khi bắt đầu mỗi tầng, toàn bộ bản đồ (trừ khu vực xuất phát) đều bị che phủ bởi sương mù. Người chơi chỉ có thể biết được nội dung của một ô gạch khi nhân vật trực tiếp di chuyển bước lên ô đó.
Cơ chế Số chỉ dẫn: Khi bước vào một ô an toàn, ô đó sẽ hiển thị 3 chỉ số tương ứng với số lượng: Quái vật, Rương báu, và Cầu thang nằm trong phạm vi 8 ô bao quanh. Người chơi sử dụng các con số này để lập luận và đánh dấu các ô nguy hiểm hoặc ô có mục tiêu.
Quy tắc Di chuyển và Tài nguyên (Movement & Resource Rules):
Giới hạn Lượt đi (Steps): Mỗi bước di chuyển sang một ô kề cạnh sẽ tiêu tốn 1 đơn vị Lượt đi (thể hiện qua biểu tượng Đồng hồ cát).
Trạng thái Kiệt sức: Nếu Lượt đi về bằng 0, nhân vật sẽ rơi vào trạng thái kiệt sức. Lúc này, mỗi bước di chuyển tiếp theo sẽ trừ trực tiếp vào lượng Máu (HP) của nhân vật.
Quản lý Năng lượng (Mana): Sử dụng các kỹ năng đặc biệt (như soi đường từ xa) sẽ tiêu tốn Năng lượng. Năng lượng không tự phục hồi mà cần thông qua vật phẩm hoặc các ô đặc biệt trên bản đồ.
Quy tắc Tương tác và Trừng phạt (Interaction & Penalty Rules):
Va chạm Quái vật: Nếu người chơi bước nhầm vào ô chứa Quái vật, một lượng HP sẽ bị trừ ngay lập tức dựa trên độ mạnh của tầng đó. Quái vật sau khi bị giẫm trúng sẽ biến mất (hoặc ô đó trở thành ô trống).
Thu thập Vật phẩm: Khi bước vào ô chứa Rương, vật phẩm sẽ tự động được thêm vào kho đồ hoặc kích hoạt hiệu ứng hồi phục ngay lập tức.
Kích hoạt Cầu thang: Chỉ khi đứng trên ô chứa Cầu thang, người chơi mới có thể chọn lệnh "Xuống tầng tiếp theo".
Điều kiện Kết thúc (End-game Conditions):
Thắng lợi (Victory): Người chơi vượt qua tất cả các tầng hầm ngục và đánh bại được thực thể canh giữ cuối cùng ở tầng đáy.
Thất bại (Defeat): Lượng Máu (HP) của nhân vật trở về 0. Theo quy tắc Roguelike, người chơi sẽ mất đi toàn bộ vật phẩm và tiến trình của lượt chơi đó, quay trở về điểm khởi đầu để bắt đầu một hành trình mới.
Hệ thống đồ họa và âm thanh
Hệ thống hình âm của DUNGEON SEEKER được thiết kế nhằm mục đích tái hiện một không gian hầm ngục đầy huyền bí, u ám nhưng không kém phần sống động, giúp người chơi đắm chìm vào trải nghiệm giải đố sinh tồn.
Hệ thống đồ họa (Graphics)
Môi trường và Lưới (Grid & Environment): Hệ thống các ô gạch (Tiles) được thiết kế đồng bộ theo chủ đề Dark Fantasy. Các ô gạch có sự phân biệt rõ ràng giữa trạng thái "Chưa lật" (phủ sương mù/fog) và "Đã lật" (hiển thị con số hoặc vật phẩm).
Nhân vật và thực thể: Nhân vật chính (Seeker) và các thực thể như Quái vật, Rương báu, Cầu thang được thiết kế dưới dạng Sprite 2D chất lượng cao, có các hoạt ảnh (Animation) cơ bản như đi bộ, đứng chờ (Idle) và hiệu ứng khi va chạm.
Giao diện người dùng (UI/UX): Sử dụng bộ biểu tượng (Icons) đặc trưng để biểu thị các chỉ số sinh tồn:
Trái tim: Đại diện cho Sinh lực (HP).
Mặt trăng khuyết: Đại diện cho Năng lượng ma thuật (Mana).
Đồng hồ cát: Đại diện cho Lượt đi còn lại (Steps).
Hiệu ứng thị giác (VFX): Các hiệu ứng hạt (Particles) nhẹ nhàng như sương mù bay lơ lửng, ánh sáng phát ra từ các rương báu hoặc hiệu ứng mặt đất rung chuyển khi lật mở các ô gạch quan trọng.
Hệ thống âm thanh (Audio)
Âm thanh trong game đóng vai trò quan trọng trong việc tạo ra bầu không khí căng thẳng và hồi hộp:
Nhạc nền (BGM): Sử dụng các bản nhạc mang phong cách Dark Ambient hoặc Lounge Game để tạo nhịp điệu thám hiểm vừa thư thái vừa ẩn chứa sự nguy hiểm.
Âm thanh tương tác: Tiếng báo hiệu chính xác (Correct sound) khi người chơi suy luận đúng hoặc âm thanh nổ/cảnh báo khi giẫm phải quái vật.
Âm thanh phản hồi: Sử dụng các kỹ năng đặc biệt (như soi đường từ xa) sẽ tiêu tốn Năng lượng. Năng lượng không tự phục hồi mà cần thông qua vật phẩm hoặc các ô đặc biệt trên bản đồ.
Hệ thống Voice & Radio: Tận dụng các thư mục voice và sound_radio hiện có để lồng ghép các đoạn thoại ngắn của nhân vật hoặc tiếng thì thầm của hệ thống, giúp dẫn dắt cốt truyện một cách tự nhiên và đầy ám ảnh.

Cốt truyện và nhân vật
Cốt truyện (The Story of the Seeker)
Khởi đầu: Vương quốc Solaryn từng là một biểu tượng của sự phồn thịnh, nơi ma thuật và logic được tôn thờ như những vị thần song hành. Tuy nhiên, sự tham vọng của các pháp sư cổ đại đã vô tình mở ra một khe nứt đến cõi u minh, hình thành nên "Mê cung Vĩnh hằng" (Abyssal Grid) ngay dưới lòng kinh thành. Để ngăn chặn thảm họa, các bậc hiền triết đã hi sinh để đặt lên hầm ngục những "Phong ấn Logic" biến nơi đây thành một bàn cờ của các con số, nơi chỉ những ai có trí tuệ mới có thể đi qua an toàn.
Diễn biến: Theo thời gian, những bức tường ngăn cách giữa thế giới thực và hầm ngục dần rạn nứt. Ma lực hắc ám bắt đầu rò rỉ, biến thành những quái vật ẩn mình trong sương mù. Nhân vật chính một hậu duệ cuối cùng của hội "Seekers" bước vào hầm ngục với nhiệm vụ tái lập các phong ấn đã mất. Qua mỗi tầng ngục, người chơi sẽ tìm thấy các mảnh nhật ký cũ, hé lộ về sự phản bội của những người canh giữ và nguồn gốc thực sự của tòa tháp.
Kết thúc: Hành trình dẫn lối cho nhân vật chính đến tận cùng của sự thật, nơi cậu phải đối mặt với thực thể mang tên "Sự Hỗn Loạn Của Các Con Số". Việc đánh bại thực thể này không chỉ là giải một bài toán cuối cùng, mà còn là hành động cứu rỗi linh hồn của vương quốc Solaryn khỏi sự tan biến vĩnh viễn.
Hệ thống nhân vật (Characters)
Nhân vật chính: The Seeker (Kẻ tầm đạo)
Vai trò: Là nhân vật duy nhất người chơi điều khiển xuyên suốt hành trình.
Đặc điểm: Một thiếu niên với đôi mắt có khả năng nhìn thấu các dòng chảy ma thuật (giải thích cho việc tại sao nhân vật thấy được các con số gợi ý). Nhân vật không sử dụng vũ khí truyền thống mà dùng các "Cuộn Sách Ma Thuật" (Magic Scrolls) để tương tác với thế giới xung quanh.
Sự phát triển: Từ một người tập sự còn non nớt, Seeker sẽ dần trở thành một bậc thầy thám mã, người có thể chế ngự được mọi cạm bẫy phức tạp nhất của hầm ngục.
Các Thực thể Hắc ám (The Corrupted Entities)
Quái vật cấp thấp (The Glitchers): Những linh hồn bị tha hóa, ẩn mình dưới lớp sương mù. Chúng không tấn công trực diện mà đóng vai trò như những quả mìn sống, chờ đợi sự sai lầm trong bước đi của người chơi.
Kẻ canh giữ (The Gatekeepers): Xuất hiện tại cuối mỗi tầng hoặc các vị trí then chốt. Chúng sở hữu các cơ chế tấn công đặc biệt, thay đổi cả quy luật của bàn cờ logic (ví dụ: làm ẩn đi các con số gợi ý), buộc người chơi phải vận dụng tối đa các kỹ năng đã học.
Nhân vật phụ (Supporting Characters)
NPC Thương nhân ẩn dật (The Mysterious Merchant): Một linh hồn lãng du thường xuất hiện tại các tầng lẻ. Ông ta cung cấp các vật phẩm hồi phục, trang bị hiếm và đôi khi là những manh mối về tầng ngục tiếp theo để đổi lấy tài nguyên thu thập được.
Sứ giả của ánh sáng (The Oracle): Chỉ hiện thân thông qua tiếng vọng hoặc các bức tượng cổ, hướng dẫn Seeker cách sử dụng các loại phép thuật cổ xưa.
Chi tiết về thế giới game và cấp độ
	Thế giới của DUNGEON SEEKER không phải là một mặt phẳng rộng mở mà là một hệ thống đa tầng đầy biến động, nơi mỗi cấp độ (Level) là một thử thách logic riêng biệt.
Cấu trúc cấp độ (Level Structure)
Hệ thống cấp độ được thiết kế theo hình thực "Cây hạ tầng", người chơi sẽ tiến dần từ tầng trên cùng xuống các tầng sâu dưới lòng đất:
Bố cục lưới (Grid Layout): Mỗi cấp độ được xây dựng trên một lưới Isometric ngẫu nhiên. Kích thước lưới sẽ tăng tiến theo từng chặng (ví dụ: Tầng 1-5 là 8x8, Tầng 6-10 là 12x12).
Điểm khởi đầu (Spawn Point): Luôn là một vùng an toàn (safe clusters) gồm 3-5 ô gạch đã được lật sẵn để người chơi có dữ kiện bắt đầu suy luận.
Cầu thang xuống tầng (The Stairs): Có duy nhất một ô gạch chứa cầu thang trong mỗi màn. Đây là mục tiêu tối thượng mà người chơi phải tìm ra để vượt qua cấp độ.
Phân khu môi trường (Environment Themes)
Dù diễn ra trong tòa tháp hầm ngục, thế giới game vẫn được chia thành các khu vực có phong cách nghệ thuật và đặc trưng riêng:
Khu vực 1: Hành lang Thạch nhũ (The Stone Corridors):
Đặc điểm: Kiến trúc bằng đá thô sơ, ánh sáng đuốc xanh mờ ảo.
Độ khó: Thấp, là nơi người chơi làm quen với các con số logic 1, 2, 3 và cách quản lý tài nguyên.
Khu vực 2: Sảnh đường Linh hồn (The Soul Halls):
Đặc điểm: Kiến trúc gothic lộng lẫy nhưng tàn tích, thảm và rèm rách nát, sương mù tím dày đặc.
Độ khó: Trung bình, bắt đầu xuất hiện các ô bẫy làm giảm lượt đi (Steps) nhanh hơn hoặc các ô bị kẹt không thể mở bằng cách thông thường.
Khu vực 3: Đáy vực Bí ẩn (The Abyssal Bottom):
Đặc điểm: Không gian mở rộng lớn trong lòng đất, nơi các ô gạch lơ lửng trên hư không.
Độ khó: Cao nhất, mật độ quái vật cực dày, đòi hỏi kết hợp nhuần nhuyễn giữa suy luận và các vật phẩm phép thuật để sống sót.
Các thực thể tương tác trong cấp độ
Trong mỗi màn chơi, ngoài các ô thông thường, người chơi sẽ bắt gặp:
Ô Rương báu (Loot Tiles): Nơi chứa trang bị, máu và mana. Tỉ lệ xuất hiện rương hiếm sẽ tăng dần ở các tầng sâu.
Ô Đền thờ (Shrines): Các ô đặc biệt cho phép hồi phục hoàn toàn một loại tài nguyên hoặc ban phước năng lượng tạm thời cho nhân vật.
Ô Chặn (Obstacles): Các vật cản như cột đá, hố sụt không thể bước vào, buộc người chơi phải đi đường vòng và tốn thêm nhiều lượt đi.
Sự tiến triển độ khó (Difficulty Progression)
Hệ thống sẽ tự động cân bằng độ khó dựa trên tiến trình của người chơi:
Monster Density: Tỉ lệ quái vật/tổng số ô sẽ tăng dần (ví dụ từ 10% lên 20%).
Logic Complexity: Các con số gợi ý sẽ có thiên hướng xuất hiện tại những vị trí hiểm hóc, tạo ra nhiều "ngã rẽ" suy luận khiến người chơi phải cân não lựa chọn.
Tối ưu hóa gameplay, Kế hoạch phát triển và Phát hành
Tối ưu hóa gameplay và Quản lý độ khó
Để đảm bảo DUNGEON SEEKER luôn giữ được sức hút và sự cân bằng, dự án tập trung vào các giải pháp tối ưu sau:
Tối ưu hóa phản hồi (Game Feel): Đảm bảo tính lồng ghép giữa hành động di chuyển nhân vật và việc lật mở ô gạch diễn ra mượt mà (không có độ trễ). Các hiệu ứng âm thanh và rung màn hình nhẹ khi lật mở đúng/sai giúp tăng cảm giác thỏa mãn cho người chơi.
Hệ thống Dynamic Difficulty (Độ khó năng động): Nếu người chơi vượt qua 3 tầng liên tiếp mà không mất HP, mật độ quái vật sẽ tăng nhẹ. Ngược lại, nếu người chơi thất bại quá nhiều, hệ thống sẽ ưu tiên sinh ra nhiều rương hồi phục hơn ở lượt chơi tiếp theo.
Kiểm soát rủi ro logic: Thuật toán sinh màn chơi được tinh chỉnh để đảm bảo luôn có ít nhất một lối đi hoàn toàn có thể suy luận được (solvable) mà không cần dùng đến vật phẩm hỗ trợ.
Kế hoạch phát triển (Development Roadmap)
Dự án được chia làm 4 giai đoạn chính để đảm bảo tiến độ đồ án tốt nghiệp:
Giai đoạn Prototype (Tuần 1-2): Xây dựng hệ thống lưới Isometric, logic số Minesweeper cơ bản và di chuyển nhân vật.
Giai đoạn Alpha (Tuần 3-4): Hoàn thiện hệ thống tài nguyên (HP, Mana, Steps), kho đồ và các loại thực thể cơ bản (Monster, Chest, Stairs).
Giai đoạn Beta (Tuần 5-6): Đưa vào hệ thống kỹ năng/sách phép, đa dạng hóa các tầng ngục (Themes) và tinh chỉnh thuật toán sinh map ngẫu nhiên.
Giai đoạn Polish (Tuần 7-Kết thúc): Tối ưu hóa hiệu năng, chỉnh sửa lỗi (debug), hoàn thiện hệ thống âm thanh và giao diện (UI) cuối cùng.
Kiểm thử và Bảo đảm chất lượng (QA & Testing)
Kiểm thử logic (Unit Testing): Kiểm tra tính chính xác của thuật toán tính toán các con số gợi ý lân cận để tránh sai sót gây ức chế cho người chơi.
Kiểm thử cân bằng (Playtesting): Tổ chức các đợt chơi thử nghiệm nhỏ để thu thập dữ liệu về lượng HP/Steps tiêu hao trung bình, từ đó điều chỉnh chỉ số sao cho game "vừa đủ khó".
Kiểm thử kịch bản (Edge-case Testing): Kiểm tra các trường hợp đặc biệt như: hết Lượt đi ngay khi đang đứng trên ô Rương, hoặc sử dụng sách phép tại các ô biên của bản đồ.
Kế hoạch phát hành và Tiếp cận thị trường
Dù là dự án tốt nghiệp, DUNGEON SEEKER vẫn được định hướng để tiếp cận người dùng thực tế:
Nền tảng phát hành: Ưu tiên phát hành trên Itch.io (nền tảng game indie lớn nhất thế giới) để nhận phản hồi từ cộng đồng.
Chiến dịch quảng bá: Xây dựng Trailer giới thiệu gameplay đặc trưng, chia sẻ quá trình phát triển (Devlog) lên các hội nhóm lập trình game tại Việt Nam và quốc tế.
Tầm nhìn mở rộng: Sau khi bảo vệ thành công đồ án,có thể được phát triển thêm các chế độ chơi mới như Chế độ Thử thách thời gian (Time Attack) hoặc Bảng xếp hạng trực tuyến để tăng tính cạnh tranh.
PHẦN 2 – TÀI LIỆU KỸ THUẬT
Phân tích ý tưởng game (SWOT Analysis)
Việc đánh giá dự án thông qua mô hình SWOT giúp xác định rõ các yếu tố thuận lợi và thách thức của DUNGEON SEEKER:
Điểm mạnh (Strengths):
Ý tưởng độc đáo: Sự kết hợp sáng tạo giữa logic "Dò mìn" kinh điển và thám hiểm hầm ngục "Roguelike" tạo nên nét thu hút riêng biệt, không bị bão hòa bởi các game hành động RPG thông thường.
Cơ chế cốt lõi rõ ràng: Minesweeper là một cơ chế đã được chứng minh về tính gây nghiện, giúp giảm thiểu rủi ro trong việc thiết kế gameplay quá phức tạp.
Tính cá nhân hóa: Việc solo giúp đảm bảo tính đồng nhất tuyệt đối về tầm nhìn nghệ thuật và logic từ đầu đến cuối dự án.
Công cụ mạnh mẽ: Sử dụng Unity Engine với hệ thống Tilemap nâng cao, giúp tối ưu hóa việc xây dựng lưới Isometric và quản lý dữ liệu hiệu quả.
Điểm yếu (Weaknesses):
Nguồn lực hạn chế: Dự án hiện chỉ còn một mình đảm nhiệm toàn bộ từ Code, Sprites đến Tài liệu (Solo-dev).
Áp lực thời gian: Thời hạn đồ án tốt nghiệp cố định, đòi hỏi việc ưu tiên hoàn thiện tính năng cốt lõi và cắt bỏ những tính năng rườm rà.
Kinh nghiệm thực tế: Chưa có nhiều trải nghiệm trong việc phát phát hành và vận hành một sản phẩm hoàn chỉnh trên thị trường.

Cơ hội (Opportunities):
Cơ hội (Opportunities): Xu hướng game Indie: Người chơi hiện nay đang rất đón nhận các tựa game nhỏ có lối chơi "vắt óc" suy luận và mang tính trí tuệ cao.
Nền tảng phát hành rộng mở: Các trang như Itch.io hay Steam tạo điều kiện cực tốt cho các nhà phát triển độc lập tiếp cận người dùng toàn cầu.
Cộng đồng hỗ trợ: Lượng tài liệu và thư viện hỗ trợ cho Unity cực kỳ phong phú, giúp giải quyết nhanh các vấn đề kỹ thuật.
Thách thức (Threats):
Độ khó trong thuật toán: Việc viết code đảm bảo mọi map sinh ra đều giải được bằng logic là một thách thức kỹ thuật lớn đối với một cá nhân.
Cân bằng tài nguyên: Nếu không thiết kế kỹ chỉ số HP/Steps, game dễ bị rơi vào trạng thái quá khó hoặc quá dễ, làm mất đi tính hấp dẫn của Roguelike.
Vấn đề hiệu năng: Tối ưu hóa việc hiển thị hàng trăm ô gạch cùng lúc trên các thiết bị cấu hình thấp.
Thiết kế Game (Sơ đồ Game Loop)
Viết tài liệu mô tả chi tiết toàn bộ thiết kế game:

Hình 2.2-1: Sơ đồ game loop
Mô tả chi tiết Luồng vận hành (Game Loop Description)
Vòng lặp cốt lõi: Người chơi bắt đầu hành trình bằng việc lựa chọn kĩ năng khởi đầu. Mỗi tầng ngục yêu cầu người chơi phải cân não giải mã các ô gạch để tìm ra lối thoát.
Cơ chế Thử thách - Phản hồi: Nếu người chơi phán đoán sai (giẫm trúng quái vật), HP sẽ giảm. Nếu giải mã đúng, người chơi sẽ nhận được EXP và tài nguyên để nâng cấp sức mạnh, chuẩn bị cho các màn chơi khó hơn.
Kết thúc lượt chơi: Game kết thúc khi người chơi chinh phục được tầng đáy hoặc nhân vật tử nạn. Điểm kinh nghiệm tích lũy sẽ được dùng để mở khóa các nâng cấp vĩnh viễn (Rogue-lite).
Hệ thống Chức năng (Core Features)
Hệ thống Grid Isometric (Lưới Isometric):
Quản lý danh sách các ô gạch và trạng thái của chúng (Fog, Revealed, Flagged). 
Tự động tính toán các chỉ số gợi ý (Monsters/Items count) dựa trên 8 ô lân cận.
Hệ thống Di chuyển & Thám thính (Movement & Reveal):
Nhân vật di chuyển sang các ô kề cạnh. Mỗi bước đi tiêu tốn 1 "Step". 
Khi nhân vật đứng lên ô mới, hệ thống sẽ thực hiện lệnh "Reveal" để mở nội dung ô đó.
Hệ thống Quản lý Tài nguyên (Resource Management):
Quản lý thanh máu (HP), năng lượng (Mana) và lượt đi (Steps). 
Tính toán sát thương khi giẫm phải bẫy/quái vật.
Hệ thống Kho đồ & Kỹ năng (Inventory & Spellbook):
Lưu trữ các vật phẩm nhặt được từ rương (Bình thuốc, Sách phép). 
Cơ chế sử dụng vật phẩm để tác động trực tiếp lên bản đồ (Soi đường, Diệt quái).
Sơ đồ lớp (Class Diagram - Sơ lược)
Trong Unity, dự án sẽ được tổ chức theo các Manager chính:
MapManager: Chịu trách nhiệm sinh bản đồ ngẫu nhiên. 
PlayerController: Xử lý di chuyển và trừ tài nguyên. 
UIManager: Hiển thị các chỉ số HP/Mana và thông tin tầng. 
GameController: Quản lý trạng thái Thắng/Thua và chuyển màn.
Phân tách trạng thái và Cấu trúc màn chơi (System Architecture)
Trạng thái Game (Game States): Hệ thống vận hành thông qua các trạng thái chuyển đổi logic sau:
Start / Splash: Khởi động và hiển thị logo nhà phát triển. 
Main Menu: Màn hình chính (Bắt đầu, Cài đặt, Thoát). 
Character Select: Lựa chọn lớp nhân vật (Guardian / Scout / Scholar). 
Load Dungeon: Khởi tạo lưới Isometric và đặt các thực thể ngẫu nhiên. 
Playing: Trạng thái chính khi người chơi đang điều khiển nhân vật thám hiểm. 
Pause: Tạm dừng game để điều chỉnh các thông số hoặc xem hướng dẫn. 
GameOver / Win: Hiển thị kết quả thắng/thua của lượt chơi (Run). 
Result / Upgrade: Màn hình tổng kết và nâng cấp vĩnh viễn trước khi quay lại Menu.
Danh sách các Scene (Scene Management): Mặc dù game sinh map ngẫu nhiên, nhưng cấu trúc Scene trong Unity được chia như sau:
Scene_Splash: Giới thiệu ban đầu.
Scene_MainMenu: Giao diện điều hướng chính. 
Scene_Selection: Lựa chọn lớp nhân vật và kĩ năng. 
Scene_GameWorld: Scene chứa logic sinh tầng (Dungeon Generator). Toàn bộ 2 tầng chơi chính hoặc các tầng vô tận sẽ diễn ra tại đây nhưng với dữ liệu Grid khác nhau. 
Scene_GameOver: Hiển thị kết quả và tích lũy điểm.
Đặc trưng Kỹ thuật (Technical Summary)
Thể loại: Giải đố nhập vai (Puzzle RPG) kết hợp Roguelike thám hiểm.
Góc nhìn: Isometric Top-down (Nghiêng từ trên xuống). Camera được cố định hoặc di chuyển mượt mà theo nhân vật chính, đảm bảo quan sát được tối đa lưới bản đồ.
Đồ họa: 2D Sprite/Tilemap trên không gian giả 3D (Z-sorting) để tạo độ sâu hầm ngục.
Nền tảng: Windows (PC).
Chi tiết Tài nguyên Dự án (Project Assets)
Tài nguyên Đồ họa (Graphics Assets):
Nhân vật chính:
The Seeker: Thiết kế theo phong cách phù thủy/giả tưởng (với 3 hướng phát triển: Guardian, Scout, Scholar). Có đầy đủ hoạt ảnh: Di chuyển (Walk), Nghỉ (Idle), Thi triển phép (Cast Spell), và Gục ngã (Die).
Kẻ địch (Enemies):
Low-level: Slime (Quái vật bùn), Glitcher (Bóng ma nhiễu loạn), Corrupted Eye (Mắt tà thuật).
Mid-level: Skeleton Mage (Xương pháp sư), Wraith (Linh hồn lãng du).
Boss: The Abyssal Lord (Chúa tể vực thẳm - xuất hiện tầng cuối).
Vật phẩm (Items):
Bình Potion: Hồi HP (Trái tim) và Hồi Mana (Mặt trăng).
Sách phép (Spellbooks): Sách lửa (Explosion), Sách ánh sáng (Reveal).
Trang bị: Giày cũ (Old Shoes - tăng steps), Nhẫn ma pháp.
Môi trường (Environment):
Góc nhìn Isometric: Tường đá, cổng sắt, đuốc treo tường, lồng đèn cổ, hầm ngục nhiều tầng, hố sụt ma thuật.
Hiệu ứng (VFX):
Particle System cho các ô gạch khi lật, hào quang xung quanh nhân vật khi lượm đồ, hiệu ứng sương mù Fog of War.
Thiết kế UI/UX (Interface Design):
Màn hình chính (Main Menu): Logo Isometric 2D, Play, Settings (Cài đặt âm thanh), Quit.
Giao diện chơi game (HUD):
Thanh HP (Dãy trái tim), Thanh Mana (Dãy mặt trăng).
Counter Steps (Đồng hồ cát): 0/30.
Entity Counter: Bảng số lượng Quái/Rương/Cầu thang còn lại trong tầng. 
Popup: Bảng nâng cấp kỹ năng khi lên cấp, bảng thông báo Game Over/Level Clear.
Chi tiết Thiết kế Gameplay & Âm thanh
Thiết kế Gameplay:
Luật chơi: Lật mở các ô gạch để suy luận số lượng quái xung quanh. Người chơi phải tìm ra lối xuống tầng (Stairs) trước khi hết Lượt đi (Steps) hoặc hết Máu (HP). 
Cấp độ: Mỗi tầng tháp là một thử thách logic riêng biệt. Độ khó tăng dần qua diện tích màn chơi và mật độ quái vật. 
Tiến trình: Người chơi có thể lựa chọn đánh nhanh thắng nhanh (tìm cầu thang sớm) hoặc thám hiểm vét sạch (tìm rương vật phẩm) để tăng sức mạnh cho tầng sau.

Thiết kế Âm thanh (Audio Design):
BGM (Nhạc nền):
Normal Mode: Nhạc huyền bí, nhẹ nhàng.
Warning Mode (Khi HP thấp hoặc gần quái): Nhạc dồn dập, căng thẳng hơn.
SFX (Hiệu ứng): Tiếng bước chân trên đá, tiếng lật gạch (Grid click), tiếng mở rương, tiếng nổ phép thuật, tiếng cảnh báo rùng rợn khi giẫm trúng quái.
Voice: Các đoạn âm thanh ngắn (grunts/whispers) của nhân vật khi bị thương hoặc khi phát hiện ra kịch bản bí ẩn.


Công nghệ & hệ thống
Tài liệu mô tả nền tảng kỹ thuật sử dụng:
Công nghệ/Engine:

Hình 2.3-1: Icon Unity.

Hình 2.3-2: Ngôn ngữ C#.
Unity Engine: Dự án sử dụng cụ thể phiên bản 6000.3.10f1
Ngôn ngữ lập trình: C#
Công cụ hổ trợ:

Hình 2.3-3: Icon Google Antigravity.
Git / GitHub Desktop
Unity Package Manager

Hình 2.3-4: Icon phần mềm Github Desktop.
Yêu cầu hệ thống:
Cấu hình tối thiếu CPU i5, Ram 8G, GPU GTX 1050, Unity có thể build được các nền tảng:
Windows (.exe): Có thể hỗ trợ tốt, tốc độ build trung bình.
WebGL: Có thể build, nhưng quá trình export khá nặng cấu hình thấp.

