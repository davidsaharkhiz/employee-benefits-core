using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeBenefits.Models
{
    public class Employee : IPerson
    {

		public int ID { get; set; }
		[StringLength(60, MinimumLength = 2)]
		[Required]
		public string Name { get; set; }

		[Range(1, 10000)]
		[DataType(DataType.Currency)]
		[Display(Name = "Weekly Salary")]
		public decimal WeeklySalary { get; set; } = 2000;

		[Display(Name = "Number of Dependents")]
		public int NumberOfDependents { get; set; } = 0;

		/* todo: remove me
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (text.Length < number)
			{
				yield return new ValidationResult(
					"Text too short, must be at least: " + number.ToString(),
					new[] { "number" });
			}
		}*/

	}
}
