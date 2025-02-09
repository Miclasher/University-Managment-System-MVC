using Mapster;
using University.Domain.Models;
using University.Shared;

namespace University.Services
{
    internal static class MaplerConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<Course, CourseDTO>.NewConfig()
                .Map(dest => dest.Groups, src => src.Groups.Adapt<List<GroupDTO>>());

            TypeAdapterConfig<Teacher, TeacherDTO>.NewConfig()
                .Map(dest => dest.Groups, src => src.Groups.Adapt<List<GroupDTO>>());

            TypeAdapterConfig<Student, StudentDTO>.NewConfig()
                .Map(dest => dest.Group, src => src.Group.Adapt<GroupDTO>());

            TypeAdapterConfig<Group, GroupDTO>.NewConfig()
                .Map(dest => dest.Students, src => src.Students.Adapt<List<StudentDTO>>())
                .Map(dest => dest.Teacher, src => src.Teacher.Adapt<TeacherDTO>())
                .Map(dest => dest.Course, src => src.Course.Adapt<CourseDTO>());
        }
    }
}
