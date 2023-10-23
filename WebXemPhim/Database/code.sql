USE master
GO
DROP DATABASE IF EXISTS WebXemPhim;
GO
CREATE DATABASE WebXemPhim
GO
USE WebXemPhim
GO
CREATE TABLE Users(
	UserName VARCHAR(50) PRIMARY KEY,
	UserPassword VARCHAR(100) NOT NULL,
    UserRole BIT DEFAULT 0
);
GO
CREATE TABLE Genres(
	GenreId INT PRIMARY KEY IDENTITY(1,1),
	GenreName NVARCHAR(30) NOT NULL UNIQUE
);
GO

CREATE TABLE Movies (
    MovieId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
	Content NTEXT,
    ReleaseDay DATE ,
	DirectorName NVARCHAR(50) NOT NULL,
	CountryName NVARCHAR(50) NOT NULL,
    GenreId INT,
    ImageLink TEXT, 
    Directory TEXT,
    MovieStatus BIT DEFAULT 0,
	CONSTRAINT FK_Movies_Genres FOREIGN KEY (GenreId) REFERENCES Genres(GenreId) ON DELETE CASCADE
);
GO
CREATE TABLE MovieActors (
	MovieActorsId INT PRIMARY KEY IDENTITY(1,1),
    MovieId INT,
    ActorName NVARCHAR(50),
    CONSTRAINT FK_MovieActors_Movies FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE
);
GO
CREATE TABLE UserRatings (
    UserRatingsId INT PRIMARY KEY IDENTITY(1,1),
    Rating INT NOT NULL,
    UserName VARCHAR(50),
    MovieId INT,
    CONSTRAINT FK_UserRatings_Users FOREIGN KEY (UserName) REFERENCES Users(UserName) ON DELETE CASCADE,
    CONSTRAINT FK_UserRatings_Movies FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE
);
GO
CREATE TABLE FavoriteMovies (
	FavoriteMoviesId INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(50),
    MovieId INT,
    CONSTRAINT FK_FavoriteMovies_Users FOREIGN KEY (UserName) REFERENCES Users(UserName) ON DELETE CASCADE,
    CONSTRAINT FK_FavoriteMovies_Movies FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE
);
GO
CREATE TABLE MovieViews (
	MovieViewsId INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(50),
    MovieId INT,
	TimeView DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_MovieViews_Users FOREIGN KEY (UserName) REFERENCES Users(UserName) ON DELETE CASCADE,
    CONSTRAINT FK_MovieViews_Movies FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE
);
GO

INSERT INTO Users (UserName, UserPassword, UserRole)
VALUES ('admin', '123', 1);
INSERT INTO Users (UserName, UserPassword, UserRole)
VALUES ('user', '123', 0);

-- Chèn 10 thể loại bằng tiếng Việt vào bảng Genres
INSERT INTO Genres (GenreName)
VALUES (N'Hành động');

INSERT INTO Genres (GenreName)
VALUES (N'Hài hước');

INSERT INTO Genres (GenreName)
VALUES (N'Khoa học viễn tưởng');

INSERT INTO Genres (GenreName)
VALUES (N'Tình cảm');

INSERT INTO Genres (GenreName)
VALUES (N'Kinh dị');

INSERT INTO Genres (GenreName)
VALUES (N'Phiêu lưu');

INSERT INTO Genres (GenreName)
VALUES (N'Hoạt hình');

INSERT INTO Genres (GenreName)
VALUES (N'Tài liệu');


INSERT INTO Movies(Title, Content, ReleaseDay, ImageLink, Directory, MovieStatus, DirectorName, CountryName, GenreId)
VALUES
    (N'Già thiên', N'Mở đầu với hình tượng 9 con rồng kéo quan tài xuất hiện ở núi Thái Sơn, rồi khi nó bay vào không gian vũ trụ đã mang theo những người bạn học cùng lớp, chính vào thời điểm bọn họ tụ hội ôn lại chuyện xưa.', '2023-01-15', 'https://dragonphim.tv/upload/product/2023_03_05_10_04_42-hhninja-gia-thien_250x350.jpg', 'https://www.youtube.com/embed/rX4qWFZggJ0?si=FJDCqwajr4yo7758', 1, N'Nguyễn Văn A', N'Trung Quốc', 1),
    (N'Thế giới hoàn mỹ', N'Thế Giới Hoàn Mỹ cải biên từ tiểu thuyết cùng tên. Hắn vì tu đạo mà sinh, vì ứng kiếp mà đến. Hắn hoá thân thành vô vàn mưa máu, vẩy rơi năm tháng vạn cổ, trải qua vô số tu luyện của thời không và thử thách của dòng chảy tháng năm. Hắn hoá thành vạn cổ, hoá thành tự tại. Xem nam chính Thạch Hạo làm thế nào đạt đến huy hoàng đỉnh cao một đời, tạo ra truyền thuyết vô tận.', '2023-02-20', 'https://i.ytimg.com/vi/V0iC4gzYFZk/maxresdefault.jpg', 'https://www.youtube.com/embed/8vhLVSOj2j8?si=cjMhDMfL6aabb_i2', 1, N'Huy Nguyễn', N'Trung Quốc', 1),
    (N'Đấu La Đại Lục', N'Đệ tử ngoại môn Đường Tam của Đường Môn, vì học trộm bí kíp của nội môn mà bị Đường Môn bài xích, Đường Tam nhảy xuống vực để tỏ rõ ý chí nhưng không chết mà còn được bước vào một thế giới khác với một thân khác, một thế giới thuộc võ về được - là gọi Đại đấu La Lục. Nơi này không có ma pháp, không có đấu khí, không có võ thuật, nhưng lại có võ hồn kỳ thần. Ở nơi này, vào lúc 6 tuổi, đều sẽ ở trong điện võ hồn lệnh võ hồn thức tỉnh. Võ hồn có động vật, có thực vật, có thể đồ vật, chúng có phụ trợ mọi người sinh hoạt hằng ngày. Đó là một vài võ hồn biệt biệt xuất sắc có thể dùng để tu luyện đồng thời có thể tiến hành chiến đấu, nghiệp chức này trên Đấu La Đại là chức vụ Lục nhất nghiệp mạnh mẽ, cũng là vinh quang nhất —— Linh sư.', '2023-03-25', 'https://play-lh.googleusercontent.com/yvMsoDiXA6jkUFsW5HLQi54rhkufgMMJte4i2o7D7FuznzLIsmyEzUsweZgDfipzMg', 'https://www.youtube.com/embed/324vfLaJr60?si=IRDiBFFB83wCMt97', 1, N'Huy Nguyễn', N'Huy Nguyễn', 1),
    (N'Đấu phá thương khung', N'Đấu Phá Thương Khung kể về Tiêu Viêm là một võ giả trẻ có năng khiếu dị bẩm. Năm 9 tuổi, mẹ cậu bị truy sát đến chết, cha cậu giấu bặt chuyện này, Tiêu Viêm gìn giữ chiếc nhẫn mẹ để lại như báu vật không bao giờ rời. Công lực của Tiêu Viêm mãi đến năm 15 tuổi vẫn chưa có tiến triển, gia tộc có hôn ước với cậu từ lâu cũng đến để thoái hôn, khiến Tiêu gia phải chịu nỗi nhục to lớn. Tiêu Viêm một lần tình cờ đánh thức được chủ nhân chiếc nhẫn là Dược Trần lão nhân', '2023-05-03', 'https://img.meta.com.vn/Data/image/2021/11/01/lich-chieu-dau-pha-khung-thuong-al.jpg', 'https://www.youtube.com/embed/3P0e4d18CzU?si=Amwn5ngmrTld7zMZ', 1, N'Huy Nguyễn', N'Trung Quốc', 2),
	(N'Vĩnh Sinh', N'Kể từ khi còn trẻ, người hầu hạ đẳng Fang Han đã có niềm tin rằng "thà làm kẻ ăn mày còn hơn làm đầy tớ cho người khác." Với niềm tin này trong tâm trí, và dựa vào sức mạnh của chính mình, anh ấy đã có thể càn quét khắp thế giới. Với một tinh thần kiên cường, anh nắm vững nghệ thuật thần thánh và rèn luyện một cơ thể vĩnh cửu, bước từng bước vào vương quốc của những người bất tử trước khi đạt đến đỉnh cao.', '2023-05-03', 'https://i.ytimg.com/vi/OmAw87gKU0c/maxresdefault.jpg', 'https://www.youtube.com/embed/MsVJ-V9Fr8Y?si=bwqzbQipbdSKva_k', 1, N'Nguyễn Văn C', N'Trung Quốc', 2),
	(N'Đại chúa tể', N'Đại thiên thế giới, nơi các vị diện giao nhau, vạn tộc hiện lên như nấm, quần hùng tụ hội, một vị Thiên chi chí tôn đến từ hạ vị diện tại vô tận thế giới diễn lại một truyền kỳ mà mọi người hướng tới, theo đuổi con đường Chúa tể.', '2023-05-03', 'https://i.ytimg.com/vi/Cw-n6MYPB-8/maxresdefault.jpg', 'https://www.youtube.com/embed/sFnStYOlYTc?si=GcwA39-icmR1Ox-D', 1, N'Nguyễn Văn C', N'Trung Quốc', 2),
	(N'Tinh thần biến', N'Tinh Thần Biến là một tiểu thuyết võ hiệp có nội dung về một câu Truyện Tiên Hiệp hoành tráng kể về người thanh niên Tần Vũ gian khổ tu luyện, vượt hết khó khăn này đến nguy hiểm khác để lên Thần Giới tìm người yêu.', '2023-05-03', 'https://i.ytimg.com/vi/Ef5JsWWdWcc/maxresdefault.jpg', 'https://www.youtube.com/embed/eySrp71Gyys?si=zRxpBQgDOCa43eJq', 1, N'Nguyễn Văn C', N'Trung Quốc', 3),
	(N'Thôn phệ tinh không', N'La Phong cũng như bao người cùng lứa, mang trong mình những ước mơ, hoài bão lớn lao với ý chí, niềm đam mê mãnh liệt để vươn tới đỉnh cao trong cuộc sống và rồi do những xui rủi của số phận và tạo ngộ của lịch sử mà chàng trai đã mang đến những kỳ tích cho nhân loại', '2023-05-03', 'https://i.ytimg.com/vi/jr-vQ4IZboo/maxresdefault.jpg', 'https://www.youtube.com/embed/UWV-4y-1fjg?si=DgZ1TQa9aicAe3r0', 1, N'Nguyễn Văn C', N'Trung Quốc', 3),
	(N'Vũ Canh Kỷ', N' Nghịch Thiên Chi Quyết là một bộ phim hoạt hình đầy màu sắc và hấp dẫn. Bộ phim xoay quanh cuộc phiêu lưu của nhân vật chính Vũ Canh Kỷ trong việc tìm kiếm sự truyền thừa của gia tộc.', '2023-05-03', 'https://i.ytimg.com/vi/mV7k6naj9mE/maxresdefault.jpg', 'https://www.youtube.com/embed/LSizeipq2YY?si=b8QepSTL5OBAyp2F', 1, N'Nguyễn Văn C', N'Trung Quốc', 3),
	(N'Tuyết Ưng Lĩnh Chủ', N'Ở tỉnh An Dương của đế quốc, có một lãnh địa quý tộc rất nhỏ và tầm thường, gọi là—— Tuyết Ưng Lĩnh!', '2023-05-03', 'https://i.ytimg.com/vi/yFjchU_riHo/maxresdefault.jpg', 'https://www.youtube.com/embed/mThVDm-y2ik?si=avJUa6M-f1XkmIcm', 1, N'Nguyễn Văn C', N'Trung Quốc', 4),
	(N'Duy Ngã Độc Thần', N'Trong vùng đất rộng lớn của Trung Quốc, các vị thần cai quản quy luật của trời đất đã chết và mất đi ranh giới pháp lý của họ. Các thế lực bóng tối sẽ di chuyển, cố gắng lật đổ toàn bộ vùng đất. Ning Chen là một lính canh bình thường trong cung điện Canglan.', '2023-05-03', 'https://i.ytimg.com/vi/L8z1bh7DSlM/maxresdefault.jpg?sqp=-oaymwEmCIAKENAF8quKqQMa8AEB-AHUBoAC4AOKAgwIABABGC8gRyh_MA8=&rs=AOn4CLC7Oy3u0u20ZzF4nBvHJIev6sDSJQ', 'https://www.youtube.com/embed/JX72vnXncnM?si=dZMUT8jM7cNsBgMp', 1, N'Nguyễn Văn C', N'Trung Quốc', 4),
	(N'Nghịch Thiên Tà Thần', N'Nhất đại thiên tài huyền mạch bị hao tổn trở thành phế vật, gia tộc vứt bỏ, thế nhân chế giễu, thậm chí đêm tân hôn bị người độc hại...... Huyền Thiên chí bảo, luân hồi kính hiện, nghịch thiên cải mệnh, khởi động lại nhân sinh, mang theo cừu hận cùng tiếc nuối, thề phải đăng đỉnh lực lượng đỉnh phong!', '2023-05-03', 'https://i.ytimg.com/vi/L3DQfXYKxIE/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLAXxEXYa38uezIHAmpHZS97JpNeEQ', 'https://www.youtube.com/embed/netxJpEzdtI?si=rrsDYAXxzgbDw5JB', 1, N'Nguyễn Văn C', N'Trung Quốc', 4),
	(N'Vô thượng thần đế', N'Tiên Vương Mục Vân bởi vì nắm giữ Tru Tiên Đồ mà bị kẻ khác ám toán, tàn hồn ngủ say vạn năm sau, thức tỉnh ở trên người "Phế vật Mục Vân" tại Nam Vân đế quốc Thiên Vận đại lục.', '2023-05-03', 'https://i.ytimg.com/vi/NX9r9k_j8cw/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLAROJGaXe5iHwl1QKamuGNi85hxZQ', 'https://www.youtube.com/embed/f5d7hDiY1j4?si=nl4h35VxAAg-WtQO', 1, N'Nguyễn Văn C', N'Trung Quốc', 5),
	(N'Hồng Hoang Linh Tôn', N'Phim xoay quanh Hồng Hoang Linh Tôn, một vị thần với sức mạnh vô song và trí tuệ tuyệt vời. Anh ta đã được giao nhiệm vụ bảo vệ vũ trụ khỏi những thế lực tà ác và đối mặt với những thử thách nguy hiểm.', '2023-05-03', 'https://i.ytimg.com/vi/GX3AkIwrdHA/hqdefault.jpg', 'https://www.youtube.com/embed/hmF3IvYZDH8?si=V4Ag3pxLCozauShU', 1, N'Nguyễn Văn C', N'Trung Quốc', 5),
	(N'Vạn Giới Độc Tôn', N'Phim xoay quanh nhân vật chính tên là Lâm Phong, vào lúc ngưng tụ vũ hồn thì bỗng nhiên vị hôn thê Cơ Mạn Yêu đã nhân lúc cướp lấy vũ hồn của hắn. Ngay sau khi bị chính vị hôn thê của mình tập kích.', '2023-05-03', 'https://i.ytimg.com/vi/d1Hl5VAYBDE/maxresdefault.jpg', 'https://www.youtube.com/embed/d1Hl5VAYBDE?si=5I0VtLuK5gDa_vv3', 1, N'Nguyễn Văn C', N'Trung Quốc', 5)