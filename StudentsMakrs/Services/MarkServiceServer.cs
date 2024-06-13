using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class MarkServiceServer : IMarksService
    {
        public async Task<IActionResult> DeleteMark(int t)
        {
            var context = Program.GetDBContext();

            var mark = await context.Marks.FindAsync(t);
            context.Marks.Remove(mark);
            await context.SaveChangesAsync();

            return new OkObjectResult(t);
        }

        public Task<Mark> GetMark(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Mark>> GetMarks()
        {
            var context = Program.GetDBContext();
            var x = await context.Marks.ToListAsync();

            return x;
        }

        public async Task<IActionResult> PostMark(Mark t)
        {
            var context = Program.GetDBContext();

            await context.Marks.AddAsync(t);
            await context.SaveChangesAsync();

            return new OkObjectResult(t);
        }

        public Task<IActionResult> PutMark(Mark t)
        {
            throw new NotImplementedException();
        }
    }
}
