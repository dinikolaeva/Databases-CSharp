namespace MiniORM.App
{
    using MiniORM.app.Data;
    using MiniORM.app.Data.Entities;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var connectionString = "Server=.;Database=MiniORM;Integrated Security =true";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            Employee employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges();
        }
    }
}
