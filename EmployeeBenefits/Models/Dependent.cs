using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeBenefits.Models
{

	/// <summary>
	/// Child, spouse, etc of an employee
	/// </summary>
	public class Dependent : IPerson
	{

		public int ID { get; set; }
		[StringLength(60, MinimumLength = 2)]

		[Required]
		public string Name { get; set; }

		[NotMapped]
		public List<Discount> Discounts { get; set; } = new List<Discount>();

		[Display(Name = "Associated Employee")]
		public ICollection<EmployeeDependent> EmployeeDependents { get; } = new List<EmployeeDependent>();

		public Dependent() {

		}

		/// <summary>
		/// Convenience constructor
		/// </summary>
		/// <param name="name">Name of the dependent</param>
		public Dependent(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Annual deduction from an employee's paycheck in USD to cover the cost of for his or her dependent benefits. Right now this leans on a hard-coded default given the simple requirements
		/// but in a real environment this would undoubtedly change in the future....I've added it here in anticipation of eventual configuration of these values.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Annual Cost of Benefits")]
		[Required]
		public decimal BaseAnnualCostOfBenefits { get; set; } = 1000;

	}
}
