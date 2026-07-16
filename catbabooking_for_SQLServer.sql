-- ============================================================
-- CatBaBooking Database - SQL Server Version
-- Chuyển đổi từ MySQL dump (catbabooking_for_MySQL.sql)
-- ============================================================
-- Các thay đổi chính khi chuyển MySQL -> SQL Server:
--   AUTO_INCREMENT        -> IDENTITY(1,1)
--                           SET IDENTITY_INSERT ON/OFF để nạp data có ID cụ thể
--   ENUM(...)             -> NVARCHAR(n) + CHECK CONSTRAINT
--   tinyint(1) (boolean)  -> BIT
--   varchar/text utf8mb4  -> NVARCHAR  (Unicode tích hợp sẵn)
--   DEFAULT CURRENT_TIMESTAMP -> DEFAULT GETDATE()
--   ON UPDATE CURRENT_TIMESTAMP -> AFTER UPDATE trigger riêng
--   ENGINE=InnoDB / COLLATE / CHARACTER SET -> bỏ (không dùng ở SQL Server)
--   Backtick (``) identifiers -> bỏ
--   ON DELETE RESTRICT    -> ON DELETE NO ACTION (mặc định SQL Server)
--   UNIQUE KEY            -> CONSTRAINT UQ_... UNIQUE
--   DROP TABLE IF EXISTS  -> IF OBJECT_ID(...) IS NOT NULL DROP TABLE
--   KEY (index)           -> CREATE INDEX (hoặc bỏ - SQL Server tự tạo trên PK/FK)
--   LOCK/UNLOCK TABLES    -> bỏ
--   /*!...*/ MySQL directives -> bỏ
--   COMMENT trên cột      -> comment SQL thường
-- ============================================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'catbabooking')
BEGIN
    CREATE DATABASE catbabooking
        COLLATE Vietnamese_CI_AS;
END
GO

USE catbabooking;
GO

-- Tắt tạm kiểm tra constraint để drop/create không lỗi FK
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
GO

-- ============================================================
-- XÓA BẢNG NẾU ĐÃ TỒN TẠI (thứ tự ngược FK)
-- ============================================================
IF OBJECT_ID('temp_carts',              'U') IS NOT NULL DROP TABLE temp_carts;
IF OBJECT_ID('table_availability',      'U') IS NOT NULL DROP TABLE table_availability;
IF OBJECT_ID('room_images',             'U') IS NOT NULL DROP TABLE room_images;
IF OBJECT_ID('room_availability',       'U') IS NOT NULL DROP TABLE room_availability;
IF OBJECT_ID('roles_features',          'U') IS NOT NULL DROP TABLE roles_features;
IF OBJECT_ID('reviews',                 'U') IS NOT NULL DROP TABLE reviews;
IF OBJECT_ID('payments',                'U') IS NOT NULL DROP TABLE payments;
IF OBJECT_ID('booking_dishes',          'U') IS NOT NULL DROP TABLE booking_dishes;
IF OBJECT_ID('booked_tables',           'U') IS NOT NULL DROP TABLE booked_tables;
IF OBJECT_ID('booked_rooms',            'U') IS NOT NULL DROP TABLE booked_rooms;
IF OBJECT_ID('bookings',                'U') IS NOT NULL DROP TABLE bookings;
IF OBJECT_ID('dishes',                  'U') IS NOT NULL DROP TABLE dishes;
IF OBJECT_ID('dish_categories',         'U') IS NOT NULL DROP TABLE dish_categories;
IF OBJECT_ID('restaurant_tables',       'U') IS NOT NULL DROP TABLE restaurant_tables;
IF OBJECT_ID('business_restaurant_types','U') IS NOT NULL DROP TABLE business_restaurant_types;
IF OBJECT_ID('business_occasions',      'U') IS NOT NULL DROP TABLE business_occasions;
IF OBJECT_ID('business_cuisines',       'U') IS NOT NULL DROP TABLE business_cuisines;
IF OBJECT_ID('business_amenities',      'U') IS NOT NULL DROP TABLE business_amenities;
IF OBJECT_ID('rooms',                   'U') IS NOT NULL DROP TABLE rooms;
IF OBJECT_ID('businesses',              'U') IS NOT NULL DROP TABLE businesses;
IF OBJECT_ID('users',                   'U') IS NOT NULL DROP TABLE users;
IF OBJECT_ID('roles',                   'U') IS NOT NULL DROP TABLE roles;
IF OBJECT_ID('features',                'U') IS NOT NULL DROP TABLE features;
IF OBJECT_ID('restaurant_types',        'U') IS NOT NULL DROP TABLE restaurant_types;
IF OBJECT_ID('occasions',               'U') IS NOT NULL DROP TABLE occasions;
IF OBJECT_ID('cuisine_types',           'U') IS NOT NULL DROP TABLE cuisine_types;
IF OBJECT_ID('amenities',               'U') IS NOT NULL DROP TABLE amenities;
IF OBJECT_ID('areas',                   'U') IS NOT NULL DROP TABLE areas;
GO

-- ============================================================
-- Table: amenities
-- ============================================================
CREATE TABLE amenities (
    amenity_id  INT           NOT NULL IDENTITY(1,1),
    name        NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_amenities PRIMARY KEY (amenity_id),
    CONSTRAINT UQ_amenities_name UNIQUE (name)
);
GO

SET IDENTITY_INSERT amenities ON;
INSERT INTO amenities (amenity_id, name) VALUES
(2, N'Bãi đỗ xe miễn phí'),
(4, N'Bữa sáng'),
(6, N'Hồ bơi ngoài trời'),
(3, N'Máy lạnh'),
(1, N'WiFi miễn phí'),
(5, N'Xe đưa đón sân bay');
SET IDENTITY_INSERT amenities OFF;
GO

-- ============================================================
-- Table: areas
-- ============================================================
CREATE TABLE areas (
    area_id INT           NOT NULL IDENTITY(1,1),
    name    NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_areas PRIMARY KEY (area_id),
    CONSTRAINT UQ_areas_name UNIQUE (name)
);
GO

SET IDENTITY_INSERT areas ON;
INSERT INTO areas (area_id, name) VALUES
(5, N'Bến Bèo'),
(4, N'Làng Việt Hải'),
(2, N'Trung tâm Cát Bà'),
(1, N'TT.Cát Bà'),
(3, N'Vườn quốc gia Cát Bà');
SET IDENTITY_INSERT areas OFF;
GO

-- ============================================================
-- Table: roles
-- ============================================================
CREATE TABLE roles (
    role_id     INT           NOT NULL IDENTITY(1,1),
    role_name   NVARCHAR(50)  NOT NULL,
    description NVARCHAR(255) NULL,
    created_at  DATETIME2     NULL DEFAULT GETDATE(),
    CONSTRAINT PK_roles PRIMARY KEY (role_id),
    CONSTRAINT UQ_roles_role_name UNIQUE (role_name)
);
GO

SET IDENTITY_INSERT roles ON;
INSERT INTO roles (role_id, role_name, description, created_at) VALUES
(1, N'customer',         N'Khách hàng đặt homestay/nhà hàng', '2025-10-02 09:58:17'),
(2, N'owner homestay',   N'Chủ homestay',                      '2025-10-02 09:58:17'),
(3, N'admin',            N'Quản trị viên hệ thống',            '2025-10-02 09:58:17'),
(4, N'owner restaurant', N'Chủ nhà hàng',                      '2025-10-02 09:58:17');
SET IDENTITY_INSERT roles OFF;
GO

-- ============================================================
-- Table: users
-- MySQL: enum('active','pending','rejected') -> NVARCHAR(10) + CHECK
-- MySQL: timestamp ON UPDATE CURRENT_TIMESTAMP -> AFTER UPDATE trigger
-- ============================================================
CREATE TABLE users (
    user_id          INT           NOT NULL IDENTITY(1,1),
    role_id          INT           NOT NULL,
    full_name        NVARCHAR(255) NOT NULL,
    email            NVARCHAR(255) NOT NULL,
    password_hash    NVARCHAR(255) NOT NULL,
    phone            NVARCHAR(20)  NULL,
    citizen_id       NVARCHAR(50)  NULL,
    personal_address NVARCHAR(500) NULL,
    status           NVARCHAR(10)  NOT NULL DEFAULT 'active',
    created_at       DATETIME2     NULL DEFAULT GETDATE(),
    updated_at       DATETIME2     NULL DEFAULT GETDATE(),
    CONSTRAINT PK_users          PRIMARY KEY (user_id),
    CONSTRAINT UQ_users_email    UNIQUE (email),
    CONSTRAINT CK_users_status   CHECK (status IN ('active','pending','rejected')),
    CONSTRAINT FK_users_role     FOREIGN KEY (role_id) REFERENCES roles(role_id)
);
GO

-- Trigger tự cập nhật updated_at
CREATE OR ALTER TRIGGER trg_users_updated_at
ON users AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE u SET u.updated_at = GETDATE()
    FROM users u INNER JOIN inserted i ON u.user_id = i.user_id;
END;
GO

SET IDENTITY_INSERT users ON;
INSERT INTO users (user_id,role_id,full_name,email,password_hash,phone,citizen_id,personal_address,status,created_at,updated_at) VALUES
(1, 3,N'Admin System',       N'catbabooking.fms@gmail.com',        N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',NULL,NULL,NULL,N'active','2025-10-02 09:58:45','2025-10-02 09:58:45'),
(2, 1,N'Đào Văn Năng',       N'nangdvhe187101@fpt.edu.vn',         N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',NULL,NULL,NULL,N'active','2025-10-02 09:59:53','2025-10-13 15:27:46'),
(3, 1,N'abcc',               N'hieu2406nh@gmail.com',              N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',NULL,NULL,NULL,N'active','2025-10-10 03:40:59','2025-10-10 03:40:59'),
(4, 1,N'Nguyễn Trung Hiếu',  N'hieu2406ny@gmail.com',              N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',NULL,NULL,NULL,N'active','2025-10-10 03:48:18','2025-10-10 03:48:18'),
(6, 2,N'Nguyễn Trung Hiếu',  N'Hieunthe187126@fpt.edu.vn',         N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0796034672',N'012345678910',N'phú thọ',N'active','2025-10-10 03:52:38','2025-10-13 15:25:27'),
(7, 2,N'nguyen van a',        N'aacsasdc@gmail.com',                N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'01223412334',N'122324234234',N'aaaa',N'active','2025-10-14 06:02:54','2025-10-14 06:03:44'),
(8, 4,N'Phạm Vy',             N'phamthituongvy30112004@gmail.com',  N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987654323',N'035204338866',N'Hà Nam',N'active','2025-10-21 12:58:53','2025-11-12 18:34:53'),
(9, 2,N'Ninh Hong',           N'ninhhong@gmail.com',                N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'09876412123',N'123456789012',N'hà nội',N'active','2025-10-15 01:14:36','2025-10-15 01:25:56'),
(10,2,N'Hoàng Thị Lan',      N'lan@gmail.com',                     N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'09874124142',N'091234123123',N'Cao Bằng',N'active','2025-10-15 01:19:10','2025-10-15 01:26:03'),
(11,2,N'nguyen van b',        N'chub@gmail.com',                    N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0124124112',N'1231231231231',N'350 Hà Sen, thị trấn Cát Bà',N'active','2025-10-15 01:21:32','2025-10-15 01:26:10'),
(12,2,N'Jerry',               N'Jerry@gmail.com',                   N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'01248124124',N'1231283129783',N'Cát Bà',N'rejected','2025-10-15 01:23:35','2025-10-31 00:54:31'),
(13,2,N'nguy van an',         N'GreenHomestay@gmail.com',           N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987645112',N'8989723423423',N'Cát Bà',N'rejected','2025-10-15 01:25:26','2025-10-31 00:54:34'),
(14,2,N'Lý Thị Kiều',        N'lythikieu@gmail.com',               N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987654333',N'035204338810',N'Cát Bà',N'active','2025-10-29 19:36:55','2025-10-31 04:38:03'),
(17,2,N'Lý Thị Thảo',        N'lythithao@gmail.com',               N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'098765412',N'035204338800',N'Cát Bà',N'active','2025-10-31 04:41:37','2025-11-14 19:07:25'),
(18,4,N'Nguyễn Ngọc Bảo',    N'nguyenngocbao@gmail.com',           N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987654328',N'035204338886',N'Hà Nam',N'active','2025-10-31 04:44:08','2025-11-14 17:17:41'),
(19,1,N'Nguyễn Nhật Minh',   N'nangdz14@gmail.com',                N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',NULL,NULL,NULL,N'active','2025-11-06 21:23:46','2025-11-06 21:23:46'),
(20,4,N'Nguyễn Huy Thiệp',   N'nguyenhuythiep@gmail.com',          N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987686868',N'035862807777',N'Hà Tĩnh',N'active','2025-11-07 03:42:13','2025-11-12 22:34:42'),
(21,4,N'Nguyễn Huy Đức',     N'nguyenhuyduc@gmail.com',            N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987171717',N'035862801111',N'Hà Nội',N'active','2025-11-10 14:00:48','2025-11-14 17:54:26'),
(22,4,N'Nguyễn Huy',         N'nguyenhuy@gmail.com',               N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987654321',N'035204338844',N'Hà Nội',N'active','2025-11-12 17:49:57','2025-11-14 17:35:30'),
(23,4,N'Nguyễn Toản',        N'nguyentoan@gmail.com',              N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987654338',N'035204338989',N'Cát Bà',N'active','2025-11-14 18:23:26','2025-11-14 18:24:13'),
(24,2,N'Nguyễn Xuân',        N'nguyenxuan@gmail.com',              N'$2b$12$AG4WSc.J/kA9PfZbP0ohb.l02QYDqglSheW61c88eMvguikA7depC',N'0987654391',N'035204338989',N'Cát Bà',N'active','2025-11-14 19:26:07','2025-11-14 19:26:58');
SET IDENTITY_INSERT users OFF;
GO

-- ============================================================
-- Table: businesses
-- enum('homestay','restaurant') -> NVARCHAR(20) + CHECK
-- enum('active','pending','rejected') -> NVARCHAR(10) + CHECK
-- ============================================================
CREATE TABLE businesses (
    business_id     INT           NOT NULL IDENTITY(1,1),
    owner_id        INT           NOT NULL,
    name            NVARCHAR(255) NOT NULL,
    type            NVARCHAR(20)  NOT NULL,
    address         NVARCHAR(500) NOT NULL,
    description     NVARCHAR(MAX) NOT NULL,
    image           NVARCHAR(500) NULL,
    area_id         INT           NULL,
    avg_rating      DECIMAL(3,2)  NULL DEFAULT 0.00,
    review_count    INT           NULL DEFAULT 0,
    capacity        INT           NULL,
    num_bedrooms    INT           NULL,
    price_per_night DECIMAL(12,2) NULL,
    status          NVARCHAR(10)  NOT NULL DEFAULT 'pending',
    created_at      DATETIME2     NULL DEFAULT GETDATE(),
    updated_at      DATETIME2     NULL DEFAULT GETDATE(),
    opening_hour    TIME          NULL,
    closing_hour    TIME          NULL,
    CONSTRAINT PK_businesses        PRIMARY KEY (business_id),
    CONSTRAINT CK_businesses_type   CHECK (type   IN ('homestay','restaurant')),
    CONSTRAINT CK_businesses_status CHECK (status IN ('active','pending','rejected')),
    CONSTRAINT FK_businesses_owner  FOREIGN KEY (owner_id) REFERENCES users(user_id)  ON DELETE CASCADE,
    CONSTRAINT FK_businesses_area   FOREIGN KEY (area_id)  REFERENCES areas(area_id)  ON DELETE SET NULL
);
GO

CREATE OR ALTER TRIGGER trg_businesses_updated_at
ON businesses AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE b SET b.updated_at = GETDATE()
    FROM businesses b INNER JOIN inserted i ON b.business_id = i.business_id;
END;
GO

SET IDENTITY_INSERT businesses ON;
INSERT INTO businesses (business_id,owner_id,name,type,address,description,image,area_id,avg_rating,review_count,capacity,num_bedrooms,price_per_night,status,created_at,updated_at,opening_hour,closing_hour) VALUES
(2, 6, N'Cat Ba Eco Garden Homestay',N'homestay',N'Số 5 Núi Ngọc, Thị trấn Cát Bà, Hải Phòng',N'Homestay thân thiện với môi trường, có vườn xanh, bếp chung, gần biển.',N'https://cf.bstatic.com/xdata/images/hotel/max1024x768/624962683.jpg?k=70302be5175e9ecc65e4632c4b1ccb1fceb7c64c69e79b745effb9673960142d&o=&hp=1',1,0.00,0,10,5,1500000.00,N'active','2025-10-13 16:10:13','2025-11-14 16:39:13','08:00:00','15:00:00'),
(3, 7, N'Mộc Homestay',N'homestay',N'1111 aaa',N'ddep',NULL,NULL,0.00,0,NULL,NULL,NULL,N'active','2025-10-14 06:02:54','2025-11-14 19:04:36',NULL,NULL),
(4, 8, N'Secret Garden Restaurant',N'restaurant',N'169 Núi Ngọc, TT. Cát Bà, Cát Hải, Hải Phòng 180000, Vietnam',N'Giữa Cát Bà nhộn nhịp, tôi đã tâm huyết tạo ra Secret Garden như một "khu vườn bí mật" đúng nghĩa – một không gian xanh mát, yên bình, nơi quý khách có thể thực sự thư giãn và tạm lánh xa ồn ào. Chúng tôi chuyên tâm phục vụ những hương vị tinh túy của ẩm thực Việt Nam và đặc biệt là nguồn hải sản tươi ngon nhất mà biển Cát Bà ban tặng. Mỗi món ăn đều chứa đựng sự chăm chút của đội ngũ chúng tôi.',N'https://media-cdn.tripadvisor.com/media/photo-s/1a/55/6e/f9/the-entrance-of-secret.jpg',5,5.00,0,100,NULL,NULL,N'active','2025-10-21 12:58:53','2025-11-12 18:34:53','08:00:00','23:00:00'),
(25,9, N'Cat Ba Santorini Homestay',N'homestay',N'Số 12, ngõ 243 Cái Bèo',N'Lấy cảm hứng từ hòn đảo Santorini xinh đẹp của Hy Lạp, homestay này có tầm nhìn tuyệt đẹp ra biển, hồ bơi và quán cà phê trên sân thượng. Lý tưởng cho các cặp đôi và nhóm bạn.',N'https://sinhtour.vn/wp-content/uploads/2024/07/homestay-cat-ba-10.jpg',2,4.00,0,10,20,3000000.00,N'active','2025-10-15 01:14:36','2025-10-25 06:47:54',NULL,NULL),
(26,10,N'Lan Homestay',N'homestay',N'Làng Việt Hải, Cát Hải, Hải Phòng',N'Nằm trong làng Việt Hải yên bình thuộc Vườn quốc gia Cát Bà, Lan Homestay mang đến một không gian thanh thản...',N'https://cf.bstatic.com/xdata/images/hotel/max1024x768/241502454.jpg?k=9dd6711aa689fa7440734297b9a7321d999dbfd03e60ecb4a14a0c35cace09f3&o=&hp=1',1,4.50,0,10,5,500000.00,N'active','2025-10-15 01:19:10','2025-10-27 08:46:46',NULL,NULL),
(27,11,N'Little Cat Ba Homestay',N'homestay',N'350 Hà Sen, Thị trấn Cát Bà, Cát Hải, Hải Phòng',N'Một homestay ấm cúng và thân thiện...',N'https://pix10.agoda.net/hotelImages/1176637/0/5773c402c9081e63783368666a25369b.jpeg?ce=0&s=414x232&ar=16x9',3,0.00,0,23,10,200000.00,N'active','2025-10-15 01:21:32','2025-11-14 19:37:20',NULL,NULL),
(28,12,N'Cat Ba Rustic Homestay',N'homestay',N'Đường xuyên đảo, Vườn Quốc gia Cát Bà, Hải Phòng',N'Homestay mang phong cách mộc mạc...',NULL,NULL,0.00,0,NULL,NULL,NULL,N'active','2025-10-15 01:23:35','2025-10-15 01:26:15',NULL,NULL),
(29,13,N'Green Homestay',N'homestay',N'Số 192, đường 1/4, Thị trấn Cát Bà, Cát Hải, Hải Phòng',N'Tọa lạc tại vị trí thuận tiện...',NULL,NULL,0.00,0,NULL,NULL,NULL,N'active','2025-10-15 01:25:26','2025-10-15 01:26:21',NULL,NULL),
(30,14,N'Eco Floating Farm Stay Cai Beo - Standard Double Room',N'homestay',N'Đảo Khỉ, Cát Hải, Hải Phòng 01234, Vietnam',N'Hầu hết các phòng tại đây đều có tầm nhìn hướng ra biển/vịnh và núi đá vôi.',N'https://cf.bstatic.com/xdata/images/hotel/max1024x768/504748753.jpg?k=f6c3044aee0a2d246ddcc92b8d3ac0e7f7890472844ba93c1c50c8ca7748f27e&o=',5,0.00,0,NULL,NULL,500000.00,N'active','2025-10-29 19:36:56','2025-11-14 19:57:09','00:30:00','14:30:00'),
(31,17,N'Cát Bà Mountain View',N'homestay',N'Đảo Khỉ, Cát Hải, Hải Phòng 01234, Vietnam',N'Cát Bà Mountain View là một trong những homestay nổi tiếng nhất, có bể bơi trên núi cao độc đáo, mang đến tầm nhìn toàn cảnh núi non hùng vĩ và một phần biển. Gần gũi thiên nhiên, yên tĩnh, có nhiều loại phòng từ phòng đôi, phòng gia đình, đến bungalow và phòng dorm.',N'https://cdn.justfly.vn/1170x400/media/202108/15/1628999265-khuon-vien-cat-ba-mountain-view-homestay-hai-phong-1.jpeg',3,0.00,0,NULL,NULL,500000.00,N'active','2025-10-31 04:41:37','2025-11-14 19:21:37','00:30:00','14:30:00'),
(32,18,N'The Three M Restaurant',N'restaurant',N'The Three M Restaurant',N'Nhà hàng tạo không khí sôi động với các chương trình giải trí như Live Music (nhạc sống) và DJ. Đặc biệt, đây còn là nơi có "Vườn bia thủ công" lần đầu tiên xuất hiện tại Cát Bà, phục vụ nhiều loại bia tươi và bia thủ công độc đáo. Phục vụ thực đơn đa dạng, hấp dẫn, kết hợp giữa các món ăn Âu và Á, đặc biệt là hải sản tươi ngon đặc trưng của Đảo Ngọc Cát Bà.',N'https://scontent.fhan14-2.fna.fbcdn.net/v/t39.30808-6/500766741_623347210728032.jpg',4,0.00,0,NULL,NULL,NULL,N'active','2025-10-31 04:44:08','2025-11-14 18:47:23','10:00:00','23:00:00'),
(33,20,N'Celery Restaurant',N'restaurant',N'Celery Restaurant',N'Nhà hàng Celery (227 Núi Ngọc, Cát Bà) là một nhà hàng thuần chay nổi bật. Quán chuyên các món chay Việt Nam và quốc tế, được đánh giá cao vì đồ ăn ngon, giá hợp lý và không gian sạch sẽ. Đây là lựa chọn hàng đầu cho ẩm thực lành mạnh tại Cát Bà.',N'https://cdn3.ivivu.com/2023/05/nha-hang-chay-ha-noi-ivivu-15.jpg',2,0.00,0,NULL,NULL,NULL,N'active','2025-11-07 03:42:13','2025-11-14 18:47:23','09:00:00','02:00:00'),
(34,21,N'Secret Garden Restaurant',N'restaurant',N'Cát Bà, Hải Phòng',N'Không gian ấm cúng, gần gũi và lý tưởng cho những bữa ăn gia đình, bạn bè. Các món ăn được chế biến tỉ mỉ, đảm bảo hương vị đậm đà.',N'https://dynamic-media-cdn.tripadvisor.com/media/photo-o/2e/63/84/89/warmly-welcome-you-to.jpg?w=900&h=500&s=1',3,0.00,0,NULL,NULL,NULL,N'active','2025-11-10 14:00:48','2025-11-14 18:47:23','08:30:00','22:00:00'),
(35,22,N'Marigold Restaurant',N'restaurant',N'Cát Bà, Hải Phòng',N'Marigold Restaurant là một trong những nhà hàng được nhắc đến nhiều nhất với phong cách sang trọng, đẳng cấp 5 sao và sở hữu tầm nhìn đẹp ra Vịnh Lan Hạ. Nhà hàng có thực đơn phong phú, kết hợp tinh tế giữa ẩm thực Việt Nam và quốc tế (Âu). Bạn có thể tìm thấy các món Âu cao cấp tại đây.',N'https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/351216995.png',2,0.00,0,NULL,NULL,NULL,N'active','2025-11-12 17:49:58','2025-11-14 18:47:23','10:00:00','01:00:00'),
(36,23,N'Lẩu Nướng Thuý Anh',N'restaurant',N'Cát Bà, Hải Phòng',N'Thực đơn phong phú, đa dạng các món lẩu nướng. Phục vụ chu đáo, phù hợp cho mọi đối tượng khách hàng.',N'https://byvn.net/r6xO',2,0.00,0,NULL,NULL,NULL,N'active','2025-11-14 18:23:26','2025-11-14 18:29:42','00:00:00','00:00:00'),
(37,24,N'Domik Homestay',N'homestay',N'Cát Bà, Hải Phòng',N'Gần bãi biển Tùng Thu (chỉ 3 phút đi bộ), không gian ấm cúng, tối giản, phù hợp cho nhóm bạn và gia đình.',N'https://bevivu.com/wp-content/uploads/image12/2024/02/domik-homestay080220241707395742.jpeg',5,0.00,0,NULL,NULL,600000.00,N'active','2025-11-14 19:26:07','2025-11-14 19:45:08','10:30:00','13:30:00');
SET IDENTITY_INSERT businesses OFF;
GO

-- ============================================================
-- Table: business_amenities
-- ============================================================
CREATE TABLE business_amenities (
    business_id INT NOT NULL,
    amenity_id  INT NOT NULL,
    CONSTRAINT PK_business_amenities PRIMARY KEY (business_id, amenity_id),
    CONSTRAINT FK_ba_business FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE,
    CONSTRAINT FK_ba_amenity  FOREIGN KEY (amenity_id)  REFERENCES amenities(amenity_id)   ON DELETE CASCADE
);
GO
INSERT INTO business_amenities VALUES (2,1);
GO

-- ============================================================
-- Table: cuisine_types
-- ============================================================
CREATE TABLE cuisine_types (
    cuisine_id INT           NOT NULL IDENTITY(1,1),
    name       NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_cuisine_types      PRIMARY KEY (cuisine_id),
    CONSTRAINT UQ_cuisine_types_name UNIQUE (name)
);
GO
SET IDENTITY_INSERT cuisine_types ON;
INSERT INTO cuisine_types (cuisine_id, name) VALUES (3,N'hải sản'),(1,N'lẩu'),(2,N'nướng');
SET IDENTITY_INSERT cuisine_types OFF;
GO

-- ============================================================
-- Table: business_cuisines
-- ============================================================
CREATE TABLE business_cuisines (
    business_id INT NOT NULL,
    cuisine_id  INT NOT NULL,
    CONSTRAINT PK_business_cuisines PRIMARY KEY (business_id, cuisine_id),
    CONSTRAINT FK_bc_business FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE,
    CONSTRAINT FK_bc_cuisine  FOREIGN KEY (cuisine_id)  REFERENCES cuisine_types(cuisine_id) ON DELETE CASCADE
);
GO
INSERT INTO business_cuisines VALUES (4,1),(4,2),(4,3);
GO

-- ============================================================
-- Table: occasions
-- ============================================================
CREATE TABLE occasions (
    occasion_id INT           NOT NULL IDENTITY(1,1),
    name        NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_occasions      PRIMARY KEY (occasion_id),
    CONSTRAINT UQ_occasions_name UNIQUE (name)
);
GO
SET IDENTITY_INSERT occasions ON;
INSERT INTO occasions (occasion_id, name) VALUES
(4,N'Công tác'),(5,N'Nghỉ dưỡng dài ngày'),(2,N'Phù hợp cho cặp đôi'),(1,N'Phù hợp cho gia đình'),(3,N'Phù hợp cho nhóm bạn');
SET IDENTITY_INSERT occasions OFF;
GO

-- ============================================================
-- Table: business_occasions
-- ============================================================
CREATE TABLE business_occasions (
    business_id INT NOT NULL,
    occasion_id INT NOT NULL,
    CONSTRAINT PK_business_occasions PRIMARY KEY (business_id, occasion_id),
    CONSTRAINT FK_bo_business FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE,
    CONSTRAINT FK_bo_occasion FOREIGN KEY (occasion_id) REFERENCES occasions(occasion_id)   ON DELETE CASCADE
);
GO
INSERT INTO business_occasions VALUES (2,1);
GO

-- ============================================================
-- Table: restaurant_types
-- ============================================================
CREATE TABLE restaurant_types (
    type_id INT           NOT NULL IDENTITY(1,1),
    name    NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_restaurant_types      PRIMARY KEY (type_id),
    CONSTRAINT UQ_restaurant_types_name UNIQUE (name)
);
GO
SET IDENTITY_INSERT restaurant_types ON;
INSERT INTO restaurant_types (type_id, name) VALUES (4,N'Lẩu'),(1,N'Nhà hàng hải sản'),(3,N'Nướng BBQ'),(2,N'Quán ăn địa phương');
SET IDENTITY_INSERT restaurant_types OFF;
GO

-- ============================================================
-- Table: business_restaurant_types
-- ============================================================
CREATE TABLE business_restaurant_types (
    business_id INT NOT NULL,
    type_id     INT NOT NULL,
    CONSTRAINT PK_business_restaurant_types PRIMARY KEY (business_id, type_id),
    CONSTRAINT FK_brt_business FOREIGN KEY (business_id) REFERENCES businesses(business_id)   ON DELETE CASCADE,
    CONSTRAINT FK_brt_type     FOREIGN KEY (type_id)     REFERENCES restaurant_types(type_id) ON DELETE CASCADE
);
GO
INSERT INTO business_restaurant_types VALUES (4,3),(4,4);
GO

-- ============================================================
-- Table: features
-- ============================================================
CREATE TABLE features (
    feature_id   INT           NOT NULL IDENTITY(1,1),
    feature_name NVARCHAR(100) NOT NULL,
    url          NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_features     PRIMARY KEY (feature_id),
    CONSTRAINT UQ_features_url UNIQUE (url)
);
GO
SET IDENTITY_INSERT features ON;
INSERT INTO features (feature_id, feature_name, url) VALUES
(1,N'Trang chủ',N'/Home'),
(2,N'Danh sách Homestay',N'/homestay-list'),
(3,N'Danh sách Restaurant',N'/restaurant'),
(4,N'Chi tiết Homestay',N'/homestay-detail'),
(5,N'Chi tiết Restaurant',N'/restaurant-detail'),
(6,N'Tìm kiếm Homestay',N'/homestays-list'),
(7,N'Tìm kiếm Restaurant',N'/restaurants'),
(8,N'Thêm vào giỏ hàng',N'/add-to-cart'),
(9,N'Cập nhật số lượng giỏ hàng',N'/update-cart-quantity'),
(10,N'Cập nhật ghi chú giỏ hàng',N'/update-cart-notes'),
(11,N'Xóa khỏi giỏ hàng',N'/remove-from-cart'),
(12,N'Kiểm tra bàn trống',N'/check-available-table'),
(13,N'Thanh toán đặt bàn Restaurant',N'/checkout-restaurant'),
(14,N'Xác nhận thanh toán',N'/confirmation-payment'),
(15,N'Trạng thái thanh toán',N'/payment-status'),
(16,N'Webhook SePay',N'/sepay-webhook'),
(17,N'Hủy booking hết hạn',N'/cancel-expired-booking'),
(18,N'Đăng Nhập',N'/Login'),
(19,N'Đăng Xuất',N'/Logout'),
(20,N'Quên Mật Khẩu',N'/forgot-password'),
(21,N'Đăng kí tài khoản Owner',N'/register-owner'),
(22,N'Đăng kí tài khoản Customer',N'/register-customer'),
(24,N'Danh sách món ăn',N'/list-dish'),
(25,N'Thêm món ăn',N'/add-dish'),
(26,N'Quản lý bàn ăn Restaurant',N'/restaurant-manage-tables'),
(27,N'Thông tin Restaurant',N'/restaurant-profile'),
(28,N'Thêm bàn',N'/restaurant-table-add'),
(29,N'Xóa bàn',N'/restaurant-table-delete'),
(30,N'Update bàn',N'/restaurant-table-update'),
(31,N'Update món ăn',N'/update-dish'),
(32,N'Xóa phòng trong homestay',N'/delete-homestay-room'),
(33,N'Xem Chi tiết Đặt phòng',N'/get-homestay-booking-details'),
(34,N'Quản lý Phòng',N'/manage-homestay-rooms'),
(35,N'Thông tin Homestay',N'/homestay-settings'),
(36,N'Chỉnh sửa phòng',N'/update-homestay-room'),
(37,N'Bật/Tắt Trạng thái Phòng',N'/toggle-homestay-room-status'),
(38,N'Danh sách Đơn đặt phòng',N'/homestay-bookings'),
(39,N'Danh sách booking restaurant',N'/owner-bookings');
SET IDENTITY_INSERT features OFF;
GO

-- ============================================================
-- Table: roles_features
-- ============================================================
CREATE TABLE roles_features (
    role_id    INT NOT NULL,
    feature_id INT NOT NULL,
    CONSTRAINT PK_roles_features PRIMARY KEY (role_id, feature_id),
    CONSTRAINT FK_rf_role    FOREIGN KEY (role_id)    REFERENCES roles(role_id)       ON DELETE CASCADE,
    CONSTRAINT FK_rf_feature FOREIGN KEY (feature_id) REFERENCES features(feature_id) ON DELETE CASCADE
);
GO
INSERT INTO roles_features VALUES
(1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),(1,15),(1,16),(1,17),(1,18),
(2,18),(4,18),(1,19),(2,19),(4,19),(1,20),(2,20),(4,20),(1,21),(2,21),(4,21),(1,22),(2,22),(4,22),
(4,24),(4,25),(4,26),(4,27),(4,28),(4,29),(4,30),(4,31),(2,32),(2,33),(2,34),(2,35),(2,36),(2,37),(2,38),(4,39);
GO

-- ============================================================
-- Table: rooms
-- tinyint(1) -> BIT
-- ============================================================
CREATE TABLE rooms (
    room_id         INT           NOT NULL IDENTITY(1,1),
    business_id     INT           NOT NULL,
    name            NVARCHAR(255) NOT NULL,
    capacity        INT           NOT NULL,
    price_per_night DECIMAL(12,2) NOT NULL,
    is_active       BIT           NULL DEFAULT 1,
    CONSTRAINT PK_rooms          PRIMARY KEY (room_id),
    CONSTRAINT FK_rooms_business FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT rooms ON;
INSERT INTO rooms (room_id,business_id,name,capacity,price_per_night,is_active) VALUES
(1, 2,N'Phòng Đôi View Biển',2,850000.00, 1),
(2, 2,N'Phòng cạnh biển',    4,1400000.00,1),
(5,27,N'phòng vip',          2,350000.00, 1),
(6,27,N'phòng thường',       2,250000.00, 1),
(7,27,N'phòng thường',       4,250000.00, 1),
(8,27,N'phong vip',          4,400000.00, 1),
(9,37,N'Phòng Thường',       2,500000.00, 1),
(10,25,N'Phòng Thường',      2,600000.00, 1),
(11,26,N'Phòng Vip',         2,1000000.00,1),
(12,31,N'Phòng Thường',      2,700000.00, 1);
SET IDENTITY_INSERT rooms OFF;
GO

-- ============================================================
-- Table: room_images
-- ============================================================
CREATE TABLE room_images (
    image_id  INT           NOT NULL IDENTITY(1,1),
    room_id   INT           NOT NULL,
    image_url NVARCHAR(500) NOT NULL,
    CONSTRAINT PK_room_images      PRIMARY KEY (image_id),
    CONSTRAINT FK_room_images_room FOREIGN KEY (room_id) REFERENCES rooms(room_id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT room_images ON;
INSERT INTO room_images (image_id,room_id,image_url) VALUES
(1, 1, N'https://cf.bstatic.com/xdata/images/hotel/max1024x768/733132235.jpg?k=651e0dc21e3855a50d1bf760010c0653a300c01b0ce6c354df8a047da1c6537f&o=&hp=1'),
(2, 5, N'https://catba.tours/wp-content/uploads/2021/04/Little-Cat-Ba-Boat-House-5.png'),
(3, 6, N'https://catba.tours/wp-content/uploads/2021/04/Little-Cat-Ba-Boat-House-8-900x500.png'),
(4, 7, N'https://catba.tours/wp-content/uploads/2021/04/Cat-Ba-Love-House-7-900x500.png'),
(5, 8, N'https://catba.tours/wp-content/uploads/2021/04/Cat-Ba-Love-House-2-900x500.png'),
(6, 9, N'https://q-xx.bstatic.com/xdata/images/hotel/max500/546727837.jpg?k=b3f64ac4f756bec1b430f98eb0256ab479154faa3a51dc1337100ee3f6529ee5&o='),
(7,10, N'https://cf.bstatic.com/xdata/images/hotel/max1024x768/688231861.jpg?k=57356fb03ed6f9f9d0c953fee92f2f25805105c183bf5200e44544727afc1b15&o='),
(8,11, N'https://dynamic-media-cdn.tripadvisor.com/media/photo-o/2a/35/13/61/viet-hai-homestay.jpg?w=700&h=-1&s=1'),
(9,12, N'https://saodieu.vn/travel/media/product/thumb_410_1713750033_deluxe-mountain-view-1.jpg');
SET IDENTITY_INSERT room_images OFF;
GO

-- ============================================================
-- Table: room_availability
-- enum('booked','blocked') -> NVARCHAR(10) + CHECK
-- bigint -> BIGINT IDENTITY
-- ============================================================
CREATE TABLE room_availability (
    availability_id BIGINT        NOT NULL IDENTITY(1,1),
    room_id         INT           NOT NULL,
    date            DATE          NOT NULL,
    price           DECIMAL(12,2) NULL,
    status          NVARCHAR(10)  NOT NULL,
    CONSTRAINT PK_room_availability       PRIMARY KEY (availability_id),
    CONSTRAINT UQ_room_availability       UNIQUE (room_id, date),
    CONSTRAINT CK_room_avail_status       CHECK (status IN ('booked','blocked')),
    CONSTRAINT FK_room_avail_room         FOREIGN KEY (room_id) REFERENCES rooms(room_id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT room_availability ON;
INSERT INTO room_availability (availability_id,room_id,date,price,status) VALUES
-- LƯU Ý: room_id=4 đã bị xóa trong MySQL dump, bỏ 2 dòng dưới để tránh lỗi FK
(1,1,'2025-10-14',850000.00,N'booked'),
(2,1,'2025-10-28',850000.00,N'booked'),
(3,1,'2025-10-29',850000.00,N'booked'),
(6,2,'2025-10-28',850000.00,N'booked'),
(7,2,'2025-10-29',850000.00,N'booked'),
(8,1,'2025-11-15',850000.00,N'booked'),
(9,1,'2025-11-16',850000.00,N'booked');
SET IDENTITY_INSERT room_availability OFF;
GO

-- ============================================================
-- Table: restaurant_tables
-- ============================================================
CREATE TABLE restaurant_tables (
    table_id    INT           NOT NULL IDENTITY(1,1),
    business_id INT           NOT NULL,
    name        NVARCHAR(100) NOT NULL,
    capacity    INT           NOT NULL,
    is_active   BIT           NULL DEFAULT 1,
    CONSTRAINT PK_restaurant_tables   PRIMARY KEY (table_id),
    CONSTRAINT FK_rt_business         FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT restaurant_tables ON;
INSERT INTO restaurant_tables (table_id,business_id,name,capacity,is_active) VALUES
(1, 4,N'Bàn 01',4,1),(2,4,N'Bàn 02',8,1),(3,4,N'Bàn 03',4,1),(4,4,N'Bàn 04',4,1),(5,4,N'Bàn 05',8,1),
(7, 4,N'Bàn 06',6,1),(11,4,N'Bàn 10',4,1),(12,4,N'Bàn 07',10,1),
(13,33,N'Bàn 01',2,1),(14,33,N'Bàn 02',4,1),(15,33,N'Bàn 03',2,1),(16,33,N'Bàn 04',4,1),(17,33,N'Bàn 05',2,1),
(18,32,N'Bàn 01',6,1),(19,32,N'Bàn 02',4,1),(20,32,N'Bàn 03',8,1),(21,32,N'Bàn 04',8,1),(22,32,N'Bàn 05',4,1),
(23,32,N'Bàn 06',4,1),(24,32,N'Bàn 07',6,1),(25,32,N'Bàn 08',8,1),
(26,35,N'Bàn 01',2,1),(27,35,N'Bàn 02',2,1),(28,35,N'Bàn 03',4,1),(29,35,N'Bàn 04',8,1),(30,35,N'Bàn 05',6,1),
(31,35,N'Bàn 06',10,1),(32,35,N'Bàn 07',4,1),
(33,34,N'Bàn 01',2,1),(34,34,N'Bàn 02',4,1),(35,34,N'Bàn 03',4,1),(36,34,N'Bàn 04',6,1),(37,34,N'Bàn 05',6,1),
(38,34,N'Bàn 06',8,1),(39,34,N'Bàn 07',8,1),
(40,36,N'Bàn 01',4,1),(41,36,N'Bàn 02',4,1),(42,36,N'Bàn 03',4,1),(43,36,N'Bàn 04',4,1),(44,36,N'Bàn 05',4,1),(45,36,N'Bàn 06',4,1);
SET IDENTITY_INSERT restaurant_tables OFF;
GO

-- ============================================================
-- Table: table_availability
-- ============================================================
CREATE TABLE table_availability (
    availability_id  BIGINT       NOT NULL IDENTITY(1,1),
    table_id         INT          NOT NULL,
    reservation_date DATE         NOT NULL,
    reservation_time TIME         NOT NULL,
    status           NVARCHAR(10) NOT NULL,
    CONSTRAINT PK_table_availability       PRIMARY KEY (availability_id),
    CONSTRAINT UQ_table_availability       UNIQUE (table_id, reservation_date, reservation_time),
    CONSTRAINT CK_table_avail_status       CHECK (status IN ('booked','blocked')),
    CONSTRAINT FK_table_avail_table        FOREIGN KEY (table_id) REFERENCES restaurant_tables(table_id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT table_availability ON;
INSERT INTO table_availability (availability_id,table_id,reservation_date,reservation_time,status) VALUES
(39,1, '2025-11-13','12:30:00',N'booked'),
(40,3, '2025-11-13','11:30:00',N'booked'),
(41,1, '2025-11-13','21:30:00',N'booked'),
(42,3, '2025-11-13','20:00:00',N'booked'),
(43,4, '2025-11-13','20:30:00',N'booked'),
(44,11,'2025-11-13','21:30:00',N'booked'),
(45,7, '2025-11-13','21:30:00',N'booked'),
(46,1, '2025-11-20','20:30:00',N'booked'),
(47,1, '2025-11-24','21:30:00',N'booked'),
(48,1, '2025-11-14','20:00:00',N'booked');
SET IDENTITY_INSERT table_availability OFF;
GO

-- ============================================================
-- Table: dish_categories
-- ============================================================
CREATE TABLE dish_categories (
    category_id   INT           NOT NULL IDENTITY(1,1),
    business_id   INT           NOT NULL,
    name          NVARCHAR(100) NOT NULL,
    display_order INT           NULL DEFAULT 0,
    CONSTRAINT PK_dish_categories   PRIMARY KEY (category_id),
    CONSTRAINT FK_dc_business        FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE
);
GO
SET IDENTITY_INSERT dish_categories ON;
INSERT INTO dish_categories (category_id,business_id,name,display_order) VALUES
(1,4,N'Nướng',0),(2,4,N'Hải Sản',0),(3,4,N'Lẩu',0),
(4,33,N'Chay',0),(5,33,N'Lẩu',0),(6,33,N'Tráng miệng',0),
(7,32,N'Hải sản',0),
(8,35,N'hải sản',0),(9,35,N'Nướng',0),
(10,34,N'Món Việt',0),
(11,36,N'Lẩu',0),(12,36,N'Nướng',0),(13,36,N'Lẩu Nướng',0),(14,36,N'Hải sản',0);
SET IDENTITY_INSERT dish_categories OFF;
GO

-- ============================================================
-- Table: dishes
-- tinyint(1) NOT NULL DEFAULT '1' -> BIT NOT NULL DEFAULT 1
-- ============================================================
CREATE TABLE dishes (
    dish_id      INT           NOT NULL IDENTITY(1,1),
    business_id  INT           NOT NULL,
    category_id  INT           NULL,
    name         NVARCHAR(255) NOT NULL,
    description  NVARCHAR(MAX) NULL,
    price        DECIMAL(12,2) NOT NULL,
    image_url    NVARCHAR(500) NULL,
    is_available BIT           NOT NULL DEFAULT 1,
    CONSTRAINT PK_dishes           PRIMARY KEY (dish_id),
    CONSTRAINT FK_dishes_business  FOREIGN KEY (business_id) REFERENCES businesses(business_id)      ON DELETE CASCADE,
    -- ON DELETE NO ACTION thay vì SET NULL để tránh lỗi "multiple cascade paths":
    -- businesses -> dish_categories -> dishes (SET NULL) VÀ businesses -> dishes (CASCADE) -> conflict!
    CONSTRAINT FK_dishes_category  FOREIGN KEY (category_id) REFERENCES dish_categories(category_id) ON DELETE NO ACTION
);
GO
SET IDENTITY_INSERT dishes ON;
INSERT INTO dishes (dish_id,business_id,category_id,name,description,price,image_url,is_available) VALUES
(1, 4, 1,N'Sườn nướng mật ong',      N'Sườn được chặt miếng vừa ăn, tẩm ướp gia vị theo công thức riêng của nhà hàng, sau đó nướng trực tiếp trên bếp than, khi nướng phết thêm mật ong để tạo hương vị đặc trưng.',185000.00,N'/CatBaBooking/uploads/dishes/cd826ac9d7d04432992c2c354e790fa4.webp',1),
(2, 4, 1,N'Mực Khô Nướng',            N'Mực khô lựa chọn toàn những con to, đẹp, nướng trực tiếp trên bếp than, sau đó đập dập, xé sợi là có thể thưởng thức cùng những cốc bia mát lạnh.',249000.00,N'/CatBaBooking/uploads/dishes/c4f28c8268b446a4b114ac859e287969.webp',1),
(3, 4, 1,N'Lợn mán nướng',            N'Lợn mán nướng giềng mẻ là món ăn đậm đà, đốt cháy mọi cảm xúc vị giác của bạn. Thịt lợn mán với vị ngọt tự nhiên, khi được ướp với riềng, mẻ, sả, và các loại gia vị khác, rồi nướng lên sẽ tạo nên hương vị thơm ngon, đậm đà, rất đưa mồi.',10000.00,N'/CatBaBooking/uploads/dishes/770a8bdb0b7e4f7fa769fa11894c1597.webp',1),
(4, 4, 2,N'Mực Hấp Chanh',            N'Mực hấp chanh là món ăn thú vị nên thử khi đến Cát Bà cùng bạn bè, người thân. Mực tươi được hấp với quả chanh tươi lát mỏng và rưới thêm nước sốt chua cay. Miếng mực giòn sần sật, ngọt tự nhiên lại ăn cùng nước chấm tỏi ớt, gừng, sả thì cực tốn bia.',285000.00,N'/CatBaBooking/uploads/dishes/b083746a93c54386a150f94b648410ec.webp',1),
(6, 4, 2,N'Tôm tắm sốt thái',         N'Tôm tắm sốt Thái có vị ngọt của tôm tươi sần sật, quyện cùng hương thơm đặc trưng dễ gây nghiện của sốt Thái chua cay khó cưỡng.',249000.00,N'/CatBaBooking/uploads/dishes/61f779ecb04045ccafa218ce807b78e8.webp',1),
(7, 4, 2,N'Tôm chiên hoàng kim',       N'Tôm tươi, căng mọng đem chiên vàng giòn rồi lại đảo qua gia vị đậm đà tạo nên món ăn hấp dẫn, ăn một miếng là không ngừng lại được.',249000.00,N'/CatBaBooking/uploads/dishes/d91433265a8840b2ab87498a87c5e4c6.webp',1),
(8, 4, 2,N'Ốc hương ủ muối thảo mộc',N'Những con ốc hương tươi ngon được ủ cùng muối biển tinh khiết tạo nên hương vị đậm đà, thơm lừng.',269000.00,N'/CatBaBooking/uploads/dishes/029d010f4c2d4232bc40539716e9b14a.webp',1),
(9, 4, 2,N'Cá dưa chua tứ xuyên',     N'Cá quả được om nguyên con, thịt cá chắc nịch, không bã, miếng nào miếng nấy ngấm nước om đậm đà.',469000.00,N'/CatBaBooking/uploads/dishes/32a58b370eb74b10a751147c5aa1bb0f.webp',1),
(10,4, 3,N'Lẩu hải sản kiểu thái',    N'Lẩu hải sản kiểu Thái với những nguyên liệu tươi roi rói: Tôm, Mực, Bạch tuộc, Ba chỉ bò, Viên thả lẩu cùng nồi nước dùng chua cay chuẩn Thái chắc chắn sẽ kích thích vị giác của mọi người.',599000.00,N'/CatBaBooking/uploads/dishes/027d3f11e7e544cfa4c66594c3ef764e.webp',1),
(11,4, 3,N'Lẩu gà đen nấm rừng Vân Nam',N'nước cốt lẩu ngọt thanh, thơm lừng mùi thảo mộc, được ninh từ đủ loại nấm rừng Vân Nam quý hiếm. Ăn kèm lẩu là nửa con gà đen săn chắc, đã được lọc xương sẵn, chỉ việc nhúng vào rồi chén thôi.',499000.00,N'/CatBaBooking/uploads/dishes/e2bb685aff914964bfd13f7bc97c21df.webp',1),
(12,4, 3,N'Lẩu riêu cua bắp bò',      N'Nước dùng có vị chua dịu từ giấm bỗng, dậy mùi thơm của riêu cua cùng với đa dạng các loại đồ nhúng: Bắp bò, Ba chỉ bò, Sườn sụn, Giò tai, Chả cá',589000.00,N'/CatBaBooking/uploads/dishes/d4d59fba22684cc6ac88d51f13260b0a.webp',1),
(13,4, 1,N'Gà đen nướng mắc khén',    N'Gà nửa con, hạt mắc khén rang thơm, quả ớt cay, lá chanh, muối, củ sả. Chuẩn gia vị chẩm chéo để chấm gà, thơm phức mùi mắc kén và tê tê đầu lưỡi.',385000.00,N'/CatBaBooking/uploads/dishes/c065bc06b537419dbe4c33bbaa13114e.webp',1),
(14,4, 2,N'Cá chép om dưa',           N'Cá chép om dưa hấp dẫn với thịt cá tươi ngon kết hợp vị chua dịu của dưa muối, mẻ, cay nồng của sốt sa tế hòa quyện với nhau tạo nên sức hấp dẫn đặc biệt.',425000.00,N'/CatBaBooking/uploads/dishes/9492f4ee91544761a3a58e8dafafe533.webp',1),
(15,33,5,N'Lẩu riêu cua chay',        N'Lẩu riêu cua chay nổi bật với nước dùng màu đỏ cam bắt mắt, vị chua thanh nhẹ của cà chua và ngọt dịu tự nhiên từ rau củ.',99000.00, N'/CatBaBooking/uploads/dishes/41a02ae3865943a4b6142ac4290c94c6.webp',1),
(16,33,6,N'Chè Bưởi',                 N'Chè bưởi là một món tráng miệng (chè) truyền thống rất nổi tiếng của miền Nam Việt Nam, đặc biệt là ở Cần Thơ.',25000.00, N'/CatBaBooking/uploads/dishes/db9dc4b35341464c951fb65859e634d0.webp',1),
(17,33,4,N'Cơm lá sen',               N'',50000.00, N'/CatBaBooking/uploads/dishes/ca35660e360543f9a010bbd71e9d1551.jpg',1),
(18,33,4,N'Phở chay',                 N'',30000.00, N'/CatBaBooking/uploads/dishes/e6be08a4962240208eaecd6edfc0eb36.jpg',1),
(19,33,4,N'Bún bò huế Chay',          N'',30000.00, N'/CatBaBooking/uploads/dishes/bed3f17b317b4e0cb566907faf0bd924.jpg',1),
(20,33,4,N'Bánh khọt',                N'',40000.00, N'/CatBaBooking/uploads/dishes/8b5d67de3ddf4502aa69412d91272fa6.jpg',1),
(21,33,4,N'Bánh Xèo',                 N'',40000.00, N'/CatBaBooking/uploads/dishes/4639b46ba0884c4d87c05b5d1e9b4aac.jpg',1),
(22,33,4,N'Gỏi cuốn',                 N'',35000.00, N'/CatBaBooking/uploads/dishes/4be6b22b6d7445929de6767b19dfa9a6.jpg',1),
(23,33,4,N'Gỏi ngó sen',              N'',40000.00, N'/CatBaBooking/uploads/dishes/4e1b6aa852c3495cb1f6e40d3a17ca69.webp',1),
(24,33,4,N'Nem chay',                 N'',30000.00, N'/CatBaBooking/uploads/dishes/04cafb2816844de099ffce420a42f023.jpg',1),
(25,32,7,N'Bề Bề rang muối',          N'',300000.00,N'/CatBaBooking/uploads/dishes/725e634e889742d5b3b4dd93005bdf3c.jpg',1),
(26,32,7,N'Cá Hồi',                   N'',249000.00,N'/CatBaBooking/uploads/dishes/65ccfbd13e0e4ff59ffde5d7a8fe9a58.jpg',1),
(27,32,7,N'Cua Hoàng Đế Hấp',        N'',3500000.00,N'/CatBaBooking/uploads/dishes/60a74095b71d44a2a4544ac8f3a7c360.jpg',1),
(28,32,7,N'Cua sốt trứng muối',       N'',149000.00,N'/CatBaBooking/uploads/dishes/a5adf744b3a64e358485fd502a954684.jpg',1),
(29,32,7,N'Ngao hoa hấp thái',        N'',159000.00,N'/CatBaBooking/uploads/dishes/10c1519fa85f46f2942d4301b206bce8.jpg',1),
(30,32,7,N'Sashimi',                  N'',449000.00,N'/CatBaBooking/uploads/dishes/b4a4eb845e0c47838bec6e302c2b1f05.jpg',1),
(31,32,7,N'Tôm Hùm nướng phô mai',   N'',999000.00,N'/CatBaBooking/uploads/dishes/49ab9584064448348f75acd961e8da68.jpg',1),
(32,35,8,N'Hàu nướng phô mai',        N'',200000.00,N'/CatBaBooking/uploads/dishes/fe839889aabb4384b8b8cf735cfa6f35.jpg',1),
(33,35,9,N'Beef Steck',               N'',499000.00,N'/CatBaBooking/uploads/dishes/2eec2c115f1c4cc8ba3c39e4bf1985a4.jpg',1),
(34,35,9,N'Cá hồi áp chảo',          N'',499000.00,N'/CatBaBooking/uploads/dishes/1cc9e1bd1d2d4a6b870f1e19931ebedc.jpg',1),
(35,35,8,N'Lodster Bay',              N'',699000.00,N'/CatBaBooking/uploads/dishes/553959f8034345f099209af2ca56d955.jpg',1),
(36,35,8,N'Cá mú hấp xì dầu',        N'',799000.00,N'/CatBaBooking/uploads/dishes/d31d3efe4af04f50932fcc6d7a387215.jpg',1),
(37,35,8,N'Mì cua',                   N'',349000.00,N'/CatBaBooking/uploads/dishes/2b6c3b46c2ba4d02b2948817dfc6d7a3.jpg',1),
(38,35,8,N'Mực nướng sa tế',          N'',549000.00,N'/CatBaBooking/uploads/dishes/92627340c7ab4293919dc6c15f1fe131.jpg',1),
(39,34,10,N'Bánh Khọt',              N'',59000.00, N'/CatBaBooking/uploads/dishes/6f2181541e8f46659552387f807be92e.jpg',1),
(40,34,10,N'Bún chả que',            N'',60000.00, N'/CatBaBooking/uploads/dishes/abd56148c3ba4d96b402e67a29ee6b11.jpg',1),
(41,34,10,N'Thịt cuốn chấm mắm nêm',N'',45000.00, N'/CatBaBooking/uploads/dishes/947a14f0028b4e9d82397be122eae2d8.jpg',1),
(42,34,10,N'Cơm tấm',                N'',89000.00, N'/CatBaBooking/uploads/dishes/b4510836339348609dd2c9c5a9bf7a86.jpg',1),
(43,34,10,N'Ếch rang muối',          N'',70000.00, N'/CatBaBooking/uploads/dishes/cb7026831e1148bfa728999fcb6d1e36.jpg',1),
(44,34,10,N'Rau củ chấm kho quẹt',  N'',39000.00, N'/CatBaBooking/uploads/dishes/e877e624cc0c4219aed3dfdf280a4a81.jpg',1),
(45,34,10,N'Súp bí ngô',             N'',110000.00,N'/CatBaBooking/uploads/dishes/f84aa512b06643a68bd64f6e69981fa0.jpg',1),
(46,34,10,N'Thịt bò xào hoa thiên lý',N'',69000.00,N'/CatBaBooking/uploads/dishes/a73f99950a894bcca53e7cf6cc62eaf2.jpg',1),
(47,34,10,N'Thịt kho tàu',           N'',59000.00, N'/CatBaBooking/uploads/dishes/144054d0f71c4600816e2d97450a70e3.jpg',1),
(48,34,10,N'Thịt xá xíu',           N'',39000.00, N'/CatBaBooking/uploads/dishes/b2a7958354124782b0aca69d0f090ad4.jpg',1),
(49,36,12,N'Buffet 1',               N'',99000.00, N'/CatBaBooking/uploads/dishes/c09795743a9e4849b93c60b8a139e380.jpg',1),
(50,36,12,N'Buffet 2',               N'',139000.00,N'/CatBaBooking/uploads/dishes/a6bfc56c532446319730378ebac3b617.jpg',1),
(51,36,12,N'Buffet 3',               N'',169000.00,N'/CatBaBooking/uploads/dishes/7017b0682ac8499abb87995344ec5c6a.jpg',1),
(52,36,11,N'Lẩu hải sản',           N'',199000.00,N'/CatBaBooking/uploads/dishes/811d8efcbde14477b00121b677ef6836.jpg',1),
(53,36,13,N'Lẩu Nướng',             N'',499000.00,N'/CatBaBooking/uploads/dishes/3db90b4b37ea41af922f70ab295b578f.jpg',1),
(54,36,11,N'Lẩu thái',              N'',199000.00,N'/CatBaBooking/uploads/dishes/882988828ec242f0859662fefd61b006.jpg',1),
(55,36,14,N'Ngao hấp thái',         N'',59000.00, N'/CatBaBooking/uploads/dishes/ccab4d0825a44b46adc81f84970d0fa8.jpg',1),
(56,36,14,N'Ốc hương sốt trứng muối',N'',79000.00,N'/CatBaBooking/uploads/dishes/a08152f327e040028bef373dfbb4a28f.jpg',1),
(57,36,14,N'Tốm sốt thái',          N'',79000.00, N'/CatBaBooking/uploads/dishes/9f8dcfb9ac954e3493252d2191478589.jpg',1);
SET IDENTITY_INSERT dishes OFF;
GO

-- ============================================================
-- Table: bookings
-- enum -> NVARCHAR + CHECK
-- COMMENT on column -> bỏ (dùng -- comment)
-- ============================================================
CREATE TABLE bookings (
    booking_id             INT           NOT NULL IDENTITY(1,1),
    booking_code           NVARCHAR(100) NOT NULL,
    user_id                INT           NULL,
    business_id            INT           NOT NULL,
    booker_name            NVARCHAR(255) NOT NULL,
    booker_email           NVARCHAR(255) NOT NULL,
    booker_phone           NVARCHAR(20)  NOT NULL,
    num_guests             INT           NOT NULL,
    total_price            DECIMAL(12,2) NOT NULL,
    paid_amount            DECIMAL(12,2) NULL DEFAULT 0.00,
    payment_status         NVARCHAR(20)  NULL DEFAULT 'unpaid',
    notes                  NVARCHAR(MAX) NULL,
    reservation_start_time DATETIME2     NULL,
    reservation_end_time   DATETIME2     NULL,
    status                 NVARCHAR(30)  NOT NULL DEFAULT 'pending',
    reservation_time       TIME          NULL,   -- Giờ đặt bàn cho restaurant (HH:MM)
    reservation_date       DATE          NULL,   -- Ngày đặt bàn cho restaurant
    updated_at             DATETIME2     NULL DEFAULT GETDATE(),
    created_at             DATETIME2     NULL DEFAULT GETDATE(),
    CONSTRAINT PK_bookings              PRIMARY KEY (booking_id),
    CONSTRAINT UQ_bookings_code         UNIQUE (booking_code),
    CONSTRAINT CK_bookings_pay_status   CHECK (payment_status IN ('unpaid','partially_paid','fully_paid','refunded')),
    CONSTRAINT CK_bookings_status       CHECK (status IN ('pending','confirmed','cancelled_by_user','cancelled_by_owner','completed','no_show')),
    CONSTRAINT FK_bookings_user         FOREIGN KEY (user_id)     REFERENCES users(user_id)          ON DELETE SET NULL,
    -- ON DELETE NO ACTION thay vì CASCADE để tránh lỗi "multiple cascade paths":
    -- users -> businesses -> bookings (CASCADE) VÀ users -> bookings (SET NULL) -> conflict!
    CONSTRAINT FK_bookings_business     FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE NO ACTION
);
GO

CREATE OR ALTER TRIGGER trg_bookings_updated_at
ON bookings AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE b SET b.updated_at = GETDATE()
    FROM bookings b INNER JOIN inserted i ON b.booking_id = i.booking_id;
END;
GO

SET IDENTITY_INSERT bookings ON;
INSERT INTO bookings (booking_id,booking_code,user_id,business_id,booker_name,booker_email,booker_phone,num_guests,total_price,paid_amount,payment_status,notes,reservation_start_time,reservation_end_time,status,reservation_time,reservation_date,updated_at,created_at) VALUES
(12,N'BKD57DFCA22487', NULL,4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'22:00:00','2025-11-07','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(13,N'BK125DDD122790', NULL,4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-07','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(14,N'BKE2E7157A1701', NULL,4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-07','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(15,N'BKBF4BB8894921', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'22:00:00','2025-11-07','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(16,N'BK5A27C9916840', NULL,4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:13:00','2025-11-11','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(17,N'BKFB53E45C9901', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-15','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(18,N'BKD6EA7F494583', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'18:00:00','2025-11-11','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(19,N'BK7A9C02ED1445', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',4,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-12','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(20,N'BKC6BD88D29878', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',6,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:30:00','2025-11-12','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(21,N'BKEED58C175946', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-13','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(22,N'BK92C94D0E1205', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:30:00','2025-11-13','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(23,N'BKBA2851774981', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'19:00:00','2025-11-11','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(24,N'BKC3AB57C66645', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'19:00:00','2025-11-13','2025-11-10 03:17:04','2025-11-06 23:06:35'),
(25,N'BKAF3C7E678304', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,20000.00,  20000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:30:00','2025-11-30','2025-11-06 23:14:09','2025-11-06 23:13:28'),
(26,N'BKB0257BDD0648', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,295000.00, 0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-20','2025-11-10 03:17:04','2025-11-06 23:40:40'),
(27,N'BK94857F858031', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:00:00','2025-11-21','2025-11-06 23:50:46','2025-11-06 23:50:28'),
(28,N'BK32D799874883', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,385000.00, 385000.00,N'fully_paid',N'',NULL,NULL,N'confirmed',N'09:00:00','2025-11-22','2025-11-07 00:01:02','2025-11-07 00:00:44'),
(29,N'BKA706C8B38147', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'22:00:00','2025-11-21','2025-11-07 00:06:03','2025-11-07 00:05:28'),
(30,N'BKD0C04F3F5719', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0334403380',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'21:00:00','2025-11-16','2025-11-10 03:17:04','2025-11-07 00:08:35'),
(31,N'BKA6AEA0661321', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:00:00','2025-11-19','2025-11-07 03:06:54','2025-11-07 03:06:41'),
(32,N'BKD0D8B03B8407', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',3,20000.00,  20000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:00:00','2025-11-18','2025-11-07 04:01:11','2025-11-07 04:00:38'),
(33,N'BKD932BF444829', NULL,4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:00:00','2025-11-09','2025-11-10 03:17:04','2025-11-09 16:48:04'),
(34,N'BK56925F808774', NULL,4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'21:00:00','2025-11-09','2025-11-10 03:17:04','2025-11-09 16:49:28'),
(35,N'BK6B86D57D2584', NULL,4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,249000.00, 0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'08:30:00','2025-11-18','2025-11-10 03:17:04','2025-11-09 17:21:22'),
(36,N'BKA8F33A524298', NULL,4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,385000.00, 0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:30:00','2025-11-15','2025-11-10 03:17:04','2025-11-09 17:31:24'),
(37,N'BK5650F8035241', NULL,4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,249000.00, 0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'10:00:00','2025-11-10','2025-11-10 03:30:29','2025-11-10 03:25:05'),
(38,N'BK4330EE025754', NULL,4,N'Nguyễn Văn Tiến', N'nangdz14@gmail.com',       N'0327724169',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'13:00:00','2025-11-10','2025-11-10 03:54:29','2025-11-10 03:48:35'),
(39,N'BKCC2F65F86941', NULL,4,N'Nguyễn Văn Đạt', N'nangdz14@gmail.com',       N'0327752851',4,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:00:00','2025-11-10','2025-11-10 11:57:43','2025-11-10 11:57:06'),
(40,N'BKC49C72366157', 8,   4,N'Phạm Vy',         N'phamthituongvy30112004@gmail.com',N'0986868686',2,249000.00,0.00,N'refunded',N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'21:00:00','2025-11-10','2025-11-10 15:31:12','2025-11-10 15:26:06'),
(41,N'BKE0E0CF404229', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0986868686',2,10000.00,  0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:30:00','2025-11-12','2025-11-10 18:32:12','2025-11-10 18:27:04'),
(42,N'BK97285A8B3381', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0986868686',2,20000.00,  20000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'12:30:00','2025-11-13','2025-11-13 14:14:49','2025-11-13 14:14:23'),
(43,N'BKAA67E4221278', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0986868686',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'11:30:00','2025-11-13','2025-11-13 14:30:36','2025-11-13 14:30:11'),
(44,N'BKEE5205708644', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0986868686',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'21:30:00','2025-11-13','2025-11-13 14:32:32','2025-11-13 14:32:18'),
(45,N'BK352DCBC83389', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0986868686',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:00:00','2025-11-13','2025-11-13 15:00:18','2025-11-13 14:59:53'),
(46,N'BK9F1ABC985437', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0986868686',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:30:00','2025-11-13','2025-11-13 15:21:07','2025-11-13 15:20:55'),
(47,N'BK0698E0F34645', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,259000.00, 259000.00,N'fully_paid',N'',NULL,NULL,N'confirmed',N'21:30:00','2025-11-13','2025-11-13 15:27:13','2025-11-13 15:27:04'),
(48,N'BKA5A42A733091', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'21:30:00','2025-11-13','2025-11-13 15:29:44','2025-11-13 15:29:33'),
(49,N'BKC4D7BE455073', 2,   4,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:30:00','2025-11-20','2025-11-13 15:32:55','2025-11-13 15:32:35'),
(50,N'BK6C5166325273', 2,   4,N'Dao Van Nang',     N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'21:30:00','2025-11-24','2025-11-13 15:39:12','2025-11-13 15:38:55'),
(51,N'HSC6D2425F2502', NULL,2,N'Đào Văn Năng',    N'nangdvhe187101@fpt.edu.vn',N'0327724169',2,1700000.00,0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán','2025-11-15 07:00:00','2025-11-17 05:00:00',N'cancelled_by_owner',NULL,NULL,'2025-11-14 01:33:11','2025-11-14 01:27:52'),
(52,N'BK12D52BD28069', 19,  4,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',       N'0327724169',2,10000.00,  10000.00, N'fully_paid',N'',NULL,NULL,N'confirmed',N'20:00:00','2025-11-14','2025-11-14 03:41:00','2025-11-14 03:39:38'),
(53,N'BKF7458D3F2670', NULL,32,N'Nguyễn Nhật Minh',N'nangdz14@gmail.com',      N'0327724169',8,249000.00, 0.00,     N'refunded',  N'[AUTO] Quá 5 phút không thanh toán',NULL,NULL,N'cancelled_by_owner',N'20:21:00','2025-11-22','2025-11-14 20:28:03','2025-11-14 20:22:22');
SET IDENTITY_INSERT bookings OFF;
GO

-- ============================================================
-- Table: booked_rooms
-- ============================================================
CREATE TABLE booked_rooms (
    booked_room_id   INT           NOT NULL IDENTITY(1,1),
    booking_id       INT           NOT NULL,
    room_id          INT           NOT NULL,
    price_at_booking DECIMAL(12,2) NOT NULL,
    CONSTRAINT PK_booked_rooms PRIMARY KEY (booked_room_id),
    CONSTRAINT FK_br_booking   FOREIGN KEY (booking_id) REFERENCES bookings(booking_id) ON DELETE CASCADE,
    CONSTRAINT FK_br_room      FOREIGN KEY (room_id)    REFERENCES rooms(room_id)        ON DELETE NO ACTION
);
GO
SET IDENTITY_INSERT booked_rooms ON;
INSERT INTO booked_rooms (booked_room_id,booking_id,room_id,price_at_booking) VALUES (1,51,1,850000.00);
SET IDENTITY_INSERT booked_rooms OFF;
GO

-- ============================================================
-- Table: booked_tables
-- ============================================================
CREATE TABLE booked_tables (
    booked_table_id INT NOT NULL IDENTITY(1,1),
    booking_id      INT NOT NULL,
    table_id        INT NOT NULL,
    CONSTRAINT PK_booked_tables PRIMARY KEY (booked_table_id),
    CONSTRAINT FK_bt_booking    FOREIGN KEY (booking_id) REFERENCES bookings(booking_id)        ON DELETE CASCADE,
    CONSTRAINT FK_bt_table      FOREIGN KEY (table_id)   REFERENCES restaurant_tables(table_id) ON DELETE NO ACTION
);
GO
SET IDENTITY_INSERT booked_tables ON;
INSERT INTO booked_tables (booked_table_id,booking_id,table_id) VALUES
(9,12,1),(10,13,1),(11,14,3),(12,15,1),(13,16,1),(14,17,1),(15,18,1),(16,19,1),(17,20,7),(18,21,1),
(19,22,3),(20,23,3),(21,24,4),(22,25,1),(23,26,1),(24,27,1),(25,28,1),(26,29,1),(27,30,1),(28,31,1),
(29,32,1),(30,33,1),(31,34,3),(32,35,1),(33,36,3),(34,37,1),(35,38,1),(36,39,1),(37,40,3),(38,41,1),
(39,42,1),(40,43,3),(41,44,1),(42,45,3),(43,46,4),(44,47,11),(45,48,7),(46,49,1),(47,50,1),(48,52,1),(49,53,20);
SET IDENTITY_INSERT booked_tables OFF;
GO

-- ============================================================
-- Table: booking_dishes
-- ============================================================
CREATE TABLE booking_dishes (
    booking_dish_id  INT           NOT NULL IDENTITY(1,1),
    booking_id       INT           NOT NULL,
    dish_id          INT           NULL,
    quantity         INT           NOT NULL,
    price_at_booking DECIMAL(12,2) NOT NULL,
    notes            NVARCHAR(500) NULL,
    CONSTRAINT PK_booking_dishes PRIMARY KEY (booking_dish_id),
    CONSTRAINT FK_bdd_booking    FOREIGN KEY (booking_id) REFERENCES bookings(booking_id) ON DELETE CASCADE,
    CONSTRAINT FK_bdd_dish       FOREIGN KEY (dish_id)    REFERENCES dishes(dish_id)       ON DELETE SET NULL
);
GO
SET IDENTITY_INSERT booking_dishes ON;
INSERT INTO booking_dishes (booking_dish_id,booking_id,dish_id,quantity,price_at_booking,notes) VALUES
(11,12,3,1,10000.00,N''),(12,13,3,1,10000.00,N''),(13,14,3,1,10000.00,N''),(14,15,3,1,10000.00,N''),(15,16,3,1,10000.00,N''),(16,17,3,1,10000.00,N''),(17,18,3,1,10000.00,N''),(18,19,3,1,10000.00,N'nướng kĩ'),(19,20,3,1,10000.00,N''),(20,21,3,1,10000.00,N'Nướng Xém'),
(21,22,3,1,10000.00,N''),(22,23,3,1,10000.00,N''),(23,24,3,1,10000.00,N''),(24,25,3,2,10000.00,N''),(25,26,3,1,10000.00,N''),(26,26,4,1,285000.00,N''),(27,27,3,1,10000.00,N''),(28,28,13,1,385000.00,N''),(29,29,3,1,10000.00,N''),(30,30,3,1,10000.00,N''),
(31,31,3,1,10000.00,N''),(32,32,3,2,10000.00,N''),(33,33,3,1,10000.00,N''),(34,34,3,1,10000.00,N''),(35,35,2,1,249000.00,N''),(36,36,13,1,385000.00,N''),(37,37,6,1,249000.00,N'ít chua'),(38,38,3,1,10000.00,N''),(39,39,3,1,10000.00,N''),(40,40,2,1,249000.00,N''),
(41,41,3,1,10000.00,N''),(42,42,3,2,10000.00,N''),(43,43,3,1,10000.00,N''),(44,44,3,1,10000.00,N''),(45,45,3,1,10000.00,N''),(46,46,3,1,10000.00,N''),(47,47,3,1,10000.00,N''),(48,47,6,1,249000.00,N''),(49,48,3,1,10000.00,N''),(50,49,3,1,10000.00,N''),
(51,50,3,1,10000.00,N''),(52,52,3,1,10000.00,N''),(53,53,26,1,249000.00,N'');
SET IDENTITY_INSERT booking_dishes OFF;
GO

-- ============================================================
-- Table: payments
-- enum('pending','completed','failed','refunded') -> NVARCHAR(15) + CHECK
-- ON DELETE RESTRICT -> ON DELETE NO ACTION (SQL Server default)
-- ============================================================
CREATE TABLE payments (
    payment_id       INT           NOT NULL IDENTITY(1,1),
    booking_id       INT           NOT NULL,
    amount           DECIMAL(12,2) NOT NULL,
    payment_method   NVARCHAR(100) NULL,
    status           NVARCHAR(15)  NOT NULL,
    transaction_code NVARCHAR(255) NULL,
    gateway_response NVARCHAR(MAX) NULL,
    paid_at          DATETIME2     NULL,
    created_at       DATETIME2     NULL DEFAULT GETDATE(),
    updated_at       DATETIME2     NULL DEFAULT GETDATE(),
    CONSTRAINT PK_payments         PRIMARY KEY (payment_id),
    CONSTRAINT CK_payments_status  CHECK (status IN ('pending','completed','failed','refunded')),
    CONSTRAINT FK_payments_booking FOREIGN KEY (booking_id) REFERENCES bookings(booking_id) ON DELETE NO ACTION
);
GO

CREATE OR ALTER TRIGGER trg_payments_updated_at
ON payments AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE p SET p.updated_at = GETDATE()
    FROM payments p INNER JOIN inserted i ON p.payment_id = i.payment_id;
END;
GO

SET IDENTITY_INSERT payments ON;
INSERT INTO payments (payment_id,booking_id,amount,payment_method,status,transaction_code,gateway_response,paid_at,created_at,updated_at) VALUES
(17,12,10000.00, N'cash', N'failed', NULL,N'[AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 11:20:22','2025-11-10 03:17:04'),
(18,12,10000.00, N'sepay',N'failed', NULL,N'BKD57DFCA22487 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 11:20:23','2025-11-10 03:17:04'),
(19,13,10000.00, N'sepay',N'failed', NULL,N'BK125DDD122790 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 12:44:43','2025-11-10 03:17:04'),
(20,14,10000.00, N'sepay',N'failed', NULL,N'BKE2E7157A1701 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 12:52:22','2025-11-10 03:17:04'),
(21,15,10000.00, N'sepay',N'failed', NULL,N'BKBF4BB8894921 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 13:07:45','2025-11-10 03:17:04'),
(22,17,10000.00, N'sepay',N'failed', NULL,N'BKFB53E45C9901 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 15:55:10','2025-11-10 03:17:04'),
(23,18,10000.00, N'sepay',N'failed', NULL,N'BKD6EA7F494583 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 20:59:25','2025-11-10 03:17:04'),
(24,19,10000.00, N'sepay',N'failed', NULL,N'BK7A9C02ED1445 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 21:17:01','2025-11-10 03:17:04'),
(25,20,10000.00, N'sepay',N'failed', NULL,N'BKC6BD88D29878 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 21:24:40','2025-11-10 03:17:04'),
(26,21,10000.00, N'sepay',N'failed', NULL,N'BKEED58C175946 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 21:34:06','2025-11-10 03:17:04'),
(27,22,10000.00, N'sepay',N'failed', NULL,N'BK92C94D0E1205 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 21:40:21','2025-11-10 03:17:04'),
(28,23,10000.00, N'sepay',N'failed', NULL,N'BKBA2851774981 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 21:44:35','2025-11-10 03:17:04'),
(29,24,10000.00, N'sepay',N'failed', NULL,N'BKC3AB57C66645 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 22:41:46','2025-11-10 03:17:04'),
(30,24,10000.00, N'sepay',N'failed', NULL,N'BKC3AB57C66645 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 22:41:47','2025-11-10 03:17:04'),
(31,25,20000.00, N'sepay',N'completed',N'669V501253110065_29369229',N'{"gateway":"TPBank","transactionDate":"2025-11-07 06:14:08","accountNumber":"00000807297","subAccount":"DVN","content":"TKPDVN BKAF3C7E678304","transferAmount":20000,"referenceCode":"669V501253110065","id":29369229}','2025-11-06 23:14:09','2025-11-06 23:13:28','2025-11-06 23:14:08'),
(32,25,20000.00, N'sepay',N'pending',NULL,N'BKAF3C7E678304',NULL,'2025-11-06 23:13:28','2025-11-06 23:13:28'),
(33,26,295000.00,N'sepay',N'failed', NULL,N'BKB0257BDD0648 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 23:40:40','2025-11-10 03:17:04'),
(34,26,295000.00,N'sepay',N'failed', NULL,N'BKB0257BDD0648 [AUTO] Expired - Quá 5 phút không thanh toán',NULL,'2025-11-06 23:40:41','2025-11-10 03:17:04'),
(35,27,10000.00, N'sepay',N'completed',N'669V501253110075_29370976',N'{"gateway":"TPBank","content":"TKPDVN BK94857F858031","transferAmount":10000,"id":29370976}','2025-11-06 23:50:46','2025-11-06 23:50:28','2025-11-06 23:50:46'),
(36,28,385000.00,N'sepay',N'completed',N'669V501253110081_29371720',N'{"gateway":"TPBank","content":"TKPDVN BK32D799874883","transferAmount":385000,"id":29371720}','2025-11-07 00:01:03','2025-11-07 00:00:44','2025-11-07 00:01:02'),
(37,29,10000.00, N'sepay',N'completed',N'669V501253110084_29372085',N'{"gateway":"TPBank","content":"TKPDVN BKA706C8B38147","transferAmount":10000,"id":29372085}','2025-11-07 00:06:04','2025-11-07 00:05:28','2025-11-07 00:06:03'),
(38,30,10000.00, N'sepay',N'failed', NULL,N'BKD0C04F3F5719 [AUTO] Expired',NULL,'2025-11-07 00:08:35','2025-11-10 03:17:04'),
(39,31,10000.00, N'sepay',N'completed',N'066ITC1253110486_29390673',N'{"gateway":"TPBank","content":"TKPDVN BKA6AEA0661321","transferAmount":10000,"id":29390673}','2025-11-07 03:06:55','2025-11-07 03:06:41','2025-11-07 03:06:54'),
(40,32,20000.00, N'sepay',N'completed',N'669V501253110300_29397496',N'{"gateway":"TPBank","content":"TKPDVN BKD0D8B03B8407","transferAmount":20000,"id":29397496}','2025-11-07 04:01:11','2025-11-07 04:00:38','2025-11-07 04:01:11'),
(41,33,10000.00, N'sepay',N'failed', NULL,N'BKD932BF444829 [AUTO] Expired',NULL,'2025-11-09 16:48:04','2025-11-10 03:17:04'),
(42,34,10000.00, N'sepay',N'failed', NULL,N'BK56925F808774 [AUTO] Expired',NULL,'2025-11-09 16:49:28','2025-11-10 03:17:04'),
(43,35,249000.00,N'sepay',N'failed', NULL,N'BK6B86D57D2584 [AUTO] Expired',NULL,'2025-11-09 17:21:22','2025-11-10 03:17:04'),
(44,36,385000.00,N'sepay',N'failed', NULL,N'BKA8F33A524298 [AUTO] Expired',NULL,'2025-11-09 17:31:24','2025-11-10 03:17:04'),
(45,37,249000.00,N'sepay',N'failed', NULL,N'BK5650F8035241 [AUTO] Expired',NULL,'2025-11-10 03:25:05','2025-11-10 03:30:29'),
(46,38,10000.00, N'sepay',N'failed', NULL,N'BK4330EE025754 [AUTO] Expired',NULL,'2025-11-10 03:48:35','2025-11-10 03:54:29'),
(47,39,10000.00, N'sepay',N'completed',N'669V501253142076_29932965',N'{"gateway":"TPBank","content":"TKPDVN BKCC2F65F86941","transferAmount":10000,"id":29932965}','2025-11-10 11:57:43','2025-11-10 11:57:06','2025-11-10 11:57:43'),
(48,40,249000.00,N'sepay',N'failed', NULL,N'BKC49C72366157 [AUTO] Expired',NULL,'2025-11-10 15:26:06','2025-11-10 15:31:12'),
(49,41,10000.00, N'sepay',N'failed', NULL,N'BKE0E0CF404229 [AUTO] Expired',NULL,'2025-11-10 18:27:04','2025-11-10 18:32:12'),
(50,42,20000.00, N'sepay',N'completed',N'669V501253170958_30429473',N'{"gateway":"TPBank","content":"TKPDVN BK97285A8B3381","transferAmount":20000,"id":30429473}','2025-11-13 14:14:49','2025-11-13 14:14:23','2025-11-13 14:14:49'),
(51,43,10000.00, N'sepay',N'completed',N'669V501253170971_30431995',N'{"gateway":"TPBank","content":"TKPDVN BKAA67E4221278","transferAmount":10000,"id":30431995}','2025-11-13 14:30:37','2025-11-13 14:30:11','2025-11-13 14:30:36'),
(52,44,10000.00, N'sepay',N'completed',N'669V501253170972_30432321',N'{"gateway":"TPBank","content":"TKPDVN BKEE5205708644","transferAmount":10000,"id":30432321}','2025-11-13 14:32:31','2025-11-13 14:32:18','2025-11-13 14:32:31'),
(53,45,10000.00, N'sepay',N'completed',N'669V501253170989_30436514',N'{"gateway":"TPBank","content":"TKPDVN BK352DCBC83389","transferAmount":10000,"id":30436514}','2025-11-13 15:00:19','2025-11-13 14:59:53','2025-11-13 15:00:18'),
(54,46,10000.00, N'sepay',N'completed',N'669V501253180005_30439066',N'{"gateway":"TPBank","content":"TKPDVN BK9F1ABC985437","transferAmount":10000,"id":30439066}','2025-11-13 15:21:07','2025-11-13 15:20:55','2025-11-13 15:21:07'),
(55,47,259000.00,N'sepay',N'completed',N'669V501253180012_30439794',N'{"gateway":"TPBank","content":"TKPDVN BK0698E0F34645","transferAmount":259000,"id":30439794}','2025-11-13 15:27:14','2025-11-13 15:27:04','2025-11-13 15:27:13'),
(56,48,10000.00, N'sepay',N'completed',N'669V501253180015_30440104',N'{"gateway":"TPBank","content":"TKPDVN BKA5A42A733091","transferAmount":10000,"id":30440104}','2025-11-13 15:29:45','2025-11-13 15:29:33','2025-11-13 15:29:44'),
(57,49,10000.00, N'sepay',N'completed',N'669V501253180018_30440543',N'{"gateway":"TPBank","content":"TKPDVN BKC4D7BE455073","transferAmount":10000,"id":30440543}','2025-11-13 15:32:55','2025-11-13 15:32:35','2025-11-13 15:32:55'),
(58,50,10000.00, N'sepay',N'completed',N'669V501253180025_30441279',N'{"gateway":"TPBank","content":"TKPDVN BK6C5166325273","transferAmount":10000,"id":30441279}','2025-11-13 15:39:12','2025-11-13 15:38:55','2025-11-13 15:39:12'),
(59,51,1700000.00,N'sepay',N'failed',NULL,N'HSC6D2425F2502 [AUTO] Expired',NULL,'2025-11-14 01:27:52','2025-11-14 01:33:11'),
(60,52,10000.00, N'sepay',N'completed',N'669V501253180287_30492685',N'{"gateway":"TPBank","content":"TKPDVN BK12D52BD28069","transferAmount":10000,"id":30492685}','2025-11-14 03:41:01','2025-11-14 03:39:38','2025-11-14 03:41:00'),
(61,53,249000.00,N'sepay',N'failed',NULL,N'BKF7458D3F2670 [AUTO] Expired',NULL,'2025-11-14 20:22:22','2025-11-14 20:28:03');
SET IDENTITY_INSERT payments OFF;
GO

-- ============================================================
-- Table: reviews
-- tinyint -> TINYINT
-- ============================================================
CREATE TABLE reviews (
    review_id   INT           NOT NULL IDENTITY(1,1),
    booking_id  INT           NULL,
    business_id INT           NOT NULL,
    user_id     INT           NOT NULL,
    rating      TINYINT       NOT NULL,
    comment     NVARCHAR(MAX) NULL,
    created_at  DATETIME2     NULL DEFAULT GETDATE(),
    CONSTRAINT PK_reviews          PRIMARY KEY (review_id),
    CONSTRAINT UQ_reviews_booking  UNIQUE (booking_id),
    CONSTRAINT FK_reviews_business FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE CASCADE,
    CONSTRAINT FK_reviews_user     FOREIGN KEY (user_id)     REFERENCES users(user_id)           ON DELETE NO ACTION,
    CONSTRAINT FK_reviews_booking  FOREIGN KEY (booking_id)  REFERENCES bookings(booking_id)     ON DELETE SET NULL
);
GO
-- Không có dữ liệu INSERT cho reviews

-- ============================================================
-- Table: temp_carts
-- ============================================================
CREATE TABLE temp_carts (
    cart_id     INT           NOT NULL IDENTITY(1,1),
    user_id     INT           NOT NULL,
    business_id INT           NOT NULL,
    dish_id     INT           NOT NULL,
    quantity    INT           NOT NULL DEFAULT 1,
    notes       NVARCHAR(500) NULL,
    subtotal    DECIMAL(12,2) NOT NULL,
    created_at  DATETIME2     NULL DEFAULT GETDATE(),
    updated_at  DATETIME2     NULL DEFAULT GETDATE(),
    CONSTRAINT PK_temp_carts   PRIMARY KEY (cart_id),
    CONSTRAINT UQ_temp_carts   UNIQUE (user_id, business_id, dish_id),
    CONSTRAINT FK_tc_user      FOREIGN KEY (user_id)     REFERENCES users(user_id)          ON DELETE CASCADE,
    CONSTRAINT FK_tc_business  FOREIGN KEY (business_id) REFERENCES businesses(business_id) ON DELETE NO ACTION,
    CONSTRAINT FK_tc_dish      FOREIGN KEY (dish_id)     REFERENCES dishes(dish_id)          ON DELETE NO ACTION
);
GO

CREATE OR ALTER TRIGGER trg_temp_carts_updated_at
ON temp_carts AFTER UPDATE AS
BEGIN
    SET NOCOUNT ON;
    UPDATE tc SET tc.updated_at = GETDATE()
    FROM temp_carts tc INNER JOIN inserted i ON tc.cart_id = i.cart_id;
END;
GO
-- Không có dữ liệu INSERT cho temp_carts

-- ============================================================
-- Bật lại kiểm tra constraint
-- ============================================================
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
GO

PRINT 'CatBaBooking (SQL Server) - Tạo database thành công!';
GO
