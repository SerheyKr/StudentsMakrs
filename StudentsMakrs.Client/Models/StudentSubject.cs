namespace StudentsMakrs.Client.Models
{
    public class StudentSubject
    {
        public int StudentId1 { get; set; }
        public int SubjectId1 { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
