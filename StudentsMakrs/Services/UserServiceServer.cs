using Microsoft.AspNetCore.Mvc;
using StudentsMakrs.Client.Interfaces;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Services
{
    public class UserServiceServer : IUserService
    {
        public Task<IActionResult> Delete(string t)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(string ID)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> Gets()
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post(User t)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Put(User t)
        {
            throw new NotImplementedException();
        }
    }
}
