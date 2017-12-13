using System.ComponentModel.DataAnnotations;

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
		/// Number of dependents associated with a given employee. This affects deductions. I'm punting development against dependents as a table and model because there is not yet a valid use case for them.
		/// If one arises, IPerson would allow such an implementation pretty easily and we could have a m2m relationship here.
		/// </summary>
		[Display(Name = "Number of Dependents")]
		public uint NumberOfDependents { get; set; } = 0;

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
		/// Some employees are eligible for a discount on the cost of their benefits.  This returns the discount available to this employee in USD.
		/// </summary>
		public decimal AnnualBenefitsDiscount { get { 
			return 1;
		} }

		/// <summary>
		/// This computes and returns the net cost to the employer for this employee on a per-paycheck basis. The cost is computed using the employee compensation, benefits, benefits discount, and his or her dependents.
		/// </summary>
		public decimal NetCostPerPaycheck
		{
			get
			{
				return 1;
			}
		}


	}
}
