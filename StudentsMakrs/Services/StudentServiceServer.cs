using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class StudentServiceServer : IStudentService
    {
        public Task<IActionResult> DeleteStudent(string student)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudent(string ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> GetStudents()
        {
            var context = Program.GetDBContext();
            var x = await context.Students.ToListAsync();

            return x;
        }

        public async Task<IActionResult> PostStudent(Student student)
        {
            var context = Program.GetDBContext();
            student.StudentID = Guid.NewGuid().ToString();
            student.StudentPassword = Guid.NewGuid().ToString().Replace("-", "");

            await context.AddAsync(student);
            await context.SaveChangesAsync();

            return new OkObjectResult(student);
        }

        public Task<IActionResult> PutStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
