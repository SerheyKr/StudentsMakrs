using System.ComponentModel.DataAnnotations;

namespace StudentsMakrs.Client.Models
{
    public class Subject
    {
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
