using Microsoft.AspNetCore.Mvc;
using EmployeeBenefits.Data;
using EmployeeBenefits.ViewModels;
using System.Linq;

namespace EmployeeBenefits.Controllers
{
    public class HomeController : BaseController
    {

		public HomeController(EmployeeBenefitsContext context) : base(context) {
			_context = context;
		}

		public IActionResult Index()
        {
			@ViewData["Title"] = "Home Page";
			var employees = _context.EmployeesWithAllData;
			var dependents = _context.Dependents;
			var viewModel = new HomeIndexViewModel
			{
				Employees = employees,
				NumberOfEmployees = employees.Count(),
				NumberOfDependents = dependents.Count(),
				GrossEmployeeCompensation = Helpers.CurrencyHelper.FormatCurrency(employees.Sum(e => e.CompensationPerPaycheck * Helpers.Constants.WEEKS_PER_YEAR)),
				TotalEmployeeBenefits = Helpers.CurrencyHelper.FormatCurrency(employees.Sum(e => e.AdjustedAnnualBenefits()))
			};
			return View(viewModel);
        }

        public IActionResult Error()
        {
			ViewData["Title"] = "Error Page";
			return View();
        }
    }
}
