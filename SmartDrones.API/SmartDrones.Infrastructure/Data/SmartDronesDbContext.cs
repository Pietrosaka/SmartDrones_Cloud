using Microsoft.EntityFrameworkCore;
using SmartDrones.Domain.Entities;

namespace SmartDrones.Infrastructure.Data
{
    public class SmartDronesDbContext : DbContext
    {
        public SmartDronesDbContext(DbContextOptions<SmartDronesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drone> Drones { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drone>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                
                entity.Property(e => e.CreatedAt)
                      .HasColumnName("CREATED_AT")
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("SYSDATE"); 

                entity.Property(e => e.UpdatedAt)
                      .HasColumnName("UPDATED_AT");

                entity.Property(e => e.LastActivity) 
                      .HasColumnName("LAST_ACTIVITY")
                      .ValueGeneratedOnAddOrUpdate() 
                      .HasDefaultValueSql("SYSDATE"); 
            });

            modelBuilder.Entity<SensorData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Timestamp)
                      .HasColumnName("TIMESTAMP_DATA")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("SYSDATE");

                entity.HasOne(sd => sd.Drone)
                      .WithMany(d => d.SensorData)
                      .HasForeignKey(sd => sd.DroneId);
            });

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Timestamp)
                      .HasColumnName("TIMESTAMP_ALERT")
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("SYSDATE");

                entity.HasOne(a => a.Drone)
                      .WithMany(d => d.Alerts)
                      .HasForeignKey(a => a.DroneId);
            });
        }
    }
}