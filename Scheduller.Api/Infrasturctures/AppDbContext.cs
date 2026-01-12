using Microsoft.EntityFrameworkCore;
using Scheduller.Api.Domains.Entities;

namespace Scheduller.Api.Infrasturctures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessDetail>()
                .HasOne(pd => pd.Model)
                .WithMany(m => m.ProcessDetails)
                .HasForeignKey(pd => pd.ModelsId);

            modelBuilder.Entity<ProcessDetail>()
                .HasOne(pd => pd.Part)
                .WithMany(p => p.ProcessDetails)
                .HasForeignKey(pd => pd.PartsId);

            modelBuilder.Entity<ProcessComponent>()
                .HasOne(pc => pc.Part)
                .WithMany(p => p.ProcessComponents)
                .HasForeignKey(pc => pc.PartsId);

            modelBuilder.Entity<ProcessComponent>()
                .HasOne(pc => pc.WorkCenter)
                .WithMany(wc => wc.ProcessComponents)
                .HasForeignKey(pc => pc.WorkCentersId);

            modelBuilder.Entity<Schedule>()
                .HasOne(sc => sc.Model)
                .WithMany(m => m.Schedules)
                .HasForeignKey(sc => sc.ModelId);

            modelBuilder.Entity<ScheduleDetail>()
                 .HasOne(sd => sd.Schedule)
                 .WithMany(sc => sc.ScheduleDetails)
                 .HasForeignKey(sd => sd.ScheduleId);

            modelBuilder.Entity<ScheduleDetail>()
                 .HasOne(sd => sd.Part)
                 .WithMany(p => p.ScheduleDetails)
                 .HasForeignKey(sd => sd.PartId);

            modelBuilder.Entity<ScheduleDetail>()
                 .HasOne(sd => sd.WorkCenter)
                 .WithMany(wc => wc.ScheduleDetails)
                 .HasForeignKey(sd => sd.WorkCenterId);

            // Optional performance indexes
            modelBuilder.Entity<ProcessDetail>()
                .HasIndex(pd => new { pd.ModelsId, pd.PartsId, pd.OperationNumber });

            modelBuilder.Entity<ProcessComponent>()
                .HasIndex(pc => new { pc.PartsId, pc.OperationNumber });
        }
    }
}
