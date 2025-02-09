using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University.Services.Abstractions
{
    public interface IServiceManager
    {
        public ICourseService CourseService { get; }
        public ITeacherService TeacherService { get; }
        public IGroupService GroupService { get; }
        public IStudentService StudentService { get; }
    }
}
