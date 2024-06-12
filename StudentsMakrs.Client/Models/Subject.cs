using StudentsMakrs.Client.Pages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsMakrs.Client.Models
{
    public class Subject
    {
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public virtual List<Student> Students { get; set; }
        public virtual List<Mark> Marks { get; set; }
        [JsonIgnore]
        public virtual List<StudentSubject> StudentSubjects { get; set; }
    }
}
