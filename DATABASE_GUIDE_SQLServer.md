# 📚 CatBaBooking – Hướng Dẫn Database (SQL Server)

> **Dành cho:** Đội Backend
> **Database:** `catbabooking` (SQL Server)
> **File script:** `catbabooking_for_SQLServer.sql`
> **Cập nhật lần cuối:** 2025-11-15

---

## 📋 Mục Lục

1. [Tổng Quan Hệ Thống](#1-tổng-quan-hệ-thống)
2. [Cài Đặt & Khởi Tạo Database](#2-cài-đặt--khởi-tạo-database)
3. [Sơ Đồ Quan Hệ Bảng (ERD)](#3-sơ-đồ-quan-hệ-bảng-erd)
4. [Mô Tả Chi Tiết Từng Bảng](#4-mô-tả-chi-tiết-từng-bảng)
5. [Giá Trị ENUM (CHECK Constraint)](#5-giá-trị-enum-check-constraint)
6. [Triggers (Tự Cập Nhật updated_at)](#6-triggers-tự-cập-nhật-updated_at)
7. [Quy Tắc IDENTITY & INSERT Data](#7-quy-tắc-identity--insert-data)
8. [Cascade Behavior (FK Actions)](#8-cascade-behavior-fk-actions)
9. [Các Query Thường Dùng](#9-các-query-thường-dùng)
10. [Quy Ước Đặt Tên](#10-quy-ước-đặt-tên)
11. [Lưu Ý Quan Trọng](#11-lưu-ý-quan-trọng)

---

## 1. Tổng Quan Hệ Thống

**CatBaBooking** là hệ thống đặt phòng homestay và đặt bàn nhà hàng cho khu du lịch Cát Bà.

### Các nhóm chức năng chính

| Nhóm | Bảng liên quan |
|------|---------------|
| **Quản lý người dùng** | `users`, `roles`, `features`, `roles_features` |
| **Quản lý doanh nghiệp** | `businesses`, `areas`, `amenities`, `business_amenities`, `cuisine_types`, `business_cuisines`, `occasions`, `business_occasions`, `restaurant_types`, `business_restaurant_types` |
| **Quản lý phòng homestay** | `rooms`, `room_images`, `room_availability` |
| **Quản lý nhà hàng** | `restaurant_tables`, `table_availability`, `dish_categories`, `dishes` |
| **Đặt chỗ** | `bookings`, `booked_rooms`, `booked_tables`, `booking_dishes` |
| **Thanh toán & đánh giá** | `payments`, `reviews` |
| **Giỏ hàng tạm** | `temp_carts` |

### Phân loại người dùng (`roles`)

| role_id | role_name | Quyền |
|---------|-----------|-------|
| 1 | `customer` | Đặt phòng, đặt bàn, xem menu |
| 2 | `owner homestay` | Quản lý phòng, xem booking homestay |
| 3 | `admin` | Quản trị toàn hệ thống |
| 4 | `owner restaurant` | Quản lý thực đơn, bàn, xem booking nhà hàng |

---

## 2. Cài Đặt & Khởi Tạo Database

### Yêu cầu
- SQL Server 2019+ (hoặc SQL Server 2016+)
- SQL Server Management Studio (SSMS) hoặc Azure Data Studio

### Các bước chạy lần đầu

```sql
-- Bước 1: Mở SSMS, kết nối vào server
-- Bước 2: Mở file catbabooking_for_SQLServer.sql
-- Bước 3: Nhấn F5 hoặc Execute để chạy toàn bộ script
```

Script sẽ tự động:
1. Tạo database `catbabooking` (collation `Vietnamese_CI_AS`) nếu chưa tồn tại
2. Drop tất cả bảng cũ theo đúng thứ tự FK
3. Tạo lại toàn bộ bảng + constraints + triggers
4. Nạp dữ liệu mẫu (seed data)

### Kết nối từ C# / ASP.NET

```json
// appsettings.json – Windows Auth
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=catbabooking;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

```json
// appsettings.json – SQL Auth
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=catbabooking;User Id=sa;Password=YOUR_PASS;TrustServerCertificate=True;"
  }
}
```

---

## 3. Sơ Đồ Quan Hệ Bảng

```
roles (1) ──── (N) roles_features (N) ──── (1) features
  |
  | (1)
  |
users (1) ─────────────────────────── (N) businesses (1) ──── (N) rooms
  |                                          |                        |
  | (N)                                      | (N)               room_images
  |                                          |                   room_availability
bookings (1) ──── (N) booked_rooms ──────────|
          |──── (N) booked_tables            | (N) restaurant_tables
          |──── (N) booking_dishes           |         |
          |──── (N) payments            table_availability
          └──── (1) reviews
                                       businesses (N:N junctions):
rooms (1) ──── (N) room_images           business_amenities  <-> amenities
               room_availability         business_cuisines   <-> cuisine_types
                                         business_occasions  <-> occasions
dish_categories (1) ──── (N) dishes      business_restaurant_types <-> restaurant_types
                              |          businesses.area_id -> areas
                  booking_dishes (N)

users (N) ──── (N) temp_carts ──── (N) dishes
                      |
                 businesses (N)
```

---

## 4. Mô Tả Chi Tiết Từng Bảng

---

### roles – Vai trò người dùng

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `role_id` | INT IDENTITY PK | |
| `role_name` | NVARCHAR(50) UNIQUE NOT NULL | |
| `description` | NVARCHAR(255) NULL | |
| `created_at` | DATETIME2 DEFAULT GETDATE() | |

---

### users – Người dùng

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `user_id` | INT IDENTITY PK | |
| `role_id` | INT FK→roles | Không CASCADE |
| `full_name` | NVARCHAR(255) NOT NULL | |
| `email` | NVARCHAR(255) UNIQUE NOT NULL | |
| `password_hash` | NVARCHAR(255) NOT NULL | Hash bằng Argon2id |
| `phone` | NVARCHAR(20) NULL | |
| `citizen_id` | NVARCHAR(50) NULL | CCCD/CMND |
| `personal_address` | NVARCHAR(500) NULL | |
| `status` | NVARCHAR(10) DEFAULT 'active' | `active`/`pending`/`rejected` |
| `created_at` | DATETIME2 | |
| `updated_at` | DATETIME2 | Trigger tự cập nhật |

Password được hash bằng Argon2id. Tuyệt đối không lưu plain-text.

---

### businesses – Doanh nghiệp (Homestay / Nhà hàng)

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `business_id` | INT IDENTITY PK | |
| `owner_id` | INT FK→users CASCADE | Chủ sở hữu |
| `name` | NVARCHAR(255) NOT NULL | |
| `type` | NVARCHAR(20) NOT NULL | `homestay` / `restaurant` |
| `address` | NVARCHAR(500) NOT NULL | |
| `description` | NVARCHAR(MAX) NOT NULL | |
| `image` | NVARCHAR(500) NULL | URL ảnh đại diện |
| `area_id` | INT FK→areas SET NULL | Khu vực địa lý |
| `avg_rating` | DECIMAL(3,2) DEFAULT 0.00 | Sao trung bình |
| `review_count` | INT DEFAULT 0 | Số lượt đánh giá |
| `capacity` | INT NULL | Sức chứa (homestay) |
| `num_bedrooms` | INT NULL | Số phòng ngủ (homestay) |
| `price_per_night` | DECIMAL(12,2) NULL | Giá/đêm (homestay) |
| `status` | NVARCHAR(10) DEFAULT 'pending' | `active`/`pending`/`rejected` |
| `created_at` | DATETIME2 | |
| `updated_at` | DATETIME2 | Trigger tự cập nhật |
| `opening_hour` | TIME NULL | Giờ mở cửa (nhà hàng) |
| `closing_hour` | TIME NULL | Giờ đóng cửa (nhà hàng) |

Lưu ý: `type = 'homestay'` → dùng capacity, num_bedrooms, price_per_night.
`type = 'restaurant'` → dùng opening_hour, closing_hour.

---

### rooms – Phòng Homestay

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `room_id` | INT IDENTITY PK | |
| `business_id` | INT FK→businesses CASCADE | |
| `name` | NVARCHAR(255) NOT NULL | |
| `capacity` | INT NOT NULL | Số người tối đa |
| `price_per_night` | DECIMAL(12,2) NOT NULL | |
| `is_active` | BIT DEFAULT 1 | 1=hiển thị, 0=ẩn |

---

### room_images – Ảnh Phòng

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `image_id` | INT IDENTITY PK | |
| `room_id` | INT FK→rooms CASCADE | |
| `image_url` | NVARCHAR(500) NOT NULL | URL ảnh |

---

### room_availability – Lịch Phòng

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `availability_id` | BIGINT IDENTITY PK | |
| `room_id` | INT FK→rooms CASCADE | |
| `date` | DATE NOT NULL | Ngày bị chiếm |
| `price` | DECIMAL(12,2) NULL | Giá áp dụng ngày đó |
| `status` | NVARCHAR(10) NOT NULL | `booked` / `blocked` |

UNIQUE(room_id, date) – mỗi phòng mỗi ngày chỉ có 1 record.
`booked` = khách đặt; `blocked` = chủ khóa thủ công.

---

### restaurant_tables – Bàn Nhà Hàng

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `table_id` | INT IDENTITY PK | |
| `business_id` | INT FK→businesses CASCADE | |
| `name` | NVARCHAR(100) NOT NULL | VD: "Bàn 01" |
| `capacity` | INT NOT NULL | Sức chứa (người) |
| `is_active` | BIT DEFAULT 1 | |

---

### table_availability – Lịch Bàn

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `availability_id` | BIGINT IDENTITY PK | |
| `table_id` | INT FK→restaurant_tables CASCADE | |
| `reservation_date` | DATE NOT NULL | Ngày đặt |
| `reservation_time` | TIME NOT NULL | Giờ đặt |
| `status` | NVARCHAR(10) NOT NULL | `booked` / `blocked` |

UNIQUE(table_id, reservation_date, reservation_time)

---

### dish_categories – Danh Mục Món Ăn

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `category_id` | INT IDENTITY PK | |
| `business_id` | INT FK→businesses CASCADE | |
| `name` | NVARCHAR(100) NOT NULL | VD: "Hải Sản", "Lẩu" |
| `display_order` | INT DEFAULT 0 | Thứ tự hiển thị |

---

### dishes – Món Ăn

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `dish_id` | INT IDENTITY PK | |
| `business_id` | INT FK→businesses CASCADE | |
| `category_id` | INT FK→dish_categories SET NULL | NULL = chưa phân loại |
| `name` | NVARCHAR(255) NOT NULL | |
| `description` | NVARCHAR(MAX) NULL | |
| `price` | DECIMAL(12,2) NOT NULL | |
| `image_url` | NVARCHAR(500) NULL | |
| `is_available` | BIT NOT NULL DEFAULT 1 | 0 = hết hàng |

---

### bookings – Đơn Đặt Chỗ

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `booking_id` | INT IDENTITY PK | |
| `booking_code` | NVARCHAR(100) UNIQUE | Mã hiển thị cho khách (BK...) |
| `user_id` | INT FK→users SET NULL | NULL = khách ẩn danh |
| `business_id` | INT FK→businesses CASCADE | |
| `booker_name` | NVARCHAR(255) NOT NULL | |
| `booker_email` | NVARCHAR(255) NOT NULL | |
| `booker_phone` | NVARCHAR(20) NOT NULL | |
| `num_guests` | INT NOT NULL | Số khách |
| `total_price` | DECIMAL(12,2) NOT NULL | Tổng tiền phải trả |
| `paid_amount` | DECIMAL(12,2) DEFAULT 0.00 | Đã thanh toán |
| `payment_status` | NVARCHAR(20) DEFAULT 'unpaid' | Xem mục ENUM |
| `notes` | NVARCHAR(MAX) NULL | Ghi chú |
| `reservation_start_time` | DATETIME2 NULL | Check-in (homestay) |
| `reservation_end_time` | DATETIME2 NULL | Check-out (homestay) |
| `status` | NVARCHAR(30) DEFAULT 'pending' | Xem mục ENUM |
| `reservation_time` | TIME NULL | Giờ đặt bàn (nhà hàng) |
| `reservation_date` | DATE NULL | Ngày đặt bàn (nhà hàng) |
| `updated_at` | DATETIME2 | Trigger tự cập nhật |
| `created_at` | DATETIME2 | |

Phân biệt loại booking:
- Homestay: dùng reservation_start_time, reservation_end_time
- Nhà hàng: dùng reservation_date, reservation_time

---

### booked_rooms – Phòng Trong Đơn Đặt

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `booked_room_id` | INT IDENTITY PK | |
| `booking_id` | INT FK→bookings CASCADE | |
| `room_id` | INT FK→rooms NO ACTION | |
| `price_at_booking` | DECIMAL(12,2) NOT NULL | Snapshot giá tại thời điểm đặt |

---

### booked_tables – Bàn Trong Đơn Đặt

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `booked_table_id` | INT IDENTITY PK | |
| `booking_id` | INT FK→bookings CASCADE | |
| `table_id` | INT FK→restaurant_tables NO ACTION | |

---

### booking_dishes – Món Ăn Trong Đơn Đặt

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `booking_dish_id` | INT IDENTITY PK | |
| `booking_id` | INT FK→bookings CASCADE | |
| `dish_id` | INT FK→dishes SET NULL | NULL nếu món bị xóa sau |
| `quantity` | INT NOT NULL | Số lượng |
| `price_at_booking` | DECIMAL(12,2) NOT NULL | Snapshot giá tại thời điểm đặt |
| `notes` | NVARCHAR(500) NULL | VD: "ít chua", "không hành" |

---

### payments – Lịch Sử Thanh Toán

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `payment_id` | INT IDENTITY PK | |
| `booking_id` | INT FK→bookings NO ACTION | |
| `amount` | DECIMAL(12,2) NOT NULL | Số tiền |
| `payment_method` | NVARCHAR(100) NULL | `sepay`, `cash`, ... |
| `status` | NVARCHAR(15) NOT NULL | `pending`/`completed`/`failed`/`refunded` |
| `transaction_code` | NVARCHAR(255) NULL | Mã giao dịch ngân hàng |
| `gateway_response` | NVARCHAR(MAX) NULL | JSON từ cổng thanh toán |
| `paid_at` | DATETIME2 NULL | Thời điểm thanh toán xong |
| `created_at` | DATETIME2 | |
| `updated_at` | DATETIME2 | Trigger tự cập nhật |

ON DELETE NO ACTION – Không xóa payment khi xóa booking (giữ lịch sử tài chính).

---

### reviews – Đánh Giá

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `review_id` | INT IDENTITY PK | |
| `booking_id` | INT FK→bookings SET NULL UNIQUE | 1 booking chỉ review 1 lần |
| `business_id` | INT FK→businesses CASCADE | |
| `user_id` | INT FK→users NO ACTION | |
| `rating` | TINYINT NOT NULL | 1–5 sao |
| `comment` | NVARCHAR(MAX) NULL | |
| `created_at` | DATETIME2 | |

---

### temp_carts – Giỏ Hàng Tạm (Nhà Hàng)

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `cart_id` | INT IDENTITY PK | |
| `user_id` | INT FK→users CASCADE | |
| `business_id` | INT FK→businesses NO ACTION | |
| `dish_id` | INT FK→dishes NO ACTION | |
| `quantity` | INT NOT NULL DEFAULT 1 | |
| `notes` | NVARCHAR(500) NULL | Ghi chú riêng món |
| `subtotal` | DECIMAL(12,2) NOT NULL | quantity x price (tính từ app) |
| `created_at` | DATETIME2 | |
| `updated_at` | DATETIME2 | Trigger tự cập nhật |

UNIQUE(user_id, business_id, dish_id) – mỗi user chỉ 1 item/món/nhà hàng.

---

### Bảng Phụ Trợ (Lookup / Junction)

| Bảng | Mô tả |
|------|-------|
| `areas` | Khu vực Cát Bà: TT.Cát Bà, Bến Bèo, Làng Việt Hải, ... |
| `amenities` | Tiện nghi: WiFi, bể bơi, bãi đỗ xe, bữa sáng, ... |
| `cuisine_types` | Loại ẩm thực: lẩu, nướng, hải sản |
| `occasions` | Dịp phù hợp: gia đình, cặp đôi, nhóm bạn, công tác, ... |
| `restaurant_types` | Loại nhà hàng: BBQ, hải sản, quán địa phương, ... |
| `features` | Route/tính năng hệ thống (dùng để phân quyền) |
| `business_amenities` | Business <-> Amenities (N:N) |
| `business_cuisines` | Business <-> Cuisine Types (N:N) |
| `business_occasions` | Business <-> Occasions (N:N) |
| `business_restaurant_types` | Business <-> Restaurant Types (N:N) |
| `roles_features` | Role <-> Feature – phân quyền theo route (N:N) |

---

## 5. Giá Trị ENUM (CHECK Constraint)

### users.status & businesses.status
| Giá trị | Ý nghĩa |
|---------|---------|
| `active` | Đang hoạt động bình thường |
| `pending` | Đang chờ admin duyệt |
| `rejected` | Bị từ chối / khóa |

### businesses.type
| Giá trị | Ý nghĩa |
|---------|---------|
| `homestay` | Nhà nghỉ / Homestay |
| `restaurant` | Nhà hàng |

### bookings.payment_status
| Giá trị | Ý nghĩa |
|---------|---------|
| `unpaid` | Chưa thanh toán |
| `partially_paid` | Đã trả một phần |
| `fully_paid` | Đã thanh toán đủ |
| `refunded` | Đã hoàn tiền |

### bookings.status
| Giá trị | Ý nghĩa |
|---------|---------|
| `pending` | Chờ xác nhận thanh toán |
| `confirmed` | Đã xác nhận – thanh toán xong |
| `cancelled_by_user` | Khách hủy |
| `cancelled_by_owner` | Chủ hủy (thường do hết hạn TT) |
| `completed` | Đã hoàn thành lượt nghỉ/ăn |
| `no_show` | Không đến |

### payments.status
| Giá trị | Ý nghĩa |
|---------|---------|
| `pending` | Chờ thanh toán |
| `completed` | Thành công |
| `failed` | Thất bại / hết hạn |
| `refunded` | Đã hoàn tiền |

### room_availability.status & table_availability.status
| Giá trị | Ý nghĩa |
|---------|---------|
| `booked` | Đã được đặt bởi khách |
| `blocked` | Chủ chủ động khóa lịch |

---

## 6. Triggers (Tự Cập Nhật updated_at)

Các bảng sau có AFTER UPDATE trigger tự cập nhật updated_at:

| Trigger | Bảng |
|---------|------|
| `trg_users_updated_at` | `users` |
| `trg_businesses_updated_at` | `businesses` |
| `trg_bookings_updated_at` | `bookings` |
| `trg_payments_updated_at` | `payments` |
| `trg_temp_carts_updated_at` | `temp_carts` |

Backend không cần tự set `updated_at` khi UPDATE. Trigger tự lo.

```sql
-- Xem danh sách trigger
SELECT name, OBJECT_NAME(parent_id) AS table_name
FROM sys.triggers
WHERE type = 'TR';
```

---

## 7. Quy Tắc IDENTITY & INSERT Data

### INSERT thông thường (không cần truyền ID)

```sql
-- SQL Server tự tăng ID
INSERT INTO dishes (business_id, category_id, name, price, is_available)
VALUES (4, 1, N'Gà nướng mật ong', 185000.00, 1);

-- Lấy ID vừa tạo
SELECT SCOPE_IDENTITY();

-- Hoặc dùng OUTPUT (recommend trong transaction)
INSERT INTO bookings (booking_code, business_id, ...)
OUTPUT inserted.booking_id
VALUES (N'BK123456789', 4, ...);
```

### INSERT với ID cụ thể (chỉ dùng khi migrate data)

```sql
SET IDENTITY_INSERT dishes ON;
INSERT INTO dishes (dish_id, business_id, ...) VALUES (99, 4, ...);
SET IDENTITY_INSERT dishes OFF;
```

Chỉ bật IDENTITY_INSERT cho 1 bảng tại một thời điểm trong cùng session.

---

## 8. Cascade Behavior (FK Actions)

### ON DELETE CASCADE – Xóa cha, tự xóa con

```
users        → businesses, bookings, temp_carts
businesses   → rooms, restaurant_tables, dish_categories, dishes,
               bookings, business_amenities, business_cuisines,
               business_occasions, business_restaurant_types, reviews
rooms        → booked_rooms, room_images, room_availability
restaurant_tables → booked_tables, table_availability
bookings     → booked_rooms, booked_tables, booking_dishes
roles        → roles_features
features     → roles_features
```

### ON DELETE SET NULL – Giữ record con, set NULL FK

```
users    → bookings.user_id        (booking vẫn tồn tại dù user bị xóa)
areas    → businesses.area_id
dishes   → booking_dishes.dish_id  (lịch sử đặt vẫn giữ dù món bị xóa)
bookings → reviews.booking_id
dish_categories → dishes.category_id
```

### ON DELETE NO ACTION – Báo lỗi nếu còn FK child

```
rooms             → booked_rooms.room_id      (xóa room khi còn booking sẽ lỗi)
restaurant_tables → booked_tables.table_id
bookings          → payments.booking_id       (giữ lịch sử tài chính)
users             → reviews.user_id
businesses        → temp_carts.business_id
dishes            → temp_carts.dish_id
```

---

## 9. Các Query Thường Dùng

### 9.1 Homestay đang hoạt động theo khu vực

```sql
SELECT b.business_id, b.name, b.address, b.avg_rating,
       b.price_per_night, a.name AS area_name, b.image
FROM businesses b
LEFT JOIN areas a ON b.area_id = a.area_id
WHERE b.type = 'homestay'
  AND b.status = 'active'
ORDER BY b.avg_rating DESC;
```

### 9.2 Kiểm tra phòng còn trống trong khoảng ngày

```sql
SELECT r.room_id, r.name, r.capacity, r.price_per_night
FROM rooms r
WHERE r.business_id = @business_id
  AND r.is_active = 1
  AND r.room_id NOT IN (
      SELECT ra.room_id FROM room_availability ra
      WHERE ra.date >= @checkin AND ra.date < @checkout
        AND ra.status IN ('booked','blocked')
  );
```

### 9.3 Kiểm tra bàn trống theo ngày & giờ

```sql
SELECT t.table_id, t.name, t.capacity
FROM restaurant_tables t
WHERE t.business_id = @business_id
  AND t.is_active = 1
  AND t.capacity >= @num_guests
  AND NOT EXISTS (
      SELECT 1 FROM table_availability ta
      WHERE ta.table_id = t.table_id
        AND ta.reservation_date = @date
        AND ta.reservation_time = @time
  );
```

### 9.4 Chi tiết booking homestay

```sql
SELECT bk.booking_code, bk.booker_name, bk.status, bk.payment_status,
       bk.total_price, bk.paid_amount,
       bk.reservation_start_time, bk.reservation_end_time,
       r.name AS room_name, br.price_at_booking,
       p.payment_method, p.paid_at
FROM bookings bk
JOIN booked_rooms br ON bk.booking_id = br.booking_id
JOIN rooms r         ON br.room_id    = r.room_id
LEFT JOIN payments p ON bk.booking_id = p.booking_id
                    AND p.status = 'completed'
WHERE bk.booking_id = @booking_id;
```

### 9.5 Menu nhà hàng theo danh mục

```sql
SELECT dc.name AS category_name, dc.display_order,
       d.dish_id, d.name AS dish_name, d.price,
       d.description, d.image_url, d.is_available
FROM dish_categories dc
LEFT JOIN dishes d ON dc.category_id = d.category_id
WHERE dc.business_id = @business_id
ORDER BY dc.display_order, d.name;
```

### 9.6 Thống kê doanh thu theo tháng

```sql
SELECT YEAR(p.paid_at)  AS nam,
       MONTH(p.paid_at) AS thang,
       COUNT(DISTINCT p.booking_id) AS so_don,
       SUM(p.amount) AS doanh_thu
FROM payments p
JOIN bookings bk ON p.booking_id = bk.booking_id
WHERE p.status = 'completed'
  AND bk.business_id = @business_id
GROUP BY YEAR(p.paid_at), MONTH(p.paid_at)
ORDER BY nam DESC, thang DESC;
```

### 9.7 Hủy booking quá hạn thanh toán (5 phút)

```sql
BEGIN TRANSACTION;
  UPDATE bookings
  SET status = 'cancelled_by_owner',
      payment_status = 'refunded',
      notes = ISNULL(notes,'') + N'
[AUTO] Quá 5 phút không thanh toán'
  WHERE status = 'pending'
    AND payment_status = 'unpaid'
    AND created_at < DATEADD(MINUTE, -5, GETDATE());

  UPDATE p
  SET p.status = 'failed',
      p.gateway_response = ISNULL(p.gateway_response,'') + N' [AUTO] Expired'
  FROM payments p
  JOIN bookings bk ON p.booking_id = bk.booking_id
  WHERE bk.status = 'cancelled_by_owner'
    AND p.status = 'pending';
COMMIT;
```

### 9.8 Giỏ hàng của user

```sql
SELECT tc.cart_id, tc.quantity, tc.notes, tc.subtotal,
       d.dish_id, d.name AS dish_name, d.price, d.image_url,
       b.business_id, b.name AS restaurant_name
FROM temp_carts tc
JOIN dishes     d ON tc.dish_id     = d.dish_id
JOIN businesses b ON tc.business_id = b.business_id
WHERE tc.user_id = @user_id
ORDER BY b.business_id, d.name;
```

### 9.9 Kiểm tra quyền truy cập route

```sql
SELECT COUNT(1) AS has_permission
FROM roles_features rf
JOIN users u    ON u.role_id     = rf.role_id
JOIN features f ON rf.feature_id = f.feature_id
WHERE u.user_id = @user_id
  AND f.url = @route_url;
```

### 9.10 Đặt phòng homestay (transaction đầy đủ)

```sql
BEGIN TRANSACTION;
BEGIN TRY
    -- 1. Tạo booking
    INSERT INTO bookings (booking_code, user_id, business_id, booker_name,
        booker_email, booker_phone, num_guests, total_price,
        reservation_start_time, reservation_end_time, status, payment_status)
    VALUES (@code, @user_id, @biz_id, @name,
        @email, @phone, @guests, @price,
        @checkin, @checkout, 'pending', 'unpaid');
    DECLARE @bid INT = SCOPE_IDENTITY();

    -- 2. Lock phòng từng đêm
    INSERT INTO room_availability (room_id, date, price, status)
    SELECT @room_id, DATEADD(DAY, n, @checkin), @price_per_night, 'booked'
    FROM (
        SELECT TOP (DATEDIFF(DAY, @checkin, @checkout))
               ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS n
        FROM sys.all_objects
    ) nums;

    -- 3. Ghi booked_rooms
    INSERT INTO booked_rooms (booking_id, room_id, price_at_booking)
    VALUES (@bid, @room_id, @price_per_night);

    -- 4. Tạo payment pending
    INSERT INTO payments (booking_id, amount, payment_method, status)
    VALUES (@bid, @total_price, 'sepay', 'pending');

    COMMIT TRANSACTION;
    SELECT @bid AS new_booking_id;
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;
```

---

## 10. Quy Ước Đặt Tên

### Bảng
| Pattern | Ví dụ |
|---------|-------|
| Số nhiều, snake_case | `users`, `bookings`, `dish_categories` |
| Junction table: `{table1}_{table2}` | `business_amenities`, `roles_features` |
| Availability: `{entity}_availability` | `room_availability`, `table_availability` |

### Cột
| Pattern | Ví dụ |
|---------|-------|
| snake_case | `booking_id`, `price_per_night` |
| FK: `{table_singular}_id` | `booking_id`, `room_id`, `business_id` |
| Boolean: prefix `is_` | `is_active`, `is_available` |
| Timestamp | `created_at`, `updated_at`, `paid_at` |
| Snapshot | `price_at_booking` – lưu giá tại thời điểm giao dịch |

### Constraint
| Loại | Quy ước | Ví dụ |
|------|---------|-------|
| Primary Key | `PK_{table}` | `PK_bookings` |
| Foreign Key | `FK_{prefix}_{rel}` | `FK_br_booking`, `FK_br_room` |
| Unique | `UQ_{table}_{col}` | `UQ_users_email` |
| Check | `CK_{table}_{col}` | `CK_bookings_status` |
| Trigger | `trg_{table}_{action}` | `trg_users_updated_at` |

---

## 11. Lưu Ý Quan Trọng

### Luôn dùng prefix N cho string tiếng Việt

```sql
-- SAI – có thể mất dấu tiếng Việt
INSERT INTO areas (name) VALUES ('Cát Bà');

-- ĐÚNG – prefix N để lưu Unicode đúng
INSERT INTO areas (name) VALUES (N'Cát Bà');
```

### Hàm thời gian SQL Server vs MySQL

| MySQL | SQL Server |
|-------|-----------|
| `NOW()` | `GETDATE()` |
| `CURDATE()` | `CAST(GETDATE() AS DATE)` |
| `DATE_ADD(d, INTERVAL n DAY)` | `DATEADD(DAY, n, d)` |
| `DATEDIFF(d1, d2)` | `DATEDIFF(DAY, d1, d2)` |

### Phân trang kết quả

```sql
-- MySQL
SELECT * FROM businesses LIMIT 10 OFFSET 20;

-- SQL Server
SELECT * FROM businesses
ORDER BY business_id
OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY;
```

### Lấy ID sau INSERT

```sql
-- Cách 1
INSERT INTO bookings (...) VALUES (...);
SELECT SCOPE_IDENTITY() AS new_id;

-- Cách 2 – dùng biến
DECLARE @new_id INT;
INSERT INTO bookings (...) VALUES (...);
SET @new_id = SCOPE_IDENTITY();

-- Cách 3 – OUTPUT (recommend trong transaction)
INSERT INTO bookings (...)
OUTPUT inserted.booking_id
VALUES (...);
```

### Xử lý NULL

```sql
SELECT ISNULL(notes, N'')                AS notes,
       COALESCE(phone, N'Chua co SDT')  AS phone
FROM users;
```

### Cảnh báo Multiple Cascade Path

SQL Server không cho phép 2 FK đến cùng 1 bảng cha đều CASCADE.
Giải pháp đã áp dụng trong schema:
- `reviews.user_id` dùng ON DELETE NO ACTION (không CASCADE từ users)
- `temp_carts.business_id`, `temp_carts.dish_id` dùng NO ACTION
- `booked_rooms.room_id`, `booked_tables.table_id` dùng NO ACTION

### Nguyên tắc Snapshot Pricing

Các cột `price_at_booking` lưu giá TẠI THỜI ĐIỂM đặt, không phải giá hiện tại.
Điều này đảm bảo lịch sử đơn hàng không bị thay đổi dù chủ cập nhật giá sau.

---

*Document này được tạo dựa trên `catbabooking_for_SQLServer.sql` – phiên bản SQL Server.*
*Mọi thắc mắc liên hệ team leader dự án CatBaBooking.*
