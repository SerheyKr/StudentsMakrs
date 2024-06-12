using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class FacultyServiceServer : IFacultyService
    {
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var context = Program.GetDBContext();
            var item = await context.Departments.FindAsync(id) ?? throw new ArgumentException($"Givend id is not finded {id}", nameof(id));
            context.Departments.Remove(item);
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public async Task<IActionResult> DeleteFaculty(int id)
        {
            var context = Program.GetDBContext();
            var item = await context.Faculties.FindAsync(id) ?? throw new ArgumentException($"Givend id is not finded {id}", nameof(id));
            context.Faculties.Remove(item);
            await context.SaveChangesAsync();

            return new OkResult();
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
