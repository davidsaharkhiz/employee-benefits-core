using EmployeeBenefits.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeBenefits.Data;
using EmployeeBenefits.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeBenefits.Controllers
{
	public class DependentController : BaseController
	{
		
		public DependentController(EmployeeBenefitsContext context) : base(context) {
			_context = context;
		}

		// Prepares a viewmodel with the select list and other data we need to render our page, attempt to automatically select the matching employee
		private DependentInputViewModel PrepareViewModel(uint id = 0) {
			var dropDownList = new SelectList(_context.Employees, "ID", "Name");
			var message = "Please select one or more employees to associate with this dependent.";
			if(id > 0) {
				message = "Your employee has been successfully entered. Now enter any associated employees or press 'Skip' to continue.";
				var matchingRecord = dropDownList.FirstOrDefault(d => d.Value == id.ToString());
				if (matchingRecord != null)
				{
					matchingRecord.Selected = true;
				}
			}
			return new DependentInputViewModel
			{
				SelectList = dropDownList,
				UserMessage = message,
				SelectedEmployeeId = id
			};
		}

		/// <summary>
		/// Renders a form to allow the user to fill out a form to input dependents for a supplied employee
		/// As part of a future requirement we could implement full CRUD for dependents with multi-select, but for now I'm just creating them at the point of employee creation in the interest of time.
		/// </summary>
		public IActionResult Input(uint id = 0)
		{
			ViewData["Title"] = "New Dependent Input";
			return View(PrepareViewModel(id));
		}
		
		/// <summary>
		/// Processes a form submitted by the user for a new dependent and employee association
		/// </summary>
		/// <param name="dependentViewModel">A viewmodel for a filled out form</param>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Input(DependentInputViewModel dependentViewModel)
		{
			ViewData["Title"] = "New Dependent Input";

			if(!_context.Employees.Any(e => e.ID == dependentViewModel.SelectedEmployeeId)) {
				ModelState.AddModelError("SelectedEmployeeId", "No such employee found. Input has been spoofed or data is corrupt.");
			}

			if (ModelState.IsValid)
			{

				var newDependent = _context.Add(new Dependent
				{
					Name = dependentViewModel.Dependent.Name
				});

				_context.SaveChanges();

				_context.Add(new EmployeeDependent { 
					Employee = _context.Employees.First(e => e.ID == dependentViewModel.SelectedEmployeeId),
					Dependent = newDependent.Entity
				});

				_context.SaveChanges();

				return RedirectToAction("index", "home");
			}
			return View(PrepareViewModel());
		}


	}
}
