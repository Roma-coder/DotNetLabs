using Lab4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab4
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Exam> Exams { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .HasOne(exam => exam.Student)
                .WithMany(o => o.Exams)
                .HasForeignKey(exam => exam.StudentId);

            modelBuilder.Entity<Exam>()
                .HasOne(exam => exam.Subject)
                .WithMany(o => o.Exams)
                .HasForeignKey(exam => exam.SubjectId);

            modelBuilder.Entity<Exam>()
                .HasOne(exam => exam.Teacher)
                .WithMany(o => o.Exams)
                .HasForeignKey(exam => exam.TeacherId);
        }
    }
}
