using System.ComponentModel.DataAnnotations;

namespace StudentsMakrs.Client.Models
{
    public class Mark
    {
        public int CurrentMark { get; set; }
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
        [Key]
        public int Id { get; set; }
        public DateTime MarkDate { get; set; }
    }
}
