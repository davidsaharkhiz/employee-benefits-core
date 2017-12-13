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

			var students = new Employee[]
			{
				new Employee("April", 2),
				new Employee("Barry", 2),
				new Employee("Charlie", 2)
			};
			foreach (Employee s in students)
			{
				context.Employees.Add(s);
			}
			context.SaveChanges();
			
		}
	}
}