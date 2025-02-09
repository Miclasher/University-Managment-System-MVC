using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Domain.Models;

namespace University.Infrastructure.Configurations
{
    internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("STUDENTS");

            builder.HasKey(student => student.Id);

            builder.Property(student => student.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FIRST_NAME");

            builder.Property(student => student.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LAST_NAME");

            builder.Property(student => student.Id)
                .IsRequired()
                .HasColumnName("STUDENT_ID");

            builder.Property(student => student.GroupId)
                .IsRequired()
                .HasColumnName("GROUP_ID");

            builder.HasOne(student => student.Group);
        }
    }
}
