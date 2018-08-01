using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThesisProcessor.Models;

namespace ThesisProcessor.Data
{
    public class SampleData
    {
        private ApplicationDbContext _context;

        public SampleData(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAdminUser()
        {
            var userStore = new UserStore<ApplicationUser>(_context);
            var user = new ApplicationUser
            {
                UserName = "Email@email.com",
                NormalizedUserName = "email@email.com",
                Email = "Email@email.com",
                NormalizedEmail = "email@email.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;

                await userStore.CreateAsync(user);
            }
            user = await userStore.FindByEmailAsync("admin@emial.com");
            await userStore.AddToRoleAsync(user, "Admin");
            await _context.SaveChangesAsync();
        }
    }
}
