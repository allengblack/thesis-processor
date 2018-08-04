using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThesisProcessor.Data;
using ThesisProcessor.Models;

public class SampleData
{
    private ApplicationDbContext _context;

    public SampleData(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAdminUser()
    {
        var user = new ApplicationUser
        {
            UserName = "admin@thesismanager.com",
            NormalizedUserName = "ADMIN@THESISMANAGER.COM",
            Email = "admin@thesismanager.com",
            NormalizedEmail = "ADMIN@THESISMANAGER.COM",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        if (!_context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "password");
            user.PasswordHash = hashed;
            var userStore = new UserStore<ApplicationUser>(_context);
            await userStore.CreateAsync(user);
            var role = new IdentityRole { Name = "Admin", Id = Guid.NewGuid().ToString() };
            _context.Roles.Add(role);
            _context.UserRoles.Add(new IdentityUserRole<string> { UserId = user.Id, RoleId = role.Id });
            await _context.SaveChangesAsync();
        }
    }
}