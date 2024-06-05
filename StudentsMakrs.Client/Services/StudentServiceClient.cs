using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Client.Services
{
    public class StudentServiceClient(HttpClient httpClient) : IStudentService
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
