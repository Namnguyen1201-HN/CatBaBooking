using System;
using System.Collections.Generic;

namespace CatBaBooking.Models;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Phone { get; set; }

    public string? CitizenId { get; set; }

    public string? PersonalAddress { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TempCart> TempCarts { get; set; } = new List<TempCart>();

    public User(int userId, int roleId, string fullName, string email, string passwordHash,
    string? phone, string? citizenId, string? personalAddress, string status,
    DateTime? createdAt, DateTime? updatedAt)
    {
        UserId = userId;
        RoleId = roleId;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
        CitizenId = citizenId;
        PersonalAddress = personalAddress;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
