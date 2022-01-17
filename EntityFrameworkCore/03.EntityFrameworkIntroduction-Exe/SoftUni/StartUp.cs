namespace SoftUni
{
    using Microsoft.EntityFrameworkCore;
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new SoftUniContext();

            //03.Employees Full Information
            //Console.WriteLine(GetEmployeesFullInformation(context));

            //04. Employees with Salary Over 50 000
            //Console.WriteLine(GetEmployeesWithSalaryOver50000(context));

            //05.Employees from Research and Development
            //Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));

            //06.Adding a New Address and Updating Employee
            //Console.WriteLine(AddNewAddressToEmployee(context));

            //07. Employees and Projects
            //Console.WriteLine(GetEmployeesInPeriod(context));

            //08. Addresses by Town
            //Console.WriteLine(GetAddressesByTown(context));

            //09. Employee 147
            //Console.WriteLine(GetEmployee147(context));

            //10. Departments with More Than 5 Employees
            //Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));

            //11. Find Latest 10 Projects
            //Console.WriteLine(GetLatestProjects(context));

            //12. Increase Salaries
            //Console.WriteLine(IncreaseSalaries(context));

            //13. Find Employees by First Name Starting With Sa
            //Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));

            //14. Delete Project by Id
            //Console.WriteLine(DeleteProjectById(context));

            //15.Remove Town
            Console.WriteLine(RemoveTown(context));
        }

        //03. Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var fullInfo = context.Employees
                                  .Select(e => new
                                  {
                                      e.EmployeeId,
                                      e.FirstName,
                                      e.LastName,
                                      e.MiddleName,
                                      e.JobTitle,
                                      e.Salary
                                  })
                                  .OrderBy(e => e.EmployeeId)
                                  .ToList();

            var result = new StringBuilder();

            foreach (var e in fullInfo)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");
            }

            return result.ToString();
        }

        //04. Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                                   .Where(e => e.Salary > 50000)
                                   .OrderBy(e => e.FirstName)
                                   .ToList();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} - {e.Salary:F2}");
            }

            return result.ToString();
        }

        //05. Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var employees = context.Employees
                                   .Where(e => e.Department.Name == "Research and Development")
                                   .OrderBy(e => e.Salary)
                                   .ThenByDescending(e => e.FirstName)
                                   .ToList();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} from Research and Development - ${e.Salary:F2}");
            }

            return result.ToString();
        }

        //06. Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var searchByName = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            searchByName.Address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.SaveChanges();

            var addresses = context.Employees
                                   .Select(a => new
                                   {
                                       a.AddressId,
                                       a.Address.AddressText
                                   })
                                   .OrderByDescending(a => a.AddressId)
                                   .Take(10)
                                   .ToList();

            var result = new StringBuilder();

            foreach (var a in addresses)
            {
                result.AppendLine(a.AddressText);
            }

            return result.ToString();
        }

        //07. Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Include(e => e.EmployeesProjects)
                .ThenInclude(e => e.Project)
                .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 &&
                                                    p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    eFirstName = e.FirstName,
                    eLastName = e.LastName,
                    mFirstName = e.Manager.FirstName,
                    mLastName = e.Manager.LastName,
                    projects = e.EmployeesProjects.Select(p => new
                    {
                        pName = p.Project.Name,
                        startDate = p.Project.StartDate,
                        endDate = p.Project.EndDate
                    })
                })
                .Take(10)
                .ToList();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.eFirstName} {e.eLastName} - Manager: {e.mFirstName} {e.mLastName}");

                foreach (var p in e.projects)
                {
                    var startDate = p.startDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                    var endDate = p.endDate.HasValue ? p.endDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture) : "not finished";

                    result.AppendLine($"--{p.pName} - {startDate} - {endDate}");
                }
            }

            return result.ToString();
        }

        //08. Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .Select(a => new
                {
                    a.AddressText,
                    a.Town.Name,
                    a.Employees.Count
                })
                .OrderByDescending(a => a.Count)
                .ThenBy(a => a.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            var result = new StringBuilder();

            foreach (var a in addresses)
            {
                result.AppendLine($"{a.AddressText}, {a.Name} - {a.Count} employees");
            }

            return result.ToString();
        }

        //09. Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    projects = e.EmployeesProjects.Select(pn => new
                    {
                        pn.Project.Name
                    })
                    .OrderBy(pn => pn.Name)
                    .ToList()
                })
               .FirstOrDefault(e => e.EmployeeId == 147);

            var result = new StringBuilder();

            result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var p in employee.projects)
            {
                result.AppendLine(p.Name);
            }

            return result.ToString();
        }

        //10. Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departments = context.Departments
                .Where(e => e.Employees.Count > 5)
                .OrderBy(e => e.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    depName = d.Name,
                    managerFirstName = d.Manager.FirstName,
                    managerLastName = d.Manager.LastName,
                    employees = d.Employees
                                 .Select(e => new
                                 {
                                     emplFirstName = e.FirstName,
                                     emplLastName = e.LastName,
                                     emplJobTitle = e.JobTitle,
                                 })
                .OrderBy(e => e.emplFirstName)
                .ThenBy(e => e.emplLastName)
                .ToList()
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var d in departments)
            {
                result.AppendLine($"{d.depName} - {d.managerFirstName} {d.managerLastName}");

                foreach (var e in d.employees)
                {
                    result.AppendLine($"{e.emplFirstName} {e.emplLastName} - {e.emplJobTitle}");
                }
            }

            return result.ToString();
        }

        //11. Find Latest 10 Projects
        public static string GetLatestProjects(SoftUniContext context)
        {

            var projects = context.Projects
                .Select(p => new
                {
                    Date = p.StartDate,
                    Name = p.Name,
                    Descrition = p.Description
                })
                .OrderByDescending(p => p.Date)
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            var result = new StringBuilder();

            foreach (var p in projects)
            {
                result.AppendLine($"{p.Name}\n{p.Descrition}\n{p.Date.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}");
            }

            return result.ToString();
        }

        //12. Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            var departments = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };
            var employees = context.Employees
                .Where(e => departments.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            foreach (var e in employees)
            {
                e.Salary *= 1.12m;
            }

            context.SaveChanges();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
            }

            return result.ToString();
        }

        //13. Find Employees by First Name Starting With Sa
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
            }

            return result.ToString();
        }

        //14. Delete Project by Id
        public static string DeleteProjectById(SoftUniContext context)
        {
            var project = context.Projects.Find(2);
            var employeesToRemove = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            foreach (var e in employeesToRemove)
            {
                context.Remove(e);
            }

            context.Projects.Remove(project);
            context.SaveChanges();

            var result = new StringBuilder();

            var projects = context.Projects
                .Take(10)
                .ToList();

            foreach (var p in projects)
            {
                result.AppendLine(p.Name);
            }

            return result.ToString();
        }

        //15. Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            var town = context.Towns.FirstOrDefault(t=>t.Name == "Seattle");

            var addresses = context.Addresses.Where(t=>t.TownId == town.TownId).ToList();

            foreach (var a in addresses)
            {
                var employees = context.Employees.Where(e => e.AddressId == a.AddressId).ToList();

                foreach (var e in employees)
                {
                    e.AddressId = null;
                }

                context.Addresses.Remove(a);
            }

            context.Towns.Remove(town);

            context.SaveChanges();

            return $"{addresses.Count()} addresses in Seattle were deleted";
        }

    }
}
