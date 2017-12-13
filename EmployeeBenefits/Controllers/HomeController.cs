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
			var viewModel = new HomeIndexViewModel();

			viewModel.Employees = _context.Employees;

			return View(viewModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
