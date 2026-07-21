using System;
using System.Collections.Generic;
using System.Linq;
using CatBaBooking.Models;
using CatBaBooking.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking.Repository;

public class ApproveApplicationRepository : IApproveApplicationRepository
{
    private readonly CatbabookingContext _context;

    public ApproveApplicationRepository(CatbabookingContext context)
    {
        _context = context;
    }

    public List<Business> GetPendingBusinesses()
    {
        return _context.Businesses
            .Include(b => b.Owner)
            .Where(b => b.Status == "pending")
            .OrderBy(b => b.CreatedAt)
            .ToList();
    }

    public Business? GetPendingBusinessById(int businessId)
    {
        return _context.Businesses
            .Include(b => b.Owner)
            .FirstOrDefault(b => b.BusinessId == businessId && b.Status == "pending");
    }

    public bool UpdateBusinessAndOwnerStatus(int businessId, string businessStatus, string ownerStatus)
    {
        var business = _context.Businesses
            .Include(b => b.Owner)
            .FirstOrDefault(b => b.BusinessId == businessId);

        if (business == null) return false;

        business.Status = businessStatus;
        business.UpdatedAt = DateTime.Now;

        if (business.Owner != null)
        {
            business.Owner.Status = ownerStatus;
            business.Owner.UpdatedAt = DateTime.Now;
        }

        _context.SaveChanges();
        return true;
    }
}
