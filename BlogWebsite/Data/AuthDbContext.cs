using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var roles = new List<IdentityRole>
            { 
           new IdentityRole
            {
                Name = "Admin",
                NormalizedName="Admin",
                Id = "02dcc0dc-7aa8-4f55-8b48-ebecbd245639",
                ConcurrencyStamp = "02dcc0dc-7aa8-4f55-8b48-ebecbd245639"
            },
           new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName="SuperAdmin",
                Id = "66248f2d-cb30-4717-ad00-c34cc01f6cb0",
                ConcurrencyStamp = "66248f2d-cb30-4717-ad00-c34cc01f6cb0"
            },
           new IdentityRole
            {
                Name = "User",
                NormalizedName="User",
                Id = "914ddef9-bbac-45da-99ec-52625a0fde3d",
                ConcurrencyStamp = "914ddef9-bbac-45da-99ec-52625a0fde3d"
            }
            };

            builder.Entity<IdentityRole>().HasData(roles);
            var superAdminId = "66248f2d-cb30-4717-ad00-c34cc01f6cb0";
            var superAdminUser = new IdentityUser
            { UserName = "Vishal_Verma",
                Email = "Vishal39715@gmail.com",
                NormalizedEmail = "Vishal39715@gmailcom".ToUpper(),
                Id = superAdminId,
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser,"Vishal@7007");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            var supuerAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId="02dcc0dc-7aa8-4f55-8b48-ebecbd245639",
                    UserId=superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId="66248f2d-cb30-4717-ad00-c34cc01f6cb0",
                    UserId=superAdminId
                },
                  new IdentityUserRole<string>
                {
                    RoleId="914ddef9-bbac-45da-99ec-52625a0fde3d",
                    UserId=superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(supuerAdminRoles);
        }
    }
}
