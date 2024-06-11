using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsMakrs.Client.Models
{
    public class Faculty
    {
        public string FacultyName { get; set; }
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public virtual List<Student>? Students { get; set; }
    }
}
