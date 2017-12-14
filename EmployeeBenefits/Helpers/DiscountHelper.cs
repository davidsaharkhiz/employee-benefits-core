using System.Collections.Generic;
using EmployeeBenefits.Models;
using System;
using System.Linq;

namespace EmployeeBenefits.Helpers
{
    public class DiscountHelper : IDiscountHelper
    {
		public List<Discount> Discounts { get; set; } = new List<Discount>();

		public DiscountHelper()
		{
		}

		public DiscountHelper(List<Discount> discounts) {
			Discounts = discounts;
		}

		/// <summary>
		/// Compute all discounts against a given person. NOTE: Discounts are additive not multiplicative so a 5% discount and a 10% discount results in a 15% discount not a 14.5% discount
		/// </summary>
		/// <param name="person">Takes a person to compute a discount for based on that person's criteria and the logic of the discount delegate</param>
		/// <returns>the sum of the discount percentage for this person</returns>
		public int ComputeDiscountPercentage(IPerson person) {
			return Discounts.Where(d => d.Active).Sum(d => d.DiscountCalculation(person));
		}

		/// <summary>
		/// Computes a human-readable summary of discounts for each supplied person (i.e. dependent)
		/// </summary>
		/// <returns>Returns a human-readable summary of discounts for each dependent</returns>
		public ICollection<string> BenefitsDiscountSummary(IPerson person)
		{
			List<string> summaries = new List<string>();
			foreach (var discount in Discounts)
			{
				if (discount.Active && discount.DiscountCalculation(person) > 0)
				{
					summaries.Add($"{person.Name} received {discount.Amount}% Discount");
				}
			}
			return summaries;
		}

		/// <summary>
		/// Compute the Adjusted Benefits based on a person's annual cost of benefits and the discount percentage they recieve
		/// </summary>
		/// <param name="person">Supply a person to check</param>
		/// <returns>a decimal representing the annual cost of benefits</returns>
		public decimal ComputeAdjustedBenefits(IPerson person) {
			return Decimal.Divide((person.BaseAnnualCostOfBenefits * (100 - person.BenefitsDiscountPercentage())), 100);
		}

	}
}
