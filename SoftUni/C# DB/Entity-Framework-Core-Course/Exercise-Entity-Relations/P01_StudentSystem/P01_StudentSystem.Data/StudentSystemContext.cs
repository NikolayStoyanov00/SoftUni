using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
        {

        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentId);

                entity
                    .Property(s => s.Name)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity
                    .Property(s => s.PhoneNumber)
                    .HasColumnType("char(10)")
                    .IsRequired(false)
                    .IsUnicode(false);

                entity
                    .Property(s => s.Birthday);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);

                entity
                    .Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true);

                entity
                    .Property(c => c.Description)
                    .IsRequired(false)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(r => r.ResourceId);

                entity
                    .Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity
                    .Property(r => r.Url)
                    .IsUnicode(false);

                entity
                    .HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(r => r.CourseId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(h => h.HomeworkId);

                entity
                    .HasOne(h => h.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(h => h.StudentId);

                entity
                    .HasOne(h => h.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(h => h.CourseId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity
                    .HasKey(sc => new { sc.StudentId, sc.CourseId });
            });
        }
    }
}
