using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeBenefits.Data;

namespace EmployeeBenefits.Controllers
{
	public class DependentController : Controller
	{
		//todo: consider moving this to a BaseController if you get time
		private readonly EmployeeBenefitsContext _context;

		// Inject our datacontext
		public DependentController(EmployeeBenefitsContext context)
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
		/// Renders a form to allow the user to fill out a form to input dependents for a supplied employee
		/// </summary>
		public IActionResult Input(int id)
		{
			ViewData["Title"] = "New Dependent Input";
			ViewData["EmployeeID"] = "id";
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Input(
			[Bind("Name")] Employee employee)
		{
			ViewData["Title"] = "New Dependent Input";
			if (ModelState.IsValid)
			{
				_context.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction("home", "index");
			}
			return View(employee);
		}


	}
}
