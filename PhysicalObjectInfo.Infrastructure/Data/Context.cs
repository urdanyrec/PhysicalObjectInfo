using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhysicalObjectInfo.Domain;

namespace PhysicalObjectInfo.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) 
        { 
        
        }
        public DbSet<PhysicalObject> PhysicalObjects { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<SampleValue> SampleValues { get; set; }
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PhysicalObjectInfoDb;Trusted_Connection=True;");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<PhysicalObject>().HasMany(p => p.Parameters).WithOne(t => t.PhysicalObject).HasForeignKey(t => t.PhysicalObjectId);
            //modelBuilder.Entity<PhysicalObject>().HasMany(p => p.Parameters).WithOne(t => t.PhysicalObject).HasForeignKey(p => p.PhysicalObjectId); //последняя версия
            modelBuilder.Entity<PhysicalObject>().HasMany(p => p.Parameters);
            //modelBuilder.Entity<PhysicalObject>().
            //modelBuilder.Entity<Parameter>().HasOne(p => p.PhysicalObject).WithMany(t => t.Parameters).HasForeignKey(f => f.PhysicalObjectId);
            //modelBuilder.Entity<Parameter>().HasKey(p => p.Id);
            //modelBuilder.Entity<Parameter>().HasMany(p => p.SampleValues);
        }
    }

}
