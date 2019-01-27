using MyProject.Application.Dtos;
using MyProject.Framework.Paging;
using MyProject.Repository.Repositories;

namespace MyProject.Application
{
    public class TestService : ITestService
    {
        private readonly IStudentRepository _studentRepository;

        private readonly ITeacherRepository _teacherRepository;

        public TestService(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public StudentDto AddStudent(StudentDto dto)
        {
            throw new System.NotImplementedException();
        }

        public PageResult<StudentDto> GetStudents(PageParam pageParam)
        {
            throw new System.NotImplementedException();
        }
    }
}
