using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Client.Interfaces
{
    public interface IFacultyService
    {
        Task<Department> GetDepartment(int ID);
        Task<List<Department>> GetDepartments();
        Task<IActionResult> PostDepartment(Department t);
        Task<IActionResult> PutDepartment(Department t);
        Task<IActionResult> DeleteDepartment(int id);
        Task<Faculty> GetFaculty(int ID);
        Task<List<Faculty>> GetFaculties();
        Task<IActionResult> PostFaculty(Faculty t);
        Task<IActionResult> PutFaculty(Faculty t);
        Task<IActionResult> DeleteFaculty(int id);
    }
}
