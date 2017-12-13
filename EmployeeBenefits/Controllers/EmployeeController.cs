using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeBenefits.Data;
using System.Linq;

namespace EmployeeBenefits.Controllers
{
	public class EmployeeController : Controller
	{
		//todo: consider moving this to a BaseController if you get time
		private readonly EmployeeBenefitsContext _context;

		// Inject our datacontext
		public EmployeeController(EmployeeBenefitsContext context)
		{
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
		/// Renders a form to allow the user to fill out a form to input employees and dependents
		/// </summary>
		public IActionResult Input()
		{
			ViewData["Message"] = $"Input New Employees and Dependents: " + _context.Employees.Count(); //todo: remove

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Input(
			[Bind("Name, WeeklySalary")] Employee employee)
		{
			if (ModelState.IsValid)
			{
				_context.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction("index", "home");
			}
			return View(employee);
		}


	}
}
