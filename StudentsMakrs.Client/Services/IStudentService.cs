using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Client.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudents();
        Task<bool> PostStudent();
    }
}
