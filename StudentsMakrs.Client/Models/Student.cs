using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsMakrs.Client.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        [Key]
        public string StudentID { get; set; }
        public string StudentPassword { get; set; }

        public string FullName => $"{FirstName} {Surname} {LastName}";

        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual List<Mark> Marks { get; set; }
        [JsonIgnore]
        public virtual List<StudentSubject> StudentSubjects { get; set; }
        public virtual List<Subject> Subjects { get; set; }
    }
}
