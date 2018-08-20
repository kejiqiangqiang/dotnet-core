using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ConsoleApp1
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        //新版本才支持MyDbContext中表与属性名不一致
        //public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Subscribe> Subscribes { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Subscribe> Subscribe { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["EFOS.Master"].ConnectionString);
                //optionsBuilder.UseSqlServer(@"Data Source=192.168.3.179;timeout=210;Initial Catalog=EFOS.Master;User ID=sa;Password=111111;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity=> {
                entity.Property(e => e.Id).IsRequired();
            });
            modelBuilder.Entity<Subscribe>(entity=> {
                //entity.HasOne(d => d.Blog)
                //    .WithMany(p => p.Post)
                //    .HasForeignKey(d => d.BlogId);
            });
        }
    }
}
