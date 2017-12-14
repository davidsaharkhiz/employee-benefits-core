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
		/// Leverages DI so we don't have to implement this for each person
		/// </summary>
		/// <param name="person">Takes a person to compute a discount for based on that person's criteria and the logic of the discount delegate</param>
		/// <returns>the discount percentage for this person</returns>
		public int ComputeDiscountPercentageForAPerson(IPerson person) {

			return Discounts.Sum(d => d.DiscountCalculation(person));
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

		public decimal ComputeDiscountForAPerson(IPerson person) {
			return Decimal.Divide((person.BaseAnnualCostOfBenefits * (100 - person.BenefitsDiscountPercentage())), 100);
		}

	}
}
