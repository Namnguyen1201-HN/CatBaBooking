using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Models;

public partial class CatbabookingContext : DbContext
{
    public CatbabookingContext()
    {
    }

    public CatbabookingContext(DbContextOptions<CatbabookingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenity> Amenities { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<BookedRoom> BookedRooms { get; set; }

    public virtual DbSet<BookedTable> BookedTables { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDish> BookingDishes { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<CuisineType> CuisineTypes { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishCategory> DishCategories { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<Occasion> Occasions { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RestaurantTable> RestaurantTables { get; set; }

    public virtual DbSet<RestaurantType> RestaurantTypes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomAvailability> RoomAvailabilities { get; set; }

    public virtual DbSet<RoomImage> RoomImages { get; set; }

    public virtual DbSet<TableAvailability> TableAvailabilities { get; set; }

    public virtual DbSet<TempCart> TempCarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=NAMNGUYEN;Initial Catalog=catbabooking; Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");
    //NamNS: "MyCnn": "Data Source=NAMNGUYEN;Initial Catalog=catbabooking; Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true"
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Vietnamese_CI_AS");

        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.ToTable("amenities");

            entity.HasIndex(e => e.Name, "UQ_amenities_name").IsUnique();

            entity.Property(e => e.AmenityId).HasColumnName("amenity_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.ToTable("areas");

            entity.HasIndex(e => e.Name, "UQ_areas_name").IsUnique();

            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BookedRoom>(entity =>
        {
            entity.ToTable("booked_rooms");

            entity.Property(e => e.BookedRoomId).HasColumnName("booked_room_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.PriceAtBooking)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price_at_booking");
            entity.Property(e => e.RoomId).HasColumnName("room_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookedRooms)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_br_booking");

            entity.HasOne(d => d.Room).WithMany(p => p.BookedRooms)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_br_room");
        });

        modelBuilder.Entity<BookedTable>(entity =>
        {
            entity.ToTable("booked_tables");

            entity.Property(e => e.BookedTableId).HasColumnName("booked_table_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookedTables)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_bt_booking");

            entity.HasOne(d => d.Table).WithMany(p => p.BookedTables)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bt_table");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("bookings", tb => tb.HasTrigger("trg_bookings_updated_at"));

            entity.HasIndex(e => e.BookingCode, "UQ_bookings_code").IsUnique();

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookerEmail)
                .HasMaxLength(255)
                .HasColumnName("booker_email");
            entity.Property(e => e.BookerName)
                .HasMaxLength(255)
                .HasColumnName("booker_name");
            entity.Property(e => e.BookerPhone)
                .HasMaxLength(20)
                .HasColumnName("booker_phone");
            entity.Property(e => e.BookingCode)
                .HasMaxLength(100)
                .HasColumnName("booking_code");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.NumGuests).HasColumnName("num_guests");
            entity.Property(e => e.PaidAmount)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("paid_amount");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValue("unpaid")
                .HasColumnName("payment_status");
            entity.Property(e => e.ReservationDate).HasColumnName("reservation_date");
            entity.Property(e => e.ReservationEndTime).HasColumnName("reservation_end_time");
            entity.Property(e => e.ReservationStartTime).HasColumnName("reservation_start_time");
            entity.Property(e => e.ReservationTime).HasColumnName("reservation_time");
            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Business).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bookings_business");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_bookings_user");
        });

        modelBuilder.Entity<BookingDish>(entity =>
        {
            entity.ToTable("booking_dishes");

            entity.Property(e => e.BookingDishId).HasColumnName("booking_dish_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.PriceAtBooking)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price_at_booking");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDishes)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_bdd_booking");

            entity.HasOne(d => d.Dish).WithMany(p => p.BookingDishes)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_bdd_dish");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.ToTable("businesses", tb => tb.HasTrigger("trg_businesses_updated_at"));

            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.AvgRating)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("avg_rating");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.ClosingHour).HasColumnName("closing_hour");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NumBedrooms).HasColumnName("num_bedrooms");
            entity.Property(e => e.OpeningHour).HasColumnName("opening_hour");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PricePerNight)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price_per_night");
            entity.Property(e => e.ReviewCount)
                .HasDefaultValue(0)
                .HasColumnName("review_count");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Area).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.AreaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_businesses_area");

            entity.HasOne(d => d.Owner).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_businesses_owner");

            entity.HasMany(d => d.Amenities).WithMany(p => p.Businesses)
                .UsingEntity<Dictionary<string, object>>(
                    "BusinessAmenity",
                    r => r.HasOne<Amenity>().WithMany()
                        .HasForeignKey("AmenityId")
                        .HasConstraintName("FK_ba_amenity"),
                    l => l.HasOne<Business>().WithMany()
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_ba_business"),
                    j =>
                    {
                        j.HasKey("BusinessId", "AmenityId");
                        j.ToTable("business_amenities");
                        j.IndexerProperty<int>("BusinessId").HasColumnName("business_id");
                        j.IndexerProperty<int>("AmenityId").HasColumnName("amenity_id");
                    });

            entity.HasMany(d => d.Cuisines).WithMany(p => p.Businesses)
                .UsingEntity<Dictionary<string, object>>(
                    "BusinessCuisine",
                    r => r.HasOne<CuisineType>().WithMany()
                        .HasForeignKey("CuisineId")
                        .HasConstraintName("FK_bc_cuisine"),
                    l => l.HasOne<Business>().WithMany()
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_bc_business"),
                    j =>
                    {
                        j.HasKey("BusinessId", "CuisineId");
                        j.ToTable("business_cuisines");
                        j.IndexerProperty<int>("BusinessId").HasColumnName("business_id");
                        j.IndexerProperty<int>("CuisineId").HasColumnName("cuisine_id");
                    });

            entity.HasMany(d => d.Occasions).WithMany(p => p.Businesses)
                .UsingEntity<Dictionary<string, object>>(
                    "BusinessOccasion",
                    r => r.HasOne<Occasion>().WithMany()
                        .HasForeignKey("OccasionId")
                        .HasConstraintName("FK_bo_occasion"),
                    l => l.HasOne<Business>().WithMany()
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_bo_business"),
                    j =>
                    {
                        j.HasKey("BusinessId", "OccasionId");
                        j.ToTable("business_occasions");
                        j.IndexerProperty<int>("BusinessId").HasColumnName("business_id");
                        j.IndexerProperty<int>("OccasionId").HasColumnName("occasion_id");
                    });

            entity.HasMany(d => d.Types).WithMany(p => p.Businesses)
                .UsingEntity<Dictionary<string, object>>(
                    "BusinessRestaurantType",
                    r => r.HasOne<RestaurantType>().WithMany()
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_brt_type"),
                    l => l.HasOne<Business>().WithMany()
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_brt_business"),
                    j =>
                    {
                        j.HasKey("BusinessId", "TypeId");
                        j.ToTable("business_restaurant_types");
                        j.IndexerProperty<int>("BusinessId").HasColumnName("business_id");
                        j.IndexerProperty<int>("TypeId").HasColumnName("type_id");
                    });
        });

        modelBuilder.Entity<CuisineType>(entity =>
        {
            entity.HasKey(e => e.CuisineId);

            entity.ToTable("cuisine_types");

            entity.HasIndex(e => e.Name, "UQ_cuisine_types_name").IsUnique();

            entity.Property(e => e.CuisineId).HasColumnName("cuisine_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("dishes");

            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price");

            entity.HasOne(d => d.Business).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_dishes_business");

            entity.HasOne(d => d.Category).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_dishes_category");
        });

        modelBuilder.Entity<DishCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("dish_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0)
                .HasColumnName("display_order");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Business).WithMany(p => p.DishCategories)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_dc_business");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.ToTable("features");

            entity.HasIndex(e => e.Url, "UQ_features_url").IsUnique();

            entity.Property(e => e.FeatureId).HasColumnName("feature_id");
            entity.Property(e => e.FeatureName)
                .HasMaxLength(100)
                .HasColumnName("feature_name");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Occasion>(entity =>
        {
            entity.ToTable("occasions");

            entity.HasIndex(e => e.Name, "UQ_occasions_name").IsUnique();

            entity.Property(e => e.OccasionId).HasColumnName("occasion_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("payments", tb => tb.HasTrigger("trg_payments_updated_at"));

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.GatewayResponse).HasColumnName("gateway_response");
            entity.Property(e => e.PaidAt).HasColumnName("paid_at");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(15)
                .HasColumnName("status");
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(255)
                .HasColumnName("transaction_code");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_payments_booking");
        });

        modelBuilder.Entity<RestaurantTable>(entity =>
        {
            entity.HasKey(e => e.TableId);

            entity.ToTable("restaurant_tables");

            entity.Property(e => e.TableId).HasColumnName("table_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Business).WithMany(p => p.RestaurantTables)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_rt_business");
        });

        modelBuilder.Entity<RestaurantType>(entity =>
        {
            entity.HasKey(e => e.TypeId);

            entity.ToTable("restaurant_types");

            entity.HasIndex(e => e.Name, "UQ_restaurant_types_name").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("reviews");

            entity.HasIndex(e => e.BookingId, "UQ_reviews_booking").IsUnique();

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Booking).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.BookingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_reviews_booking");

            entity.HasOne(d => d.Business).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_reviews_business");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reviews_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "UQ_roles_role_name").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");

            entity.HasMany(d => d.Features).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesFeature",
                    r => r.HasOne<Feature>().WithMany()
                        .HasForeignKey("FeatureId")
                        .HasConstraintName("FK_rf_feature"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_rf_role"),
                    j =>
                    {
                        j.HasKey("RoleId", "FeatureId");
                        j.ToTable("roles_features");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<int>("FeatureId").HasColumnName("feature_id");
                    });
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("rooms");

            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PricePerNight)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price_per_night");

            entity.HasOne(d => d.Business).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("FK_rooms_business");
        });

        modelBuilder.Entity<RoomAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId);

            entity.ToTable("room_availability");

            entity.HasIndex(e => new { e.RoomId, e.Date }, "UQ_room_availability").IsUnique();

            entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("price");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomAvailabilities)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_room_avail_room");
        });

        modelBuilder.Entity<RoomImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("room_images");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.RoomId).HasColumnName("room_id");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomImages)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_room_images_room");
        });

        modelBuilder.Entity<TableAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId);

            entity.ToTable("table_availability");

            entity.HasIndex(e => new { e.TableId, e.ReservationDate, e.ReservationTime }, "UQ_table_availability").IsUnique();

            entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");
            entity.Property(e => e.ReservationDate).HasColumnName("reservation_date");
            entity.Property(e => e.ReservationTime).HasColumnName("reservation_time");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasColumnName("status");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.Table).WithMany(p => p.TableAvailabilities)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("FK_table_avail_table");
        });

        modelBuilder.Entity<TempCart>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.ToTable("temp_carts", tb => tb.HasTrigger("trg_temp_carts_updated_at"));

            entity.HasIndex(e => new { e.UserId, e.BusinessId, e.DishId }, "UQ_temp_carts").IsUnique();

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DishId).HasColumnName("dish_id");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Business).WithMany(p => p.TempCarts)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tc_business");

            entity.HasOne(d => d.Dish).WithMany(p => p.TempCarts)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tc_dish");

            entity.HasOne(d => d.User).WithMany(p => p.TempCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_tc_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users", tb => tb.HasTrigger("trg_users_updated_at"));

            entity.HasIndex(e => e.Email, "UQ_users_email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CitizenId)
                .HasMaxLength(50)
                .HasColumnName("citizen_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PersonalAddress)
                .HasMaxLength(500)
                .HasColumnName("personal_address");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
