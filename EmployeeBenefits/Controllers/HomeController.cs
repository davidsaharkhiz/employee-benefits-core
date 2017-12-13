using Microsoft.AspNetCore.Mvc;
using EmployeeBenefits.Data;
using EmployeeBenefits.ViewModels;
using System.Linq;

namespace EmployeeBenefits.Controllers
{
    public class HomeController : Controller
    {

		//todo: consider moving this to a BaseController if you get time
		private readonly EmployeeBenefitsContext _context;

		// Inject our datacontext
		public HomeController(EmployeeBenefitsContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {

			ViewData["Title"] = "Home Page";

			var viewModel = new HomeIndexViewModel
			{
				Employees = _context.Employees
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
