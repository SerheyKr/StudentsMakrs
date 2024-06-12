using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;
using System.Net.Http.Json;

namespace StudentsMakrs.Client.Services
{
    public class SubjectServiceClient(HttpClient httpClient) : ISubjectService
    {
        public async Task<IActionResult> Delete(int t)
        {
            await httpClient.DeleteAsync($"/Subject/Delete/{t}");
            return new OkResult();
        }

        public Task<Subject> Get(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subject>> Gets()
        {
            return await httpClient.GetFromJsonAsync<List<Subject>>("/Subject/All") ?? throw new InvalidProgramException();
        }

        public async Task<IActionResult> Post(Subject t)
        {
            await httpClient.PostAsJsonAsync("/Subject/Post", t);
            return new OkResult();
        }

        public Task<IActionResult> Put(Subject t)
        {
            throw new NotImplementedException();
        }
    }
}
