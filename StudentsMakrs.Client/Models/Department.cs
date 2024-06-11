using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentsMakrs.Client.Models
{
    public class Department
    {
        public string DepartmentName { get; set; }
        public string BranchName { get; set; }

        public string FullName => $"{DepartmentName}-{BranchName}";
        [Key] 
        public int Id { get; set; }
        [JsonIgnore]
        public virtual List<Student>? Students { get; set; }
    }
}
