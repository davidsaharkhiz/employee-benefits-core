using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeBenefits.Data;
using EmployeeBenefits.ViewModels;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

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

		// Prepares a viewmodel with the select list and other data we need to render our page 
		private DependentInputViewModel PrepareViewModel(uint id = 0) {
			var dropDownList = new SelectList(_context.Employees, "ID", "Name");
			var message = "Please select one or more employees to associate with this dependent.";
			if(id > 0) {
				message = "Your employee has been successfully entered. Now enter any associated employees or press 'Skip' to continue.";
				var matchingRecord = dropDownList.First(d => d.Value == id.ToString());
				if (matchingRecord != null)
				{
					matchingRecord.Selected = true;
				}
			}
			return new DependentInputViewModel
			{
				SelectList = dropDownList,
				UserMessage = message
			};
		}

		/// <summary>
		/// Renders a form to allow the user to fill out a form to input dependents for a supplied employee
		/// As part of a future requirement we could implement full CRUD for dependents with multi-select, but for now I'm just creating them at the point of employee creation in the interest of time.
		/// </summary>
		public IActionResult Input(uint id)
		{
			ViewData["Title"] = "New Dependent Input";
			
			if(!_context.Employees.Any(e => e.ID == id)) {
				throw new ArgumentException($"Employee with {nameof(id)} {id} does not exist.");
				//todo: logger here
			}
			return View(PrepareViewModel(id));

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Input(DependentInputViewModel dependentViewModel)
		{
			ViewData["Title"] = "New Dependent Input";
			if (ModelState.IsValid)
			{
				_context.Add(dependentViewModel);
				await _context.SaveChangesAsync();
				return RedirectToAction("index", "home");
			}
			return View(PrepareViewModel());
		}


	}
}
