using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQEmployee
{
    class Program
    {
        public static List<Employee> employees = new List<Employee>();
        public static List<Project> projects = new List<Project>();

        static void Main(string[] args)
        {
            InitializeEmployees();
            InitializeProjects();

            //WHERE
            var querySyntax1 = from employee in employees
                              where employee.EmployeeName.StartsWith("T")
                              select employee.EmployeeName;            

            Console.WriteLine("Where in querySyntax------");
            foreach (var item in querySyntax1)
            {
                Console.WriteLine(item);
            }

            var methodSyntax1 = employees.Where(e => e.EmployeeName.StartsWith("T"));
            Console.WriteLine("Where in methodSyntax-----");
            foreach (var item in methodSyntax1)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //ORDER BY ASCENDING
            var querySyntax2 = from employee in employees
                               orderby employee.EmployeeName
                               select employee.EmployeeName;


            var methodSyntax2 = employees.OrderBy(e => e.EmployeeName);

            Console.WriteLine("Order by ascending in querySyntax------");
            foreach (var item in querySyntax2)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Order by ascending in methodSyntax------");
            foreach (var item in methodSyntax2)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //ORDER BY DESCENDING
            var querySyntax3 = from employee in employees
                              orderby employee.EmployeeName descending
                              select employee.EmployeeName;


            var methodSyntax3 = employees.OrderByDescending(e => e.EmployeeName);

            Console.WriteLine("Order by descending in querySyntax------");
            foreach (var item in querySyntax3)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Order by descending in methodSyntax------");
            foreach (var item in methodSyntax3)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');




            //THEN BY

        
            var querySyntax4 = from employee in employees
                               orderby employee.ProjectId, employee.EmployeeName descending
                               select employee;


            var methodSyntax4 = employees.OrderBy(e => e.ProjectId).ThenByDescending(e => e.EmployeeName);

            Console.WriteLine("Then by in querySyntax------");
            foreach (var item in querySyntax4)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectId);
            }

            Console.WriteLine("Then by in methodSyntax------");
            foreach (var item in methodSyntax4)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectId);
            }

            Console.WriteLine('\n');

            //TAKE
            var querySyntax5 = (from employee in employees
                               select employee).Take(2);


            var methodSyntax5 = employees.Take(2);

     
            Console.WriteLine("Take in querySyntax------");
            foreach (var item in querySyntax5)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine("Take in methodSyntax------");
            foreach (var item in methodSyntax5)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');


            //SKIP
            var querySyntax6 = (from employee in employees
                                select employee).Skip(2);


            var methodSyntax6 = employees.Skip(2);

            Console.WriteLine("Skip in querySyntax------");
            foreach (var item in querySyntax6)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine("Skip in methodSyntax------");
            foreach (var item in methodSyntax6)
            {
                Console.WriteLine(item.EmployeeName);
            }

            Console.WriteLine('\n');

            //GROUP
            var querySyntax7 = from employee in employees
                              group employee by employee.ProjectId;


            var methodSyntax7 = employees.GroupBy(e => e.ProjectId);

            Console.WriteLine("Group in querySyntax------");
            foreach (var item in querySyntax7)
            {
                Console.WriteLine(item.Key + ":" + item.Count());
            }

            Console.WriteLine("Group in methodSyntax------");
            foreach (var item in methodSyntax7)
            {
                Console.WriteLine(item.Key + ":" + item.Count());
            }

            Console.WriteLine('\n');



            //FIRST
            var querySyntax8 = (from employee in employees
                                //where employee.EmployeeName.StartsWith("Q")
                                select employee).First();

            var methodSyntax8 = employees
                                //.Where(e => e.EmployeeName.StartsWith("Q"))                 
                                .First();

            Console.WriteLine("First in querySyntax------");
            if (querySyntax8 != null)
            {
                Console.WriteLine(querySyntax8.EmployeeName);
            }

            Console.WriteLine("First in methodSyntax------");
            if (methodSyntax8 != null)
            {
                Console.WriteLine(methodSyntax8.EmployeeName);
            }

            Console.WriteLine('\n');

            //FIRST OR DEFAULT
            var querySyntax9 = (from employee in employees
                                //where employee.EmployeeName.StartsWith("Q")
                                select employee).FirstOrDefault();

            var methodSyntax9 = employees
                               //.Where(e => e.EmployeeName.StartsWith("Q"))
                                .FirstOrDefault();

            Console.WriteLine("First or default in querySyntax------");
            if (querySyntax9 != null)
            {
                Console.WriteLine(querySyntax9.EmployeeName);
            }

            Console.WriteLine("First or default in methodSyntax------");
            if (methodSyntax9 != null)
            {
                Console.WriteLine(methodSyntax9.EmployeeName);
            }

            Console.WriteLine('\n');

            //JOIN
            var joinquery = from employee in employees
                              join project in projects on employee.ProjectId equals project.ProjectId
                              select new { employee.EmployeeName, project.ProjectName };

            var joinmehod = employees.Join(projects,
                                              e => e.ProjectId,
                                              p => p.ProjectId,
                                              (e, p) => new { e.EmployeeName, p.ProjectName });

            Console.WriteLine("Join in querySyntax------");
            foreach (var item in joinquery)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }


            Console.WriteLine("Join in querymethod------");
            foreach (var item in joinmehod)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }

            Console.WriteLine('\n');

            //LEFT JOIN
            var leftjoin = from employee in employees
                              join project in projects on employee.ProjectId equals project.ProjectId into group1
                              from project in group1.DefaultIfEmpty()
                              select new { employee.EmployeeName, ProjectName = project?.ProjectName ?? "NULL" };

            
            
            Console.WriteLine("Left Join------");
            foreach (var item in leftjoin)
            {
                Console.WriteLine(item.EmployeeName + ":" + item.ProjectName);
            }

            Console.WriteLine('\n');
            Console.ReadKey();
           
        }

        public static void InitializeEmployees()
        {
            employees.Add(new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Khaled",
                ProjectId = 100
            });

            employees.Add(new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Bithy",
                ProjectId = 101
            });

            employees.Add(new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Aziz",
                ProjectId = 101
            });
        }

        public static void InitializeProjects()
        {
            projects.Add(new Project
            {
                ProjectId = 100,
                ProjectName = "Padma"

            });

            projects.Add(new Project
            {
                ProjectId = 101,
                ProjectName = "IDB"

            });

        }
    }
}

