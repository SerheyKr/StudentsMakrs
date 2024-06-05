using StudentsMakrs.Client.Models;
using System.Net.Http.Json;

namespace StudentsMakrs.Client.Services
{
    public class StudentServiceClient(HttpClient httpClient) : IStudentService
    {
        public async Task<List<Student>> GetStudents()
        {
            return await httpClient.GetFromJsonAsync<List<Student>>("/Students/All") ?? throw new InvalidProgramException();
        }

        public Task<bool> PostStudent()
        {
            throw new NotImplementedException();
        }
    }
}
