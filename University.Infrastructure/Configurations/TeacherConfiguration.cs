using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Domain.Models;

namespace University.Infrastructure.Configurations
{
    internal sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("TEACHERS");

            builder.HasKey(teacher => teacher.Id);

            builder.Property(teacher => teacher.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("FIRST_NAME");

            builder.Property(teacher => teacher.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LAST_NAME");

            builder.Property(teacher => teacher.Id)
                .IsRequired()
                .HasColumnName("TEACHER_ID");

            builder.HasMany(teacher => teacher.Groups);
        }
    }
}
