using DeveloperNet.Domain.Entities.Common;
using DeveloperNet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeveloperNet.Domain.ProjectContext
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Налаштування зв’язків між сутностями
            builder.Entity<Post>()
                .HasMany(p => p.Answers)
                .WithOne(a => a.Post)
                .HasForeignKey(a => a.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Tag>()
                .HasMany(t => t.Posts)
                .WithMany(p => p.Tags);

            builder.Entity<Comment>()
                .HasOne(c => c.Answer)
                .WithMany()
                .HasForeignKey(c => c.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Answer>()
                .HasOne(a => a.Author)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId);

            builder.Entity<Vote>()
                .HasOne(v => v.Post)
            .WithMany()
                .HasForeignKey(v => v.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Vote>()
                .HasOne(v => v.Answer)
                .WithMany()
                .HasForeignKey(v => v.AnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Vote>()
                .HasOne(v => v.Comment)
                .WithMany()
                .HasForeignKey(v => v.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<IdentityUserRole<Guid>>().HasKey(ur => new { ur.UserId, ur.RoleId });

           
            builder.Seed();
        }
    }
}
