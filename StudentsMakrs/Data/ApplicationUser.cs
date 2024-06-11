using Microsoft.AspNetCore.Identity;
using StudentsMakrs.Client.Models;

namespace StudentsMakrs.Data
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		public bool IsTeacher { get; set; }
		public string? StudentId { get; set; }
		public virtual Student? Student { get; set; }
	}
}
