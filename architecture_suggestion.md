# Gợi ý Kiến trúc – CatBaBooking (ASP.NET Core MVC)

> [!NOTE]
> Dựa trên phân tích codebase hiện tại (scaffolded models, DbContext, Controllers/Views) và SRS document với 5 actors, 24 use cases.

---

## 📌 Tổng quan hiện trạng

| Thành phần | Hiện tại |
|---|---|
| Models | 24 entity class được scaffold từ DB |
| DbContext | `CatbabookingContext` trong `Models/` |
| Controllers | Chỉ có `HomeController` với các action trống |
| Views | Mới có thư mục `Home/` và `Shared/` |
| Services | Chưa có |
| Repositories | Chưa có |

---

## 🏗️ Kiến trúc đề xuất: Layered Architecture (3 tầng)

Phù hợp cho dự án học thuật MVC quy mô vừa, dễ phân chia task cho nhóm.

```
CatBaBooking/
│
├── Models/                         ← Entity Models (giữ nguyên từ scaffold)
│   ├── Booking.cs
│   ├── Business.cs
│   ├── Room.cs
│   ├── ...
│   └── CatbabookingContext.cs
│
├── Repositories/                   ← Tầng truy cập dữ liệu (Data Access)
│   ├── Interfaces/
│   │   ├── IBookingRepository.cs
│   │   ├── IBusinessRepository.cs
│   │   ├── IUserRepository.cs
│   │   └── ...
│   └── Implementations/
│       ├── BookingRepository.cs
│       ├── BusinessRepository.cs
│       ├── UserRepository.cs
│       └── ...
│
├── Services/                       ← Tầng xử lý nghiệp vụ (Business Logic)
│   ├── Interfaces/
│   │   ├── IBookingService.cs
│   │   ├── IBusinessService.cs
│   │   ├── IUserService.cs
│   │   ├── IPaymentService.cs
│   │   └── ...
│   └── Implementations/
│       ├── BookingService.cs
│       ├── BusinessService.cs
│       ├── UserService.cs
│       ├── PaymentService.cs
│       └── ...
│
├── ViewModels/                     ← DTO cho View (không dùng entity trực tiếp)
│   ├── Auth/
│   │   ├── LoginViewModel.cs
│   │   └── RegisterViewModel.cs
│   ├── Homestay/
│   │   ├── HomestayListViewModel.cs
│   │   └── HomestayDetailViewModel.cs
│   ├── Restaurant/
│   │   ├── RestaurantListViewModel.cs
│   │   └── RestaurantDetailViewModel.cs
│   ├── Booking/
│   │   ├── BookingFormViewModel.cs
│   │   └── BookingHistoryViewModel.cs
│   └── Admin/
│       └── DashboardViewModel.cs
│
├── Controllers/                    ← Tầng điều hướng (Presentation)
│   ├── HomeController.cs
│   ├── AuthController.cs           ← Login, Register, Logout, ForgotPassword
│   ├── HomestayController.cs       ← Browse, Detail (Guest/Customer)
│   ├── RestaurantController.cs     ← Browse, Detail, Table booking
│   ├── BookingController.cs        ← Checkout, History, Cancel
│   ├── PaymentController.cs        ← Payment flow, Callback
│   ├── ReviewController.cs         ← Leave review, View reviews
│   │
│   ├── Owner/                      ← [Area] Owner Dashboard
│   │   ├── OwnerDashboardController.cs
│   │   ├── HomestayManageController.cs
│   │   └── RestaurantManageController.cs
│   │
│   └── Admin/                      ← [Area] Admin Dashboard
│       ├── AdminDashboardController.cs
│       ├── UserManageController.cs
│       └── ListingManageController.cs
│
├── Views/
│   ├── Home/
│   ├── Auth/
│   ├── Homestay/
│   ├── Restaurant/
│   ├── Booking/
│   ├── Payment/
│   ├── Review/
│   ├── Areas/
│   │   ├── Owner/
│   │   │   └── Views/
│   │   └── Admin/
│   │       └── Views/
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _LayoutAdmin.cshtml
│       ├── _LayoutOwner.cshtml
│       └── _Navbar.cshtml
│
├── Helpers/                        ← Tiện ích dùng chung
│   ├── PasswordHelper.cs           ← Hash/Verify password (Argon2/BCrypt)
│   ├── EmailHelper.cs              ← Gửi email OTP
│   ├── PaginationHelper.cs         ← Phân trang
│   └── SessionHelper.cs            ← Đọc/ghi session
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
│
├── Program.cs
└── appsettings.json
```

---

## 🔄 Luồng dữ liệu (Data Flow)

```
View (.cshtml)
    ↕ ViewModel
Controller
    ↕ Service Interface (DI)
Service (Business Logic)
    ↕ Repository Interface (DI)
Repository
    ↕ DbContext (EF Core)
SQL Server Database
```

---

## 👥 Phân chia theo Actor (gợi ý phân task nhóm)

| Actor | Controllers | Views | Services |
|---|---|---|---|
| **Guest / Customer** | Home, Homestay, Restaurant, Booking, Payment, Review | Home, Homestay, Restaurant, Booking | BookingService, PaymentService, ReviewService |
| **Homestay Owner** | Areas/Owner/HomestayManage | Areas/Owner/Homestay/ | BusinessService, RoomService, AvailabilityService |
| **Restaurant Owner** | Areas/Owner/RestaurantManage | Areas/Owner/Restaurant/ | BusinessService, TableService, DishService |
| **Admin** | Areas/Admin/* | Areas/Admin/ | UserService, ListingService |

---

## 📋 Repository Pattern – Ví dụ cụ thể

### Interface
```csharp
// Repositories/Interfaces/IBookingRepository.cs
public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(int id);
    Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
    Task<Booking> CreateAsync(Booking booking);
    Task UpdateAsync(Booking booking);
    Task DeleteAsync(int id);
}
```

### Implementation
```csharp
// Repositories/Implementations/BookingRepository.cs
public class BookingRepository : IBookingRepository
{
    private readonly CatbabookingContext _context;

    public BookingRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(int id)
        => await _context.Bookings
            .Include(b => b.BookedRooms)
            .Include(b => b.BookedTables)
            .FirstOrDefaultAsync(b => b.BookingId == id);

    // ... các method khác
}
```

---

## ⚙️ Service Layer – Ví dụ cụ thể

```csharp
// Services/Implementations/BookingService.cs
public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepo;
    private readonly IUserRepository _userRepo;

    public BookingService(IBookingRepository bookingRepo, IUserRepository userRepo)
    {
        _bookingRepo = bookingRepo;
        _userRepo = userRepo;
    }

    public async Task<BookingResult> CreateHomestayBookingAsync(BookingFormViewModel form, int userId)
    {
        // Validate business rules ở đây (không ở Controller)
        // Tính toán giá, kiểm tra availability, v.v.
        var booking = new Booking { ... };
        await _bookingRepo.CreateAsync(booking);
        return new BookingResult { Success = true, BookingId = booking.BookingId };
    }
}
```

---

## 🔐 Xử lý Authentication & Authorization

### Session-based (phù hợp với MVC thuần)
```csharp
// Program.cs – thêm session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Dùng SessionHelper để đọc user hiện tại
// Dùng [Authorize] với custom policy hoặc check thủ công trong Controller
```

### Authorization theo Role
```csharp
// Helpers/SessionHelper.cs
public static class SessionHelper
{
    public static User? GetCurrentUser(ISession session)
        => session.GetString("CurrentUser") is string json
            ? JsonSerializer.Deserialize<User>(json)
            : null;

    public static bool IsInRole(ISession session, string role)
        => GetCurrentUser(session)?.Role?.RoleName == role;
}
```

---

## 📦 Đăng ký Dependency Injection trong Program.cs

```csharp
// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// Helpers
builder.Services.AddSingleton<IEmailHelper, EmailHelper>();

// Session
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
```

---

## 🗂️ ViewModels – Tại sao không dùng Entity trực tiếp?

> [!IMPORTANT]
> **Không nên** truyền thẳng entity (vd: `Booking`, `Business`) vào View vì:
> - Entity chứa navigation properties → risk vòng lặp JSON / over-fetching
> - Entity không có validation attributes cho form
> - Gây coupling chặt giữa DB schema và UI

Thay vào đó dùng **ViewModel** riêng cho từng màn hình:

```csharp
// ViewModels/Homestay/HomestayDetailViewModel.cs
public class HomestayDetailViewModel
{
    public int BusinessId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal PricePerNight { get; set; }
    public List<string> Amenities { get; set; } = new();
    public List<RoomSummary> AvailableRooms { get; set; } = new();
    public double AverageRating { get; set; }
    public List<ReviewSummary> RecentReviews { get; set; } = new();
}
```

---

## 🚀 Gợi ý thứ tự triển khai (cho nhóm)

```
Phase 1 – Nền tảng (1-2 ngày)
├── Tạo cấu trúc thư mục Repositories/, Services/, ViewModels/
├── Viết Interfaces cho tất cả repositories
├── Implement UserRepository, UserService
├── Setup Session + AuthController (Login/Logout/Register)
└── Setup Areas cho Admin và Owner

Phase 2 – Core Features Customer (3-4 ngày)
├── HomestayController + HomestayRepository + HomestayService
├── RestaurantController + RestaurantRepository + RestaurantService
├── Views: Home, Homestay list/detail, Restaurant list/detail
└── Search & Filter

Phase 3 – Booking & Payment (2-3 ngày)
├── BookingController + BookingRepository + BookingService
├── PaymentController + PaymentService (VNPay/mock)
├── BookingFormViewModel, CheckoutViewModel
└── Booking history

Phase 4 – Owner Dashboard (2-3 ngày)
├── Areas/Owner: HomestayManage, RestaurantManage
├── CRUD Rooms, Tables, Dishes
└── Revenue statistics view

Phase 5 – Admin Dashboard (1-2 ngày)
├── Areas/Admin: UserManage, ListingManage
├── Approve/reject business listings
└── Manage feedback

Phase 6 – Polish (1-2 ngày)
├── Review/Feedback feature
├── Email notifications
└── Error handling, validation
```

---

## ⚠️ Lưu ý quan trọng

> [!WARNING]
> **Không sửa file scaffold** (`CatbabookingContext.cs` và các entity class). Nếu schema DB thay đổi, chạy lại scaffold sẽ ghi đè. Mọi logic nghiệp vụ phải nằm ở Services/Repositories.

> [!TIP]
> Dùng `partial class` nếu muốn extend entity mà không sợ bị ghi đè khi scaffold lại:
> ```csharp
> // Models/Partial/BusinessExtensions.cs
> public partial class Business
> {
>     // Thêm computed properties ở đây
>     public string DisplayAddress => $"{Area?.AreaName}, Cat Ba";
> }
> ```

> [!NOTE]
> `Areas` trong ASP.NET Core MVC giúp tách biệt hoàn toàn giao diện Admin/Owner khỏi phần Customer, rất tiện cho phân quyền và phân chia task nhóm.
