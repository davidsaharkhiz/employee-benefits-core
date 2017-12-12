﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeBenefits.Models
{

	/// <summary>
	/// Representation of a 'person' in our system. This can be for example an employee, a spouse, or a dependent. I am avoiding looping in identity because this is just a simple test app.
	/// </summary>
	public interface IPerson
	{

		int ID { get; set; }
		string Name { get; set; }


	}
}
