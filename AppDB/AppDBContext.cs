using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using AppDB.Models.Entity;

namespace AppDB
{
    public class AppDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDBContext(DbContextOptions<AppDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<StatusOrders> StatusOrders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Combos> Combos { get; set; }
        public DbSet<ComboDetails> ComboDetails { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Orders - Users relationship
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.Customer_Orders)
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Staff)
                .WithMany(u => u.Staff_Orders)
                .HasForeignKey(o => o.StaffID)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Roles
            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var staffRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var customerRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Roles>().HasData(
                new Roles
                {
                    id = adminRoleId,
                    RoleName = "Admin",
                    createdDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    createdById = adminRoleId,
                    createdByName = "System"
                },
                new Roles
                {
                    id = staffRoleId,
                    RoleName = "Staff",
                    createdDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    createdById = adminRoleId,
                    createdByName = "System"
                },
                new Roles
                {
                    id = customerRoleId,
                    RoleName = "Customer",
                    createdDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    createdById = adminRoleId,
                    createdByName = "System"
                }
            );

            // Seed Admin User
            var adminUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var hashedPassword = HashPassword("admin123456");

            modelBuilder.Entity<Users>().HasData(
                new Users
                {
                    id = adminUserId,
                    Username = "admin",
                    Password = hashedPassword,
                    FullName = "Administrator",
                    Email = "admin@example.com",
                    Phone = "0123456789",
                    Address = "Admin Address",
                    RoleId = adminRoleId,
                    createdDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    createdById = adminUserId,
                    createdByName = "System"
                }
            );
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdClaim?.Value, out var userId);

            var userNameClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name);
            var userName = userNameClaim?.Value;
            foreach (var entry in entries)
            {
                foreach (var prop in entry.Properties)
                {
                    if (prop.CurrentValue != null)
                    {
                        if (prop.Metadata.ClrType == typeof(DateTime))
                            prop.CurrentValue = ((DateTime)prop.CurrentValue).ToLocalTime();

                        if (prop.Metadata.ClrType == typeof(DateTime?))
                            prop.CurrentValue = ((DateTime?)prop.CurrentValue).Value.ToLocalTime();
                    }
                }
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.createdDate = DateTime.UtcNow;
                    entry.Entity.createdById = userId;
                    entry.Entity.createdByName = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.updatedDate = DateTime.UtcNow;
                    entry.Entity.updatedById = userId;
                    entry.Entity.updatedByName = userName;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
