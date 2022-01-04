namespace MiniORM.app.Data
{
    using Entities;

    public class SoftUniDbContext : DbContext
    {
        public SoftUniDbContext(string connectonString) 
            : base(connectonString)
        {
        }

        public DbSet<Employee> Employees { get;}
        public DbSet<Department> Departments { get;}
        public DbSet<Project> Projects { get; }
        public DbSet<EmployeeProject> EmployeeProjects { get; }
    }
}
