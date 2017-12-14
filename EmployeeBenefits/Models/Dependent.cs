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
		private IDiscountHelper DiscountHelper { get; set; }

		/// <summary>
		/// Annual deduction from an employee's paycheck in USD to cover the cost of for his or her dependent benefits. Right now this leans on a hard-coded default given the simple requirements
		/// but in a real environment this would undoubtedly change in the future....I've added it here in anticipation of eventual configuration of these values.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Annual Cost of Benefits")]
		[Required]
		public decimal BaseAnnualCostOfBenefits { get; set; } = 500;

		/// <summary>
		/// Determine how much of a discount this person gets based on eligibility criteria
		/// </summary>
		/// <returns>The percentage discount availabile</returns>
		public int BenefitsDiscountPercentage()
		{
			return DiscountHelper.ComputeDiscountPercentage(this);
		}

		/// <summary>
		/// What are my benefit costs when adjusted for my discounts?
		/// </summary>
		public decimal AdjustedAnnualBenefits()
		{
			return DiscountHelper.ComputeAdjustedBenefits(this);
		}

		[Display(Name = "Associated Employee")]
		public ICollection<EmployeeDependent> EmployeeDependents { get; } = new List<EmployeeDependent>();

		public Dependent() {
			DiscountHelper = new DiscountHelper();
		}

		/// <summary>
		/// Our discount helper must be populated before all of our computed properties can return a value
		/// </summary>
		/// <param name="discounts">list of discounts available</param>
		public void ApplyDiscounts(IDiscountHelper discountHelper)
		{
			DiscountHelper = discountHelper;
		}

		

	}
}
