using Microsoft.EntityFrameworkCore;
using HealthCare.Domain.Entities;

namespace HealthCare.Infrastructure.Data
{
    public class HealthCareDbContext : DbContext
    {
        public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddresses { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Username).IsRequired().HasMaxLength(100);
                b.Property(u => u.Email).IsRequired().HasMaxLength(200);
                b.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.Property(r => r.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Permission>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId });
                b.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId);
                b.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<RolePermission>(b =>
            {
                b.HasKey(rp => new { rp.RoleId, rp.PermissionId });
                b.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId);
                b.HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId);
            });

            modelBuilder.Entity<RefreshToken>(b =>
            {
                b.HasKey(rt => rt.Id);
                b.Property(rt => rt.Token).IsRequired();
                b.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId);
            });

            modelBuilder.Entity<Patient>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.PatientCode).HasMaxLength(50);
                b.Property(p => p.FirstName).HasMaxLength(100);
                b.Property(p => p.LastName).HasMaxLength(100);
                b.Property(p => p.Email).HasMaxLength(200);
                b.Property(p => p.PhoneNumber).HasMaxLength(50);
                b.Property(p => p.BloodGroup).HasMaxLength(10);
            });

            modelBuilder.Entity<PatientAddress>(b =>
            {
                b.HasKey(pa => pa.Id);
                b.HasOne(pa => pa.Patient).WithMany(p => p.PatientAddresses).HasForeignKey(pa => pa.PatientId);
            });

            modelBuilder.Entity<EmergencyContact>(b =>
            {
                b.HasKey(ec => ec.Id);
                b.HasOne(ec => ec.Patient).WithMany(p => p.EmergencyContacts).HasForeignKey(ec => ec.PatientId);
                b.Property(ec => ec.FullName).HasMaxLength(200);
                b.Property(ec => ec.ContactNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<UserSession>(b =>
            {
                b.HasKey(us => us.Id);
                b.HasOne(us => us.User).WithMany(u => u.Sessions).HasForeignKey(us => us.UserId);
            });

            modelBuilder.Entity<AuditLog>(b =>
            {
                b.HasKey(a => a.Id);
            });
        }
    }
}
