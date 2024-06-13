using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class SubjectServiceServer : ISubjectService
    {
        public async Task<IActionResult> Delete(int t)
        {
            var context = Program.GetDBContext();

            var subj = await context.Subjects.FindAsync(t);

            context.Subjects.Remove(subj);
            await context.SaveChangesAsync();

            return new OkResult();
        }

        public Task<Subject> Get(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Subject>> Gets()
        {
            var context = Program.GetDBContext();
            var x = await context.Subjects.ToListAsync();

            return x;
        }

        public async Task<IActionResult> Post(Subject t)
        {
            var context = Program.GetDBContext();

            await context.Subjects.AddAsync(t);
            await context.SaveChangesAsync();

            return new OkObjectResult(t);
        }

        public Task<IActionResult> Put(Subject t)
        {
            throw new NotImplementedException();
        }
    }
}
