using EmployeeBenefits.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBenefits.Controllers
{
	public class BaseController : Controller
    {
		protected  EmployeeBenefitsContext _context;

		// Inject our datacontext
		public BaseController(EmployeeBenefitsContext context)
		{
			_context = context;
		}
	}
}
