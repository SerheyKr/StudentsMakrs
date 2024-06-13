using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Client.Interfaces
{
    public interface IStudentService
    {
        Task<Student> GetStudent(string ID);
        Task<Student?> GetStudentAnon(CertificateData certificateData);
        Task<List<Student>> GetStudents();
        Task<IActionResult> PostStudent(Student student);
        Task<IActionResult> PutStudent(Student student);
        Task<IActionResult> DeleteStudent(string student);
        Task<IActionResult> AddSubjectToStudent(Student student, int subject);
        Task<IActionResult> RemoveSubject(Student student, int subject);
    }
}
