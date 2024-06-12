using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;
using System.Net.Http.Json;

namespace StudentsMakrs.Client.Services
{
    public class MarkServiceClient(HttpClient httpClient) : IMarksService
    {
        public async Task<IActionResult> DeleteMark(int t)
        {
            await httpClient.DeleteAsync($"/Marks/Delete/{t}");
            return new OkResult();
        }

        public Task<Mark> GetMark(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Mark>> GetMarks()
        {
            return await httpClient.GetFromJsonAsync<List<Mark>>("/Marks/All") ?? throw new InvalidProgramException();
        }

        public async Task<IActionResult> PostMark(Mark t)
        {
            await httpClient.PostAsJsonAsync("/Marks/Post", t);
            return new OkResult();
        }

        public Task<IActionResult> PutMark(Mark t)
        {
            throw new NotImplementedException();
        }
    }
}
