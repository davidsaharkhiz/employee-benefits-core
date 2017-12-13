using System;
using System.Collections.Generic;
using EmployeeBenefits.Models;

namespace EmployeeBenefits.ViewModels
{
    public class HomeIndexViewModel
    {
		public System.Linq.IQueryable<Employee> Employees { get; set; }
    }
}
