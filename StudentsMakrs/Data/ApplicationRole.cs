using Microsoft.AspNetCore.Identity;

namespace StudentsMakrs.Data
{
    public class ApplicationRole: IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
