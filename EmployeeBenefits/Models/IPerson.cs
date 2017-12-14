using EmployeeBenefits.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeBenefits.Models
{
	/// <summary>
	/// Representation of a 'person' in our system. This can be for example an employee, a spouse, or a dependent.
	/// </summary>
	public interface IPerson
	{
		int ID { get; set; }
		string Name { get; set; }

		[NotMapped]
		DiscountHelper DiscountHelper { get; set; }

		/// <summary>
		/// This method should compute the discount percentage for this person
		/// </summary>
		/// <returns>the discount percentage for this person</returns>
		int BenefitsDiscountPercentage();

	}

}
