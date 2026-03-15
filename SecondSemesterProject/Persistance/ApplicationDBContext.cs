using Microsoft.EntityFrameworkCore;
using SecondSemesterProject.Models;


namespace SecondSemesterProject.Persistance

{
    public class ApplicationDBContext: DbContext
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

    }
}
