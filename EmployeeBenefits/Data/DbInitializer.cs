﻿using EmployeeBenefits.Models;
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

			var discount = new Discount
			{
				Active = true,
				Amount = 10,
				Name = Discount.Names.BEGINS_WITH_LETTER_CASE_INSENSITIVE,
				OptionalOperand = "A"
			};
			context.Discounts.Add(discount);
			context.SaveChanges();

			var employees = new Employee[]
			{
				new Employee {
					Name = "Artanis Jones"
				},
				new Employee {
					Name = "Billy Blazko"
				},
				new Employee {
					Name = "Zeratul"
				},
				new Employee {
					Name = "Jeff Bezos"
				},
				new Employee {
					Name = "Yosemite Sam"
				},
			};

			foreach (Employee employee in employees)
			{
				context.Employees.Add(employee);
			}

			var dependents = new Dependent[]
			{
				new Dependent("Jack Jack"),
				new Dependent("Velociraptor"),
				new Dependent("BB-8"),
				new Dependent("Aaron McDiscount"),
				new Dependent("Alfred the Butler"),
				new Dependent("Anomander Rake")
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
					Employee = employees[0],
					Dependent = dependents[2]
				},
				new EmployeeDependent
				{
					Employee = employees[0],
					Dependent = dependents[5]
				},
				new EmployeeDependent
				{
					Employee = employees[2],
					Dependent = dependents[3]
				},
				new EmployeeDependent
				{
					Employee = employees[2],
					Dependent = dependents[4]
				}
			);

			context.SaveChanges();

			


		}
	}
}