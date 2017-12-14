using EmployeeBenefits.Models;

namespace EmployeeBenefits.ViewModels
{
    public class HomeIndexViewModel
    {
		public System.Linq.IQueryable<Employee> Employees { get; set; }
		public string GrossEmployeeCompensation { get; set; }
		public int NumberOfEmployees { get; set; }
		public int NumberOfDependents { get; set; }
		public string TotalEmployeeBenefits { get; set; }
	}
}
