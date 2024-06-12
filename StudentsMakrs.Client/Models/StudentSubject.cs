using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentsMakrs.Client.Models
{
    public class StudentSubject
    {
        public string StudentIdSec { get; set; }
        public virtual Student Student { get; set; }

        public int SubjectIdSec { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
