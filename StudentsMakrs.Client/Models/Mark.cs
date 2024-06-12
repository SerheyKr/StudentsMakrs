using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsMakrs.Client.Models
{
    public class Mark
    {
        public int CurrentMark { get; set; }
        public string StudentId { get; set; }
        public int SubjectId { get; set; }
        [JsonIgnore]
        public virtual Student Student { get; set; }
        [JsonIgnore]
        public virtual Subject Subject { get; set; }
        [Key]
        public int Id { get; set; }
        public DateTime MarkDate { get; set; }
    }
}
