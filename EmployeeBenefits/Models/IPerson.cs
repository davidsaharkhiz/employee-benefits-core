using System.Collections.Generic;
using EmployeeBenefits.Helpers;
namespace EmployeeBenefits.Models
{
	/// <summary>
	/// Representation of a 'person' in our system. This can be for example an employee, a spouse, or a dependent.
	/// </summary>
	public interface IPerson
	{
		int ID { get; set; }
		string Name { get; set; }
		decimal BaseAnnualCostOfBenefits { get; set; }

		/// <summary>
		/// Supply helper via IoC to drive property calculations at runtime
		/// </summary>
		void ApplyDiscounts(IDiscountHelper discounts);

		/// <summary>
		/// This method should compute the discount percentage for this person
		/// </summary>
		/// <returns>the discount percentage for this person</returns>
		int BenefitsDiscountPercentage();

		/// <summary>
		/// Calculates the adjusted cost of benefits based on a discount
		/// </summary>
		/// <returns></returns>
		decimal AdjustedAnnualBenefits();


	}

}
