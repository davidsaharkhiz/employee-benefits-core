using System.Collections.Generic;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.Helpers
{
    public class DiscountHelper
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
			var totalDiscount = 0;
			foreach (var discount in Discounts)
			{
				if (discount.Active)
				{
					totalDiscount += discount.DiscountCalculation(person);
				}
			}
			return totalDiscount;
		}

	}
}
