using MyProject.Application.Dtos;
using MyProject.Framework.Paging;

namespace MyProject.Application
{
    public interface ITestService
    {
        StudentDto AddStudent(StudentDto dto);

        PageResult<StudentDto> GetStudents(PageParam pageParam);
    }
}
