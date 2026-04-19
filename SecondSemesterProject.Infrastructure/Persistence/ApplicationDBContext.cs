using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Domain.Models;


namespace SecondSemesterProject.Infrastructure.Persistence

{
    public class ApplicationDBContext: IdentityDbContext<Users, Roles, long>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }

        public DbSet<Module> Module { get; set; }

        public DbSet<Instructor> Instructor { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }

        public DbSet<ModuleInstructor> ModuleInstructor { get; set; }

        public DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Roles>().HasData(
                new Roles
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "admin-role"
                },
                new Roles
                {
                    Id = 2,
                    Name = "Instructor",
                    NormalizedName = "INSTRUCTOR",
                    ConcurrencyStamp = "instructor-role"
                },
                new Roles
                {
                    Id = 3,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    ConcurrencyStamp = "student-role"
                }
            );

            builder.Ignore<IdentityPasskeyData>();
        }

    }
}
