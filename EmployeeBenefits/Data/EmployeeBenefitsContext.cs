using Microsoft.EntityFrameworkCore;
using EmployeeBenefits.Models;
using System.Linq;

namespace EmployeeBenefits.Data
{
	public class EmployeeBenefitsContext : DbContext
	{
		public EmployeeBenefitsContext(DbContextOptions<EmployeeBenefitsContext> options) : base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Dependent> Dependents { get; set; }
		public DbSet<EmployeeDependent> EmployeeDependents { get; set; }

		/// <summary>
		/// Convenience method to automatically fetch all associated records
		/// </summary>
		public IQueryable<Employee> EmployeesWithDependents { 
			get {
				return Employees.Include(e => e.EmployeeDependents).ThenInclude(e => e.Dependent);
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