using StudentsMakrs.Client.Models;
using StudentsMakrs.Client.Services;

namespace StudentsMakrs.Services
{
    public class StudentServiceServer : IStudentService
    {
        public async Task<List<Student>> GetStudents()
        {
            await Task.Delay(10);

            var student = new Student()
            {
                FirstName = "First",
                SecondName = "Second",
                LastName = "Last",
                Marks =
                    [
                        new Mark()
                        {
                            CurrentMark = 100,
                            MaxMark = 100,
                        }
                    ],
                Department = new Department()
                {
                    Name = "Depart",
                },
                Faculty = new Faculty()
                {
                    Name = "Faculty",
                }
            };

            var list = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(student);
            }
            return list;
        }

        public Task<bool> PostStudent()
        {
            throw new NotImplementedException();
        }
    }
}
