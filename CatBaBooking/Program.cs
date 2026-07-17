
using CatBaBooking.Helpers;
using CatBaBooking.Models;
using CatBaBooking.Repositories.Implementations;
using CatBaBooking.Repositories.Interfaces;
using CatBaBooking.Services.Implementations;
using CatBaBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── MVC + Areas ───────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// ── Database ──────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<CatbabookingContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

// ── Session ───────────────────────────────────────────────────────────────────
// [HƯỚNG DẪN] Session dùng để lưu thông tin user sau khi Login.
// Cần gọi app.UseSession() bên dưới (ĐẶT TRƯỚC UseRouting).
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session hết hạn sau 30 phút không thao tác
    options.Cookie.HttpOnly = true;                   // Bảo mật: không cho JS đọc cookie
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();            // Cho phép inject IHttpContextAccessor

// ── Repositories (tầng truy cập DB) ──────────────────────────────────────────
// [HƯỚNG DẪN] AddScoped = tạo 1 instance mới cho mỗi HTTP request.
// Phù hợp với DbContext vì DbContext cũng là Scoped.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRestaurantTableRepository, RestaurantTableRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();

// ── Services (tầng business logic) ───────────────────────────────────────────
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

// ── Helpers ───────────────────────────────────────────────────────────────────
// [HƯỚNG DẪN] AddSingleton = tạo 1 instance dùng chung cho cả app (phù hợp với stateless helpers).
builder.Services.AddSingleton<EmailHelper>();

// ─────────────────────────────────────────────────────────────────────────────

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession(); // ← PHẢI đặt trước UseRouting

app.UseRouting();

app.UseAuthorization();

// ── Routes ────────────────────────────────────────────────────────────────────
// Route cho Areas (Admin, Owner)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HomePage}/{id?}");

app.Run();
