using Lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Exam> Exams { get; set; }

        const string Host = "localhost";
        const string Db = "student_perfomance_2";
        const string User = "root";
        const string Password = "";

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql($"Database={Db};Datasource={Host};User={User};Password={Password}");
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
