using EmployeeBenefits.Models;
using System.Collections.Generic;

namespace EmployeeBenefits.Helpers
{
	public interface IDiscountHelper : IHelper
	{
		List<Discount> Discounts { get; set; }

		decimal ComputeAdjustedBenefits(IPerson person);
		int ComputeDiscountPercentage(IPerson person);
		
		/// <summary>
		/// Should return a human readable collection detailing the discounts for this entity
		/// </summary>
		/// <param name="person"></param>
		/// <returns>A collection detailing the discounts for this entity</returns>
		ICollection<string> BenefitsDiscountSummary(IPerson person);
	}
}
