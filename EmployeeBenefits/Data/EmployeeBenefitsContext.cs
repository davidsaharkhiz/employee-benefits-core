using Microsoft.EntityFrameworkCore;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.Data
{
	public class EmployeeBenefitsContext : DbContext
	{
		public EmployeeBenefitsContext(DbContextOptions<EmployeeBenefitsContext> options) : base(options)
		{
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Dependent> Dependents { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Define composite key for our join table
			modelBuilder.Entity<EmployeeDependent>()
				.HasKey(t => new { t.EmployeeId, t.DependentId });
		}

	}
}