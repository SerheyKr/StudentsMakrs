namespace StudentsMakrs.Client.Models
{
    public class User
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public string UserEmal { get; set; }
        public Student Student { get; set; }
        public List<string> Roles { get; set; }
    }
}
