using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Models;
using System.Reflection.Emit;

namespace StudentsMakrs.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Mark> Marks { get; set; }
		public DbSet<Subject> Subjects { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Department>().HasKey(d => d.Id);
			modelBuilder.Entity<Student>().HasMany(x => x.Subjects).WithMany(x => x.Students).UsingEntity<StudentSubject>();
			modelBuilder.Entity<Student>().HasOne(x => x.Department).WithMany(x => x.Students).HasForeignKey(x => x.DepartmentId);
			modelBuilder.Entity<Student>().HasOne(x => x.Faculty).WithMany(x => x.Students).HasForeignKey(x => x.FacultyId);

			modelBuilder.Entity<Student>().HasMany(x => x.Marks).WithOne(x => x.Student).HasForeignKey(x => x.StudentId);

			modelBuilder.Entity<ApplicationUser>().HasOne(x => x.Student).WithOne();
		}
	}
}
