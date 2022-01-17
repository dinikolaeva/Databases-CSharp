namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using CarDealer.XMLHelper;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var projects = context.Projects
                .ToArray()
                .Where(p => p.Tasks.Any())
                .Select(p => new ExportProjectDto
                {
                    Name = p.Name,
                    TasksCount = p.Tasks.Count,
                    HasEndDate = p.DueDate.HasValue ? "Yes" : "No",
                    Tasks = p.Tasks
                             .Select(t => new ExportTaskDto
                             {
                                 TaskName = t.Name,
                                 Label = t.LabelType.ToString()
                             })
                             .OrderBy(t => t.TaskName)
                             .ToArray()
                })
                .OrderByDescending(tc => tc.TasksCount)
                .ThenBy(p => p.Name)
                .ToList();

            return XmlConverter.Serialize(projects, "Projects");
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context
                .Employees
                .ToArray()
                .Where(e => e.EmployeesTasks.Any(od => od.Task.OpenDate >= date))
                .Select(t => new
                {
                    Username = t.Username,
                    Tasks = t.EmployeesTasks
                             .Where(od => od.Task.OpenDate >= date)
                             .Select(t => t.Task)
                             .OrderByDescending(d => d.DueDate)
                             .ThenBy(n => n.Name)
                             .Select(et => new
                             {
                                 TaskName = et.Name,
                                 OpenDate = et.OpenDate
                                              .ToString("d", CultureInfo.InvariantCulture),
                                 DueDate = et.DueDate
                                                .ToString("d", CultureInfo.InvariantCulture),
                                 LabelType = et.LabelType.ToString(),
                                 ExecutionType = et.ExecutionType.ToString()
                             })
                             .ToArray()
                })
                .OrderByDescending(t => t.Tasks.Length)
                .ThenBy(un => un.Username)
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(employees, Formatting.Indented);
        }
    }
}