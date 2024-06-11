using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class FacultyServiceServer : IFacultyService
    {
        public Task<IActionResult> DeleteDepartment(Department t)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> DeleteFaculty(Faculty t)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetDepartment(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await Program.GetDBContext().Departments.ToListAsync();
        }

        public Task<Faculty> GetFaculty(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Faculty>> GetFaculties()
        {
            return await Program.GetDBContext().Faculties.ToListAsync();
        }

        public async Task<IActionResult> PostDepartment(Department t)
        {
            var context = Program.GetDBContext();
            await context.Departments.AddAsync(t);
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> PostFaculty(Faculty t)
        {
            var context = Program.GetDBContext();
            await context.Faculties.AddAsync(t);
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> PutDepartment(Department t)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutFaculty(Faculty t)
        {
            throw new NotImplementedException();
        }
    }
}
