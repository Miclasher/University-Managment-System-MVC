using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Domain.Models;

namespace University.Infrastructure.Configurations
{
    internal sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("COURSES");

            builder.HasKey(course => course.Id);

            builder.Property(course => course.Id)
                .HasColumnName("COURSE_ID")
                .IsRequired();

            builder.Property(course => course.Name)
                .HasColumnName("NAME")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(course => course.Description)
                .HasColumnName("DESCRIPTION")
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(course => course.Groups);
        }
    }
}
