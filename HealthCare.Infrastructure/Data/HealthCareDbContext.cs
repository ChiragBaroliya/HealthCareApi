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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<PatientAllergy> PatientAllergies { get; set; }
        public DbSet<VaccinationRecord> VaccinationRecords { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<LabOrder> LabOrders { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<PharmacyOrder> PharmacyOrders { get; set; }
        public DbSet<PharmacyInventory> PharmacyInventories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }

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

            modelBuilder.Entity<DoctorSpecialization>(b =>
            {
                b.HasKey(ds => ds.Id);
                b.Property(ds => ds.Name).IsRequired().HasMaxLength(100);
                b.Property(ds => ds.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<Doctor>(b =>
            {
                b.HasKey(d => d.Id);
                b.Property(d => d.DoctorCode).IsRequired().HasMaxLength(50);
                b.Property(d => d.FullName).IsRequired().HasMaxLength(200);
                b.Property(d => d.Email).IsRequired().HasMaxLength(200);
                b.Property(d => d.PhoneNumber).HasMaxLength(50);
                b.Property(d => d.LicenseNumber).IsRequired().HasMaxLength(100);
                b.Property(d => d.ConsultationFee).HasPrecision(18, 2);

                b.HasOne(d => d.Specialization)
                 .WithMany(s => s.Doctors)
                 .HasForeignKey(d => d.SpecializationId);
            });

            modelBuilder.Entity<DoctorSchedule>(b =>
            {
                b.HasKey(ds => ds.Id);
                b.HasOne(ds => ds.Doctor)
                 .WithMany(d => d.Schedules)
                 .HasForeignKey(ds => ds.DoctorId);
            });

            modelBuilder.Entity<Appointment>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(a => a.AppointmentNumber).IsRequired().HasMaxLength(50);
                b.Property(a => a.Reason).HasMaxLength(500);
                b.Property(a => a.Notes).HasMaxLength(1000);
                b.Property(a => a.Status).HasConversion<string>();

                b.HasOne(a => a.Patient)
                 .WithMany()
                 .HasForeignKey(a => a.PatientId)
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(a => a.Doctor)
                 .WithMany()
                 .HasForeignKey(a => a.DoctorId)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Medicine>(b =>
            {
                b.HasKey(m => m.Id);
                b.Property(m => m.MedicineName).IsRequired().HasMaxLength(200);
                b.Property(m => m.Manufacturer).HasMaxLength(200);
                b.Property(m => m.UnitPrice).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Prescription>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Notes).HasMaxLength(1000);

                b.HasOne(p => p.Appointment)
                 .WithMany()
                 .HasForeignKey(p => p.AppointmentId)
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(p => p.Doctor)
                 .WithMany()
                 .HasForeignKey(p => p.DoctorId)
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(p => p.Patient)
                 .WithMany()
                 .HasForeignKey(p => p.PatientId)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<PrescriptionMedicine>(b =>
            {
                b.HasKey(pm => pm.Id);
                b.Property(pm => pm.Dosage).IsRequired().HasMaxLength(100);
                b.Property(pm => pm.Frequency).HasMaxLength(100);
                b.Property(pm => pm.Duration).HasMaxLength(100);
                b.Property(pm => pm.Instructions).HasMaxLength(500);

                b.HasOne(pm => pm.Prescription)
                 .WithMany(p => p.PrescriptionMedicines)
                 .HasForeignKey(pm => pm.PrescriptionId);

                b.HasOne(pm => pm.Medicine)
                 .WithMany()
                 .HasForeignKey(pm => pm.MedicineId);
            });

            modelBuilder.Entity<MedicalRecord>(b =>
            {
                b.HasKey(mr => mr.Id);
                b.Property(mr => mr.Diagnosis).IsRequired().HasMaxLength(500);
                b.Property(mr => mr.Symptoms).HasMaxLength(1000);
                b.Property(mr => mr.TreatmentPlan).HasMaxLength(2000);
                b.Property(mr => mr.Notes).HasMaxLength(1000);

                b.HasOne(mr => mr.Patient)
                 .WithMany()
                 .HasForeignKey(mr => mr.PatientId);
            });

            modelBuilder.Entity<PatientAllergy>(b =>
            {
                b.HasKey(pa => pa.Id);
                b.Property(pa => pa.AllergyName).IsRequired().HasMaxLength(200);
                b.Property(pa => pa.Severity).HasMaxLength(50);
                b.Property(pa => pa.Notes).HasMaxLength(500);

                b.HasOne(pa => pa.Patient)
                 .WithMany()
                 .HasForeignKey(pa => pa.PatientId);
            });

            modelBuilder.Entity<VaccinationRecord>(b =>
            {
                b.HasKey(vr => vr.Id);
                b.Property(vr => vr.VaccineName).IsRequired().HasMaxLength(200);

                b.HasOne(vr => vr.Patient)
                 .WithMany()
                 .HasForeignKey(vr => vr.PatientId);
            });

            modelBuilder.Entity<Invoice>(b =>
            {
                b.HasKey(i => i.Id);
                b.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(50);
                b.Property(i => i.TotalAmount).HasPrecision(18, 2);
                b.Property(i => i.TaxAmount).HasPrecision(18, 2);
                b.Property(i => i.DiscountAmount).HasPrecision(18, 2);
                b.Property(i => i.PaidAmount).HasPrecision(18, 2);
                b.Property(i => i.Status).HasConversion<string>();

                b.HasOne(i => i.Patient)
                 .WithMany()
                 .HasForeignKey(i => i.PatientId)
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(i => i.Appointment)
                 .WithMany()
                 .HasForeignKey(i => i.AppointmentId)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Payment>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(100);
                b.Property(p => p.TransactionId).HasMaxLength(100);
                b.Property(p => p.Amount).HasPrecision(18, 2);
                b.Property(p => p.PaymentStatus).HasConversion<string>();

                b.HasOne(p => p.Invoice)
                 .WithMany(i => i.Payments)
                 .HasForeignKey(p => p.InvoiceId);
            });

            modelBuilder.Entity<Hospital>(b =>
            {
                b.HasKey(h => h.Id);
                b.Property(h => h.Name).IsRequired().HasMaxLength(200);
                b.Property(h => h.Address).HasMaxLength(500);
                b.Property(h => h.ContactNumber).HasMaxLength(50);
                b.Property(h => h.Email).HasMaxLength(100);
            });

            modelBuilder.Entity<Department>(b =>
            {
                b.HasKey(d => d.Id);
                b.Property(d => d.DepartmentName).IsRequired().HasMaxLength(200);
                b.Property(d => d.Description).HasMaxLength(500);

                b.HasOne(d => d.Hospital)
                 .WithMany(h => h.Departments)
                 .HasForeignKey(d => d.HospitalId);
            });

            modelBuilder.Entity<LabTest>(b =>
            {
                b.HasKey(lt => lt.Id);
                b.Property(lt => lt.TestName).IsRequired().HasMaxLength(200);
                b.Property(lt => lt.Cost).HasPrecision(18, 2);
            });

            modelBuilder.Entity<LabOrder>(b =>
            {
                b.HasKey(lo => lo.Id);
                b.Property(lo => lo.Status).HasMaxLength(50);

                b.HasOne(lo => lo.Patient)
                 .WithMany()
                 .HasForeignKey(lo => lo.PatientId)
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(lo => lo.Doctor)
                 .WithMany()
                 .HasForeignKey(lo => lo.DoctorId)
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(lo => lo.Appointment)
                 .WithMany()
                 .HasForeignKey(lo => lo.AppointmentId)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<LabResult>(b =>
            {
                b.HasKey(lr => lr.Id);

                b.HasOne(lr => lr.LabOrder)
                 .WithMany(lo => lo.LabResults)
                 .HasForeignKey(lr => lr.LabOrderId);
            });

            modelBuilder.Entity<PharmacyOrder>(b =>
            {
                b.HasKey(po => po.Id);
                b.Property(po => po.Status).HasMaxLength(50);

                b.HasOne(po => po.Patient)
                 .WithMany()
                 .HasForeignKey(po => po.PatientId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(po => po.Prescription)
                 .WithMany()
                 .HasForeignKey(po => po.PrescriptionId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PharmacyInventory>(b =>
            {
                b.HasKey(pi => pi.Id);
                b.Property(pi => pi.BatchNumber).HasMaxLength(100);

                b.HasOne(pi => pi.Medicine)
                 .WithMany()
                 .HasForeignKey(pi => pi.MedicineId);
            });

            modelBuilder.Entity<Document>(b =>
            {
                b.HasKey(d => d.Id);
                b.Property(d => d.FileName).IsRequired().HasMaxLength(255);
                b.Property(d => d.FileType).HasMaxLength(50);
                b.Property(d => d.BlobUrl).IsRequired();

                b.HasOne(d => d.Patient)
                 .WithMany()
                 .HasForeignKey(d => d.PatientId);
            });

            modelBuilder.Entity<Notification>(b =>
            {
                b.HasKey(n => n.Id);
                b.Property(n => n.Title).IsRequired().HasMaxLength(200);
                b.Property(n => n.NotificationType).HasConversion<string>();

                b.HasOne(n => n.User)
                 .WithMany()
                 .HasForeignKey(n => n.UserId);
            });

            modelBuilder.Entity<EmailQueue>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.RecipientEmail).IsRequired().HasMaxLength(255);
                b.Property(e => e.Subject).HasMaxLength(500);
            });
        }
    }
}
