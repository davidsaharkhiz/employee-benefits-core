using EmployeeBenefits.Models;
using System.Collections.Generic;

namespace EmployeeBenefits.Helpers
{
	public interface IDiscountHelper : IHelper
	{
		List<Discount> Discounts { get; set; }

		decimal ComputeDiscountForAPerson(IPerson person);
		int ComputeDiscountPercentageForAPerson(IPerson employee);
		//todo: make this generic too!
		List<string> BenefitsDiscountSummaryForDependents(List<Dependent> proccessedDependents);
	}
}
