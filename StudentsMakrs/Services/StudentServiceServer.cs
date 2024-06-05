using StudentsMakrs.Client.Models;
using StudentsMakrs.Client.Services;

namespace StudentsMakrs.Services
{
    public class StudentServiceServer : IStudentService
    {
        public Task<List<Student>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostStudent()
        {
            throw new NotImplementedException();
        }
    }
}
