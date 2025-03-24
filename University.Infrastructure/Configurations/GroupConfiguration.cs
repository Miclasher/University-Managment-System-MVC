using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Domain.Models;

namespace University.Infrastructure.Configurations
{
    internal sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("GROUPS");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("GROUP_ID")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("NAME")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.CourseId)
                .HasColumnName("COURSE_ID")
                .IsRequired();

            builder.Property(e => e.TeacherId)
                .HasColumnName("TEACHER_ID")
                .IsRequired();

            builder.HasOne(e => e.Course);

            builder.HasOne(e => e.Teacher);

            builder.HasMany(e => e.Students);
        }
    }
}
