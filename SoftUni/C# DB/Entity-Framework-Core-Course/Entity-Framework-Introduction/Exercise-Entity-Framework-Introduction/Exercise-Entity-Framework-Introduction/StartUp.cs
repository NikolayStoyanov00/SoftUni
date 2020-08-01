using Microsoft.EntityFrameworkCore.Internal;
using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SoftUniContext softUniContext = new SoftUniContext();

            string result = RemoveTown(softUniContext);
            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary,
                    e.EmployeeId
                })
                .OrderBy(e => e.EmployeeId)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .OrderBy(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} from {e.Name} - ${e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Address address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownId = 4;

            context.Addresses.Add(address);

            Employee employee = context
                .Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;

            context.SaveChanges();

            var employees = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => new
                {
                    e.AddressId,
                    e.Address.AddressText
                })
                .ToList();

            foreach (var e in employees)
            {
                sb.AppendLine(e.AddressText); 
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .EmployeesProjects
                .Where(e => e.Project.StartDate.Year >= 2001 && e.Project.StartDate.Year <= 2003)
                .Select(e => new
                {
                    e.Employee.FirstName,
                    e.Employee.LastName,
                    managerFirstName = e.Employee.Manager.FirstName,
                    managerLastName = e.Employee.Manager.LastName,
                })
                .Distinct()
                .Take(10)
                .ToList();

            

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.managerFirstName} {e.managerLastName}");

                var projects = context
                .EmployeesProjects
                .Where(p => p.Employee.FirstName == e.FirstName)
                .Select(p => new
                {
                    p.Project.Name,
                    p.Project.StartDate,
                    p.Project.EndDate
                })
                .ToList();

                foreach (var p in projects)
                {
                    if (p.EndDate == null)
                    {
                        sb.AppendLine($"--{p.Name} - {p.StartDate} - not finished");
                    }
                    else
                    {
                        sb.AppendLine($"--{p.Name} - {p.StartDate} - {p.EndDate}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context
                .Addresses
                .Select(a => new
                {
                    a.AddressText,
                    townName = a.Town.Name,
                    employeeCount = a.Employees.Count()
                })
                .OrderByDescending(a => a.employeeCount)
                .ThenBy(a => a.townName)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .ToList();

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.townName} - {a.employeeCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Employee employee = context
                .Employees
                .FirstOrDefault(e => e.EmployeeId == 147);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            var projects = context
                .EmployeesProjects
                .Where(e => e.EmployeeId == 147)
                .Select(p => new
                {
                    ProjectName = p.Project.Name
                })
                .OrderBy(p => p.ProjectName)
                .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine(p.ProjectName);
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context
                .Departments
                .Where(d => d.Employees.Count() > 5)
                .Select(d => new
                {
                    EmployeesCount = d.Employees.Count(),
                    d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName
                })
                .OrderBy(d => d.EmployeesCount)
                .ThenBy(d => d.Name)
                .ToList();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");

                var employees = context
                    .Employees
                    .Where(e => e.Department.Name == d.Name)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.JobTitle
                    })
                    .OrderBy(e => e.FirstName)
                    .OrderBy(e => e.LastName)
                    .ToList();

                foreach (var e in employees)
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }

            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var projects = context
                .Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
                sb.AppendLine(p.Description);
                sb.AppendLine(p.StartDate.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Engineering" ||
                       e.Department.Name == "Tool Design" ||
                       e.Department.Name == "Marketing" ||
                       e.Department.Name == "Information Services")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    Salary = e.Salary + (decimal)e.Salary * (decimal)0.12
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

            context.SaveChanges();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context
                .Employees
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

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Project project = context
                .Projects
                .FirstOrDefault(p => p.ProjectId == 2);

            var employeesProjects = context
                .EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            for (int i = 0; i < employeesProjects.Count; i++)
            {
                EmployeeProject employeeProject = employeesProjects[i];
                context.EmployeesProjects.Remove(employeeProject);
            }

            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context
                .Projects
                .Select(p => new
                {
                    p.Name
                })
                .Take(10)
                .ToList();

            foreach (var p in projects)
            {
                sb.AppendLine(p.Name);
            }

            return sb.ToString().TrimEnd();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var employees = context
                .Employees
                .Where(e => e.Address.Town.Name == "Seattle")
                .ToList();

            for (int i = 0; i < employees.Count; i++)
            {
                Employee employee = employees[i];
                employee.AddressId = (int?)null;
            }

            context.SaveChanges();

            var addresses = context
                .Addresses
                .Where(a => a.Town.Name == "Seattle")
                .ToList();

            int removedAddresses = addresses.Count;

            for (int i = 0; i < addresses.Count; i++)
            {
                Address address = addresses[i];
                context.Addresses.Remove(address);
            }

            context.SaveChanges();

            Town town = context
                .Towns
                .FirstOrDefault(t => t.Name == "Seattle");

            context.Towns.Remove(town);
            context.SaveChanges();

            return $"{removedAddresses} addresses in Seattle were deleted";
        }
    }
}
