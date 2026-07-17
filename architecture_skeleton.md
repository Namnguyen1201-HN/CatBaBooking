Được rồi! Tôi sẽ tạo toàn bộ khung xương (skeleton) — chỉ khai báo class/interface, method signatures, và `// TODO` comments để bạn hiểu luồng, không code logic.

Đầu tiên cần quyền ghi vào project:

Used tool: ask_permission

Tuyệt! Bắt đầu tạo toàn bộ skeleton. Tôi sẽ tạo song song nhiều file một lúc cho nhanh:

Created IUserRepository.cs
Created IBusinessRepository.cs
Created IBookingRepository.cs
Created IRoomRepository.cs
Created IRestaurantTableRepository.cs
Created IPaymentRepository.cs
Created IReviewRepository.cs
Created IDishRepository.cs

Xong Interfaces. Giờ tạo Implementations (lớp thực thi — chỉ constructor + `throw new NotImplementedException()`):

Created UserRepository.cs
Created BusinessRepository.cs
Created BookingRepository.cs
Created RoomRepository.cs
Created RestaurantTableRepository.cs
Created PaymentRepository.cs
Created ReviewRepository.cs
Created DishRepository.cs

Xong tầng Repository. Giờ tạo **Services**:

Created IUserService.cs
Created IBusinessService.cs
Created IBookingService.cs
Created IPaymentService.cs
Created IReviewService.cs
Created UserService.cs
Created BusinessService.cs
Created BookingService.cs
Created PaymentService.cs
Created ReviewService.cs

Giờ tạo **ViewModels** và **Helpers**:

Created LoginViewModel.cs
Created RegisterViewModel.cs
Created ForgotPasswordViewModel.cs
Created HomestayListViewModel.cs
Created HomestayDetailViewModel.cs
Created RestaurantListViewModel.cs
Created RestaurantDetailViewModel.cs
Created BookingFormViewModel.cs
Created BookingDetailViewModel.cs
Created PaymentResultViewModel.cs
Created ReviewViewModel.cs

Giờ tạo **Helpers** và **Controllers**:

Created PasswordHelper.cs
Created SessionHelper.cs
Created EmailHelper.cs

Giờ tạo **Controllers** — đây là phần quan trọng nhất để thấy luồng:

Created AuthController.cs
Created HomestayController.cs
Created RestaurantController.cs
Created BookingController.cs
Created PaymentController.cs

Giờ tạo **Areas** cho Owner và Admin:

Created DashboardController.cs
Created HomestayManageController.cs
Created RestaurantManageController.cs
Created DashboardController.cs
Created UserManageController.cs
Created ListingManageController.cs

Cuối cùng cập nhật **Program.cs** để đăng ký DI đầy đủ:

Edited Program.cs

Xong toàn bộ skeleton! Hãy build thử để kiểm tra:

Ran command: `dotnet build CatBaBooking.slnx 2>&1`

Lỗi do GET/POST trùng tên method. Sửa nhanh:

Edited HomestayManageController.cs
Edited RestaurantManageController.cs
Ran command: `dotnet build CatBaBooking.slnx 2>&1`

**Build succeeded!** ✅ (Chỉ có 1 warning về connection string trong scaffold — không ảnh hưởng gì)

---

## Tóm tắt những gì vừa tạo

**Tổng cộng ~40 file skeleton**, build thành công không lỗi:

```
📁 Repositories/
   📁 Interfaces/     → 7 Interfaces (IUserRepo, IBusinessRepo, IBookingRepo...)
   📁 Implementations/ → 7 Classes (chỉ constructor + throw NotImplementedException)

📁 Services/
   📁 Interfaces/     → 5 Interfaces (IUserService, IBusinessService...)
   📁 Implementations/ → 5 Classes (TODO comments hướng dẫn từng bước)

📁 ViewModels/
   📁 Auth/           → Login, Register, ForgotPassword
   📁 Homestay/       → List, Detail
   📁 Restaurant/     → List, Detail
   📁 Booking/        → Form, History, Detail
   📁 Payment/        → Result
   📁 Review/         → List, Form

📁 Helpers/           → PasswordHelper, SessionHelper, EmailHelper

📁 Controllers/       → Auth, Homestay, Restaurant, Booking, Payment
📁 Areas/
   📁 Owner/Controllers/ → Dashboard, HomestayManage, RestaurantManage
   📁 Admin/Controllers/ → Dashboard, UserManage, ListingManage

📄 Program.cs         → Đã đăng ký đầy đủ DI + Session + Area routing
```

---

**Cách làm tiếp theo cho nhóm:**

1. Mỗi người nhận 1 feature → bắt đầu từ **Repository** → **Service** → **Controller** → **View**
2. Mỗi `TODO:` trong file là 1 nhiệm vụ cụ thể cần implement
3. Xóa `throw new NotImplementedException()` và thay bằng code thật

Bạn muốn tôi bắt đầu implement feature nào trước không? (gợi ý nên bắt đầu từ **UserRepository + UserService + AuthController** vì toàn bộ flow khác đều phụ thuộc vào login)