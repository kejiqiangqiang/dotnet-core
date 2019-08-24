using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Samples.EFLogging;

namespace TodoApi.Models
{
    public class TodoContext:DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            :base(options)
        {
            ////EFLogging Extend--2
            //DbContextLoggingExtensions.ConfigureLogging(this,s => Console.WriteLine(s));
        }
        public DbSet<TodoItem> TodoItems{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ////EFLogging Extend--3
            //DbContextLoggingExtensions.ConfigureLogging(this,s => Console.WriteLine(s));
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.;Database=dotnet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }
}
