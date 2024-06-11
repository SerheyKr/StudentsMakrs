using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Client.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<T> Get(long ID);
        Task<List<T>> Gets();
        Task<IActionResult> Post(T t);
        Task<IActionResult> Put(T t);
        Task<IActionResult> Delete(T t);
    }
}
