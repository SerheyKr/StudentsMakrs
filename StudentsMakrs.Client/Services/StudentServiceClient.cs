using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;
using System.Net.Http.Json;

namespace StudentsMakrs.Client.Services
{
    public class StudentServiceClient(HttpClient httpClient) : IStudentService
    {
        public async Task<IActionResult> DeleteStudent(string student)
        {
            await httpClient.DeleteAsync($"/Students/Delete/{student}");
            throw new NotImplementedException();
        }

        public async Task<Student> GetStudent(string ID)
        {
            return await httpClient.GetFromJsonAsync<Student>($"/Students/Get/{ID}") ?? throw new InvalidProgramException();
        }

        public async Task<List<Student>> GetStudents()
        {
            return await httpClient.GetFromJsonAsync<List<Student>>("/Students/All") ?? throw new InvalidProgramException();
        }

        public async Task<IActionResult> PostStudent(Student student)
        {
            await httpClient.PostAsJsonAsync("/Students/Post", student);
            return new OkResult();
        }

        public Task<IActionResult> PutStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
