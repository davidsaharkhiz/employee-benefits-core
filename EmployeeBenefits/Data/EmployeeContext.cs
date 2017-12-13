using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

	}
}