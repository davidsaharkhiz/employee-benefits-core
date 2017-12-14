using System;
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
	}
}
