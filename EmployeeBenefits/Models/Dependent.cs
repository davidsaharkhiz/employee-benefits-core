using EmployeeBenefits.Helpers;
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
		private DiscountHelper DiscountHelper { get; set; }

		/// <summary>
		/// Determine how much of a discount this person gets based on eligibility criteria
		/// </summary>
		/// <returns>The percentage discount availabile</returns>
		public int BenefitsDiscountPercentage()
		{
			return DiscountHelper.ComputeDiscountPercentageForAPerson(this);
		}

		[Display(Name = "Associated Employee")]
		public ICollection<EmployeeDependent> EmployeeDependents { get; } = new List<EmployeeDependent>();

		public Dependent() {
			DiscountHelper = new DiscountHelper();
		}

		/// <summary>
		/// Convenience constructor
		/// </summary>
		/// <param name="name">Name of the dependent</param>
		public Dependent(string name)
		{
			Name = name;
		}

		public void ApplyDiscounts(List<Discount> discounts)
		{
			DiscountHelper.Discounts = discounts;
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
