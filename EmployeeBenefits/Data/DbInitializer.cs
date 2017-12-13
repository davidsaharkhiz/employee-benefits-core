using EmployeeBenefits.Models;
using System.Linq;

namespace EmployeeBenefits.Data
{
	public static class DbInitializer
	{
		public static void Initialize(EmployeeBenefitsContext context)
		{
			context.Database.EnsureCreated();

			// Quick demo hack to seed the test DB
			if (context.Employees.Any())
			{
				return;   // DB has been seeded
			}

			var employees = new Employee[]
			{
				new Employee("April"),
				new Employee("Barry"),
				new Employee("Charlie")
			};
			foreach (Employee s in employees)
			{
				context.Employees.Add(s);
			}

			var dependents = new Dependent[]
			{
				new Dependent("Jack Jack"),
				new Dependent("Dinosaur Junior"),
				new Dependent("BB-8")
			};
			foreach (Employee s in employees)
			{
				context.Employees.Add(s);
			}

			context.AddRange(
				new EmployeeDependent { 
					Employee = employees[0],
					Dependent = dependents[0]
				},
				new EmployeeDependent
				{
					Employee = employees[1],
					Dependent = dependents[0]
				},
				new EmployeeDependent
				{
					Employee = employees[2],
					Dependent = dependents[2]
				}
			);

			context.SaveChanges();
			
		}
	}
}