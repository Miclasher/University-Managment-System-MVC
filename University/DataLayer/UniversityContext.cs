﻿using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.DataLayer
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(e => e.ToTable("COURSES"));
            modelBuilder.Entity<Group>(e => e.ToTable("GROUPS"));
            modelBuilder.Entity<Student>(e => e.ToTable("STUDENTS"));
            modelBuilder.Entity<Teacher>(e => e.ToTable("TEACHERS"));
        }
    }
}
