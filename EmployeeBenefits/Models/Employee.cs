using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Models
{
    public class Employee : IPerson
    {

		public int ID { get; set; }
		[StringLength(60, MinimumLength = 2)]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// How much the employee is compensated each week in USD. This is gross compensations before any calculations are applied.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Weekly Salary")]
		public decimal WeeklySalary { get; set; } = 2000;

		/// <summary>
		/// Annual deduction from the employee's paycheck in USD to cover the cost of his or her benefits. Right now this is hard coded due to the requirements
		/// but in a real environment this would undoubtedly change in the future....I've added it here in anticipation of eventual configuration of these values.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Annual Cost of Benefits")]
		public decimal BaseAnnualCostOfBenefits { get; set; } = 1000;

		/// <summary>
		/// Number of dependents associated with a given employee. This affects deductions.
		/// </summary>
		[Display(Name = "Number of Dependents")]
		public uint NumberOfDependents { get; set; } = 0;

	

	}
}
