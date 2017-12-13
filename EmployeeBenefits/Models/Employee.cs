using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefits.Models
{

    public class Employee : IPerson
    {

		// This is not likely to change for a few billion years, so I think a constant is acceptable #futureproof 
		private const int WEEKS_PER_YEAR = 26;
		// Typically these would be configurable by the business but going 100% data-driven for a sample app seems like a waste of time.
		private const decimal STANDARD_ANNUAL_BENEFITS_USD = 1000;
		private const decimal STANDARD_ANNUAL_BENEFITS_DEPENDENT_USD = 500;

		public int ID { get; set; }
		[StringLength(60, MinimumLength = 2)]
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// How much the employee is compensated each week in USD. This is gross compensations before any calculations are applied.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Compensation Per Paycheck")]
		[Required]
		public decimal CompensationPerPaycheck { get; set; } = 2000;

		/// <summary>
		/// Annual deduction from the employee's paycheck in USD to cover the cost of his or her benefits. Right now this is hard coded due to the requirements
		/// but in a real environment this would undoubtedly change in the future....I've added it here in anticipation of eventual configuration of these values.
		/// </summary>
		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Annual Cost of Benefits")]
		[Required]
		public decimal BaseAnnualCostOfBenefits { get; set; } = 1000;

		/// <summary>
		/// Number of dependents associated with a given employee. This affects deductions. I'm punting development against dependents as a table and model because there is not yet a valid use case for them.
		/// If one arises, IPerson would allow such an implementation pretty easily and we could have a m2m relationship here.
		/// </summary>
		[Display(Name = "Number of Dependents")]
		[Required]
		public int NumberOfDependents { get; set; } = 0;

		private decimal StandardAnnualBenefits() {
			return STANDARD_ANNUAL_BENEFITS_USD + (STANDARD_ANNUAL_BENEFITS_DEPENDENT_USD * NumberOfDependents);
		}

		/// <summary>
		/// Some employees are eligible for a discount on the cost of their benefits. This returns the percentage available to this employee.
		/// </summary>
		public int BenefitsDiscountPercentage
		{
			get
			{
				return 1;
			}
		}

		/// <summary>
		/// Computes a benefits deduction on a per-paycheck basis in USD
		/// </summary>
		public decimal BenefitsPerPaycheck
		{
			get
			{
				return AnnualBenefitsDiscount / WEEKS_PER_YEAR;
			}
		}

		/// <summary>
		/// Some employees are eligible for a discount on the cost of their benefits.  This returns the discount available to this employee in USD.
		/// </summary>
		public decimal AnnualBenefitsDiscount { 
			get {
				return BenefitsDiscountPercentage * StandardAnnualBenefits();
			} 
		}

		/// <summary>
		/// This computes and returns the net cost to the employer for this employee on a per-paycheck basis. The cost is computed using the employee compensation, benefits, benefits discount, and his or her dependents.
		/// </summary>
		public decimal NetCostPerPaycheck
		{
			get
			{
				return CompensationPerPaycheck - BenefitsPerPaycheck;
			}
		}

		/// <summary>
		/// Parameterless constructor for Model Binding
		/// </summary>
		public Employee() {

		}

		/// <summary>
		/// All other params are auto-populated
		/// </summary>
		/// <param name="name"></param>
		public Employee(string name, int numberOfDependents) {
			Name = name;
			NumberOfDependents = numberOfDependents;
		}


	}
}
