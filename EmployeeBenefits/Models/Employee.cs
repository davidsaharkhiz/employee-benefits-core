using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeeBenefits.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EmployeeBenefits.Models
{

    public class Employee : IPerson
    {

		public int ID { get; set; }

		[StringLength(60, MinimumLength = 2)]
		[Required]
		public string Name { get; set; }

		// Many to many relationship here just to handle the edge-case of working couples sharing the same dependent
		public ICollection<EmployeeDependent> EmployeeDependents { get; } = new List<EmployeeDependent>();

		[NotMapped]
		private IDiscountHelper DiscountHelper { get; set; }

		[NotMapped]
		public List<Dependent> ProccessedDependents = new List<Dependent>();
		
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

		/// <summary>
		/// Parameterless constructor for Model Binding
		/// </summary>
		public Employee()
		{
			DiscountHelper = new DiscountHelper();
		}

		public string BaseCostOfBenefitsFormatted() {
			return CurrencyHelper.FormatCurrency(BaseAnnualCostOfBenefits / Constants.WEEKS_PER_YEAR);
		}

		/// <summary>
		/// Gross total of standard dependent benefits, formatted in USD
		/// </summary>
		public string BaseCostOfDependentBenefitsFormatted()
		{
			return CurrencyHelper.FormatCurrency(ProccessedDependents.Sum(d => d.BaseAnnualCostOfBenefits) / Constants.WEEKS_PER_YEAR);
		}

		/// <summary>
		/// Includes dependents
		/// </summary>
		public decimal AdjustedAnnualBenefits()
		{
			var total = DiscountHelper.ComputeAdjustedBenefits(this);
			return total + ProccessedDependents.Sum(d => d.AdjustedAnnualBenefits());
		}

		/// <summary>
		/// We need to supply our DiscountHelper with discounts from the database before all computed properties will work
		/// </summary>
		/// <param name="discounts"></param>
		public void ApplyDiscounts(IDiscountHelper discountHelper) {
			DiscountHelper = discountHelper;
			var dependents = EmployeeDependents.Select(d => d.Dependent).ToList();
			foreach (var dependent in dependents)
			{
				dependent.ApplyDiscounts(discountHelper);
				ProccessedDependents.Add(dependent);
			}
		}

		/// <summary>
		/// Determine how much of a discount this person gets based on eligibility criteria
		/// </summary>
		/// <returns>The percentage discount availabile</returns>
		public int BenefitsDiscountPercentage()
		{
			return DiscountHelper.ComputeDiscountPercentage(this);
		}

		/// <summary>
		/// Computes a human-readable summary of discounts for each dependent
		/// </summary>
		/// <returns>Returns a human-readable summary of discounts for each dependent</returns>
		public List<string> BenefitsDiscountSummary() {

			var summaries = new List<string>();
			foreach (var person in ProccessedDependents)
			{
				summaries.AddRange(DiscountHelper.BenefitsDiscountSummary(person));
			}
			return summaries;
		}

		#region Computed Properties (I normally avoid these when working with EF because of obvious pitfalls, but they were convenient for this little demo.)

		[NotMapped]
		/// <summary>
		/// Computes a benefits deduction on a per-paycheck basis in USD
		/// </summary>
		public decimal BenefitsPerPaycheck
		{
			get
			{
				return AdjustedAnnualBenefits() / Constants.WEEKS_PER_YEAR;
			}
		}

		/// <summary>
		/// Returns the number of Dependents
		/// </summary>
		[Display(Name = "Number of Dependents")]
		[NotMapped]
		public int NumberOfDependents
		{
			get
			{
				return EmployeeDependents.Count;
			}
		}

		/// <summary>
		/// Returns the cost of employee and dependent benefits for a pay period, formatted in USD
		/// </summary>
		[NotMapped]
		public string BenefitsPerPaycheckFormatted
		{
			get { return CurrencyHelper.FormatCurrency(BenefitsPerPaycheck); }
		}


		/// <summary>
		/// This computes and returns the net cost to the employer for this employee on a per-paycheck basis. The cost is computed using the employee compensation, benefits, benefits discount, and his or her dependents.
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		[NotMapped]
		public string NetCostPerPaycheckFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency(CompensationPerPaycheck - BenefitsPerPaycheck);
			}
		}

		/// <summary>
		/// This computes and returns the net cost to the employer for this employee on an annual basis. The cost is computed using the employee compensation, benefits, benefits discount, and his or her dependents.
		/// </summary>
		public decimal NetCost {
			get {
				return (CompensationPerPaycheck * Constants.WEEKS_PER_YEAR) - AdjustedAnnualBenefits();
			}
		}

		[NotMapped]
		public string NetCostFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency(NetCost);
			}
		}

		/// <summary>
		/// Some employees are eligible for a discount on the cost of their benefits.  This returns the discount available to this employee in USD.
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		[NotMapped]
		public string AnnualBenefitsDiscountFormatted
		{
			get
			{
				return CurrencyHelper.FormatCurrency(AdjustedAnnualBenefits());
			}
		}
		/// <summary>
		/// How much does this employee cost to the company per pay period
		/// </summary>
		/// <returns>A formatted string in USD</returns>
		[NotMapped] 
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
