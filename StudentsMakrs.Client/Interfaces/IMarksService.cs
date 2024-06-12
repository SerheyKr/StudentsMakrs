using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Client.Interfaces
{
    public interface IMarksService
    {
        Task<Mark> GetMark(int ID);
        Task<List<Mark>> GetMarks();
        Task<IActionResult> PostMark(Mark t);
        Task<IActionResult> PutMark(Mark t);
        Task<IActionResult> DeleteMark(int t);
    }
}
