namespace StudentsMakrs.Client.Models
{
    public class Student
    {
        public string FirstName;
        public string SecondName;
        public string LastName;

        public string FullName => $"{FirstName} {SecondName} {LastName}";
        public Faculty Faculty;
        public Department Department;
        public List<Mark> Marks;
    }
}
