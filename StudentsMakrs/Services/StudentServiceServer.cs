using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class StudentServiceServer : IStudentService
    {
        public async Task<IActionResult> RemoveSubject(Student student, int subject)
        {
            var context = Program.GetDBContext();
            var toDelete = await context.StudentSubjects.Where(x => x.SubjectIdSec == subject && x.StudentIdSec == student.StudentID).FirstAsync();
            context.StudentSubjects.Remove(toDelete);
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> AddSubjectToStudent(Student student, int subject)
        {
            var context = Program.GetDBContext();
            await context.StudentSubjects.AddAsync(new StudentSubject()
            {
                StudentIdSec = student.StudentID,
                SubjectIdSec = subject,
            });
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> DeleteStudent(string student)
        {
            var context = Program.GetDBContext();
            var std = await context.Students.FindAsync(student) ?? throw new ArgumentException($"Givend id is not finded {student}", nameof(student));
            context.Students.Remove(std);
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<Student> GetStudent(string ID)
        {
            var context = Program.GetDBContext();
            var std = await context.Students.FindAsync(ID) ?? throw new ArgumentException($"Givend id is not finded {ID}", nameof(ID));

            return std;
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

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            return new OkObjectResult(student);
        }

        public async Task<IActionResult> PutStudent(Student student)
        {
            var context = Program.GetDBContext();

            context.Students.Update(student);
            await context.SaveChangesAsync();

            return new OkObjectResult(student);
        }
    }
}
