using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace StudentsMakrs.Client.Services
{
    public class FacultyServiceClient(HttpClient httpClient) : IFacultyService
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
            return await httpClient.GetFromJsonAsync<List<Department>>("/Department/All") ?? throw new InvalidProgramException();
        }

        public Task<Faculty> GetFaculty(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Faculty>> GetFaculties()
        {
            return await httpClient.GetFromJsonAsync<List<Faculty>>("/Faculty/All") ?? throw new InvalidProgramException();
        }

        public async Task<IActionResult> PostDepartment(Department t)
        {
            await httpClient.PostAsJsonAsync("/Department/Post", t);

            return new OkResult();
        }

        public async Task<IActionResult> PostFaculty(Faculty t)
        {
            await httpClient.PostAsJsonAsync("/Faculty/Post", t);

            return new OkResult();
        }

        public Task<IActionResult> PutDepartment(Department t)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutFaculty(Faculty t)
        {
            throw new NotImplementedException();
        }
    }
}
