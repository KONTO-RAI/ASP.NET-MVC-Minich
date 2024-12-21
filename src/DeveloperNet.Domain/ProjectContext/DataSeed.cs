using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperNet.Domain.Entities.Common;
using DeveloperNet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeveloperNet.Domain.ProjectContext
{
    public static class DataSeed
    {
        private static Guid ADMIN_ROLE_ID;
        private static Guid USER_ROLE_ID;
        private static Guid GUEST_ROLE_ID;

        public static void Seed(this ModelBuilder builder)
        {
            SeedRoles(builder);
            SeedUsers(builder);
            SeedPosts(builder);
            SeedTags(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            ADMIN_ROLE_ID = Guid.NewGuid();
            USER_ROLE_ID = Guid.NewGuid();
            GUEST_ROLE_ID = Guid.NewGuid();

            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = ADMIN_ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = ADMIN_ROLE_ID.ToString()
                },
                new IdentityRole<Guid>
                {
                    Id = USER_ROLE_ID,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = USER_ROLE_ID.ToString()
                },
                new IdentityRole<Guid>
                {
                    Id = GUEST_ROLE_ID,
                    Name = "Guest",
                    NormalizedName = "GUEST",
                    ConcurrencyStamp = GUEST_ROLE_ID.ToString()
                }
            );
        }

        private static void SeedUsers(ModelBuilder builder)
        {
            var adminId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var guestId = Guid.NewGuid();

            var admin = new User
            {
                Id = adminId,
                UserName = "admin@project.com",
                NormalizedUserName = "ADMIN@PROJECT.COM",
                Email = "admin@project.com",
                NormalizedEmail = "ADMIN@PROJECT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Admin User"
            };

            var user = new User
            {
                Id = userId,
                UserName = "user@project.com",
                NormalizedUserName = "USER@PROJECT.COM",
                Email = "user@project.com",
                NormalizedEmail = "USER@PROJECT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Regular User"
            };

            var guest = new User
            {
                Id = guestId,
                UserName = "guest@project.com",
                NormalizedUserName = "GUEST@PROJECT.COM",
                Email = "guest@project.com",
                NormalizedEmail = "GUEST@PROJECT.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                FullName = "Guest User"
            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "AdminPass123!");
            user.PasswordHash = passwordHasher.HashPassword(user, "UserPass123!");
            guest.PasswordHash = passwordHasher.HashPassword(guest, "GuestPass123!");

            builder.Entity<User>().HasData(admin, user, guest);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { UserId = adminId, RoleId = ADMIN_ROLE_ID },
                new IdentityUserRole<Guid> { UserId = userId, RoleId = USER_ROLE_ID },
                new IdentityUserRole<Guid> { UserId = guestId, RoleId = GUEST_ROLE_ID }
            );
        }

        private static void SeedPosts(ModelBuilder builder)
        {
            var postId = Guid.NewGuid();
            var userId = Guid.NewGuid(); 

            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = postId,
                    Title = "Welcome to the Project!",
                    Content = "This is the first post in our system.",
                    AuthorId = userId,
                    CreatedAt = DateTime.Now
                }
            );
        }

        private static void SeedTags(ModelBuilder builder)
        {
            builder.Entity<Tag>().HasData(
                new Tag { Id = Guid.NewGuid(), Name = "General" },
                new Tag { Id = Guid.NewGuid(), Name = "Announcements" },
                new Tag { Id = Guid.NewGuid(), Name = "Questions" }
            );
        }
    }
}
