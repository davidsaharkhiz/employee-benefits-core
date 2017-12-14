using EmployeeBenefits.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmployeeBenefits.ViewModels
{
    public class DependentInputViewModel
    {
		public IQueryable<Employee> Employees { get; set; }
		public SelectList SelectList { get; set; }
		public string UserMessage { get; set; }
		public uint SelectedEmployeeId { get; set; }
		public Dependent Dependent { get; set; } = new Dependent();
	}
}
