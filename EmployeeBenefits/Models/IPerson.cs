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
		List<Discount> Discounts { get; set; }
}

}
