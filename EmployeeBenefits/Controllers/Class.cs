using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

		
	}
}
