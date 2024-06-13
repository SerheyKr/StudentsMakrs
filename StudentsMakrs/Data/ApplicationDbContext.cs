using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsMakrs.Client.Models;
using System.Reflection.Emit;

namespace StudentsMakrs.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Faculty> Faculties { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Mark> Marks { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<StudentSubject> StudentSubjects { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
            .HasMany(s => s.Subjects)
            .WithMany(s => s.Students)
            .UsingEntity<StudentSubject>(
                j => j
                    .HasOne(ss => ss.Subject)
                    .WithMany()
                    .HasForeignKey(ss => ss.SubjectIdSec),
                j => j
                    .HasOne(ss => ss.Student)
                    .WithMany()
                    .HasForeignKey(ss => ss.StudentIdSec),
                j =>
                {
                    j.HasKey(t => new { t.StudentIdSec, t.SubjectIdSec });
                });


            modelBuilder.Entity<Student>().HasOne(x => x.Department).WithMany(x => x.Students).HasForeignKey(x => x.DepartmentId);
			modelBuilder.Entity<Student>().HasOne(x => x.Faculty).WithMany(x => x.Students).HasForeignKey(x => x.FacultyId);

			modelBuilder.Entity<Student>().HasMany(x => x.Marks).WithOne(x => x.Student).HasForeignKey(x => x.StudentId);

			modelBuilder.Entity<Mark>().HasOne(x => x.Subject).WithMany(x => x.Marks).HasForeignKey(x => x.SubjectId);

            modelBuilder.Entity<ApplicationUser>().HasOne(x => x.Student).WithOne();
		}
	}
}
