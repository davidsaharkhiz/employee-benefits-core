using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeeBenefits.Helpers;

namespace EmployeeBenefits.Models
{

    public class Employee : IPerson
    {

		public int ID { get; set; }
		[StringLength(60, MinimumLength = 2)]
		[Required]
		public string Name { get; set; }
		private List<Discount> Discounts { get; set; } = new List<Discount>();

		// Many to many relationship here just to handle the edge-case of working couples sharing the same dependent
		public ICollection<EmployeeDependent> EmployeeDependents { get; } = new List<EmployeeDependent>();

		/// <summary>
		/// Parameterless constructor for Model Binding
		/// </summary>
		public Employee()
		{
			var testDiscount = new Discount();
		}

		/// <summary>
		/// Convenience constructor
		/// </summary>
		/// <param name="name">Name of the employee</param>
		public Employee(string name)
		{
			Name = name;
		}

		/// <summary>
		/// How much the employee is compensated each week in USD. This is gross compensations before any calculations are applied.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Compensation Per Paycheck")]
		[Required]
		public decimal CompensationPerPaycheck { get; set; } = 2000;

		/// <summary>
		/// Annual deduction from the employee's paycheck in USD to cover the cost of his or her benefits. Right now this leans on a hard-coded default given the simple requirements 
		/// but in a real environment this would undoubtedly change in the future....I've added it here in anticipation of eventual configuration of these values.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Annual Cost of Benefits")]
		[Required]
		public decimal BaseAnnualCostOfBenefits { get; set; } = 1000;

		#region Private Properties

		/// Benefits before any discount is added
		private decimal StandardAnnualBenefits()
		{
			return BaseAnnualCostOfBenefits + (500 * NumberOfDependents); //todo: replace this with a calculation from the dependents by looping over them
		}

		/// <summary>
		/// Computes a benefits deduction on a per-paycheck basis in USD
		/// </summary>
		private decimal BenefitsPerPaycheck
		{
			get
			{
				return AnnualBenefitsDiscount / Constants.WEEKS_PER_YEAR;
			}
		}

		/// <summary>
		/// Determine how much of a discount this person gets based on eligibility criteria
		/// </summary>
		/// <returns>The percentage discount availabile</returns>
		private int DetermineDiscountPercentage()
		{
			var totalDiscount = 0;
			foreach(var discount in Discounts) {
				if(discount.Active) {
					totalDiscount += discount.DiscountCalculation(this);
				}
			}
			return totalDiscount;
		}

		/// <summary>
		/// Some employees are eligible for a discount on the cost of their benefits.  This returns the discount available to this employee in USD.
		/// </summary>
		private decimal AnnualBenefitsDiscount
		{
			get
			{
				return Decimal.Divide((StandardAnnualBenefits() * (100 - BenefitsDiscountPercentage)), 100);
			}
		}

		#endregion

		#region Computed Properties

		/// <summary>
		/// Returns the number of Dependents
		/// </summary>
		[Display(Name = "Number of Dependents")]
		[Required]
		public int NumberOfDependents
		{
			get
			{
				return EmployeeDependents.Count;
			}
		}

		

		/// <summary>
		/// Some employees are eligible for a discount on the cost of their benefits. This returns the percentage available to this employee.
		/// </summary>
		public int BenefitsDiscountPercentage
		{
			get
			{
				
				return DetermineDiscountPercentage();
			}
		}

		
		/// <summary>
		/// Returns the cost of employee and dependent benefits for a pay period, formatted in USD
		/// </summary>
		public string BenefitsPerPaycheckFormatted
		{
			get { return CurrencyHelper.FormatCurrency(BenefitsPerPaycheck); }
		}
		

		/// <summary>
		/// This computes and returns the net cost to the employer for this employee on a per-paycheck basis. The cost is computed using the employee compensation, benefits, benefits discount, and his or her dependents.
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		public string NetCostPerPaycheckFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency(CompensationPerPaycheck - BenefitsPerPaycheck);
			}
		}

		/// <summary>
		/// This computes and returns the net cost to the employer for this employee on a per-paycheck basis. The cost is computed using the employee compensation, benefits, benefits discount, and his or her dependents.
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		public string NetCostFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency((CompensationPerPaycheck * Constants.WEEKS_PER_YEAR) - AnnualBenefitsDiscount);
			}
		}

		/// <summary>
		/// Some employees are eligible for a discount on the cost of their benefits.  This returns the discount available to this employee in USD.
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		public string AnnualBenefitsDiscountFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency(AnnualBenefitsDiscount);
			}
		}
		/// <summary>
		/// How much does this employee cost to the company per pay period
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		public string CompensationPerPaycheckFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency(CompensationPerPaycheck);
			}
		}

		#endregion


	}
}
