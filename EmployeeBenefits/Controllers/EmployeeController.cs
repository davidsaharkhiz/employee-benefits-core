using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeBenefits.Controllers
{
	public class EmployeeController : Controller
	{
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
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Input(
			[Bind("Name, WeeklySalary")] Employee person)
		{
			if (ModelState.IsValid)
			{
				//todo:
				//Context.Add(movie);
				//await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(person);
		}


	}
}
