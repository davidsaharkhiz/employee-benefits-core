using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeBenefits.Data;

namespace EmployeeBenefits.Controllers
{
	public class EmployeeController : BaseController
	{
		public EmployeeController(EmployeeBenefitsContext context) : base(context) {
			_context = context;
		}

		/// <summary>
		/// Home Page for employee controller
		/// </summary>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// Renders a form to allow the user to fill out a form to input an employee
		/// </summary>
		public IActionResult Input()
		{
			ViewData["Title"] = "New Employee Input";
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Input(
			[Bind("Name")] Employee employee)
		{
			ViewData["Title"] = "New Employee Input";
			if (ModelState.IsValid)
			{
				_context.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction("input", "dependent", new { employee.ID });
			}
			return View(employee);
		}


	}
}
