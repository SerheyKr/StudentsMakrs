using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;
using System.Net.Http.Json;

namespace StudentsMakrs.Client.Services
{
    public class StudentServiceClient(HttpClient httpClient) : IStudentService
    {
        public async Task<IActionResult> RemoveSubject(Student student, int subject)
        {
            await httpClient.PutAsJsonAsync($"/Students/{subject}/DeleteSubject", student);
            return new OkResult();
        }

        public async Task<IActionResult> AddSubjectToStudent(Student student, int subject)
        {
            await httpClient.PostAsJsonAsync($"/Students/{subject}/AddSubject", student);
            return new OkResult();
        }

        public async Task<IActionResult> DeleteStudent(string student)
        {
            await httpClient.DeleteAsync($"/Students/Delete/{student}");
            return new OkResult();
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

        public async Task<IActionResult> PutStudent(Student student)
        {
            await httpClient.PutAsJsonAsync("/Students/Put", student);
            return new OkResult();
        }
    }
}
