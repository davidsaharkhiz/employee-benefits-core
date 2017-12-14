using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeBenefits.Models
{
    public class Discount
    {

		/// <summary>
		/// Constant names for discounts
		/// </summary>
		[NotMapped]
		public static class Names {
			public const string BEGINS_WITH_LETTER_CASE_INSENSITIVE = "BEGINS_WITH_LETTER";
		}

		[Required]
		public int ID { get; set; }

		/// <summary>
		/// Name of the discount. Since we switch on this it needs to be unique.
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Amount of the discount, expressed as a percentage
		/// </summary>
		[Required]
		public int Amount { get; set; }

		/// <summary>
		/// Since a delegate function is being applied, if there needs to be some sort of value to check supply it here.
		/// </summary>
		public string OptionalOperand { get; set; }

		/// <summary>
		/// Delegate to define the logic to apply the discount. This cannot be stored in the database.
		/// </summary>
		[NotMapped] 
		public Func<IPerson, int> DiscountCalculation { get; set; }

		/// <summary>
		/// Is the discount currently available or has it been disabled?
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		/// I was looking into implementing expression trees but ran out of time. This is less than ideal!!!
		/// I really hate this because it violates the open-closed principle but I needed more time to to work around it.
		/// </summary>
		public static List<Discount> MapDelegatesToDiscounts(DbSet<Discount> discountsFromDbSet) {
			var validDiscounts = new List<Discount>();
			foreach (var discount in discountsFromDbSet)
			{
				switch (discount.Name)
				{
					case Names.BEGINS_WITH_LETTER_CASE_INSENSITIVE:
						if (string.IsNullOrEmpty(discount.OptionalOperand))
						{
							throw new ArgumentException("Operand was not supplied but was necessary for this discount!");
						}
						discount.DiscountCalculation = delegate (IPerson person)
						{
							return person.Name.ToLower().StartsWith(discount.OptionalOperand.ToLower()) ? discount.Amount : 0;
						};
						break;
					default:
						continue;
				}
				validDiscounts.Add(discount);
			}
			return validDiscounts;
		}

	}
}
