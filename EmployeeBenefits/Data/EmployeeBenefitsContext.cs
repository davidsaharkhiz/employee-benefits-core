using Microsoft.EntityFrameworkCore;
using EmployeeBenefits.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace EmployeeBenefits.Data
{
	public class EmployeeBenefitsContext : DbContext
	{
		public EmployeeBenefitsContext(DbContextOptions<EmployeeBenefitsContext> options) : base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Dependent> Dependents { get; set; }
		public DbSet<Discount> Discounts { get; set; }

		public DbSet<EmployeeDependent> EmployeeDependents { get; set; }

		/// <summary>
		/// I was looking into implementing expression trees but ran out of time. This is less than ideal!!!
		/// I really hate this because it violates the open-closed principle but I needed more to to work around it.
		/// </summary>
		public List<Discount> DiscountsWithCalculations { 
			get
			{
				var validDiscounts = new List<Discount>();

				foreach(var discount in Discounts) {
					switch(discount.Name) {
						case Discount.Names.BEGINS_WITH_LETTER_CASE_INSENSITIVE:
							if(string.IsNullOrEmpty(discount.OptionalOperand)) {
								throw new ArgumentException("Operand was not supplied but was necessary for this discount!");
							}
							discount.DiscountCalculation = delegate (IPerson person)
							{
								return person.Name.ToLower().StartsWith(discount.OptionalOperand.ToLower()) ? discount.Amount : 0;
							};
							break;
						default:
							continue;
					}
					validDiscounts.Add(discount);
				}

				return validDiscounts;
			}
		}

		/// <summary>
		/// Convenience method to automatically fetch all associated records and apply all discounts
		/// </summary>
		public IQueryable<Employee> EmployeesWithAllData { 
			get {
				var employees = Employees.Include(e => e.EmployeeDependents).ThenInclude(e => e.Dependent);
				var discountsWithCalculations = DiscountsWithCalculations;
				foreach (var employee in employees) {
					employee.DiscountHelper = new Helpers.DiscountHelper(discountsWithCalculations);
					///but what about dependent needing a helper?!?!? #todo
				}
				
				return employees;
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Define composite key for our join table
			modelBuilder.Entity<EmployeeDependent>()
				.HasKey(t => new { t.EmployeeId, t.DependentId });
		}

	}
}