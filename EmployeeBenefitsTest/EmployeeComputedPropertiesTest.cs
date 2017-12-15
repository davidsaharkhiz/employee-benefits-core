using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeBenefits.Models;
using EmployeeBenefits.Helpers;
using System;

namespace EmployeeBenefitsTest
{
    [TestClass]
    public class EmployeeComputedPropertiesTest
    {
        [TestMethod]
        public void TestNetCost()
        {

			var employee = new Employee
			{
				Name = "Andre",
				CompensationPerPaycheck = 2000,
				BaseAnnualCostOfBenefits = 1000,
				ID = 1
			};

			Assert.AreEqual(employee.NetCost, ((employee.CompensationPerPaycheck * Constants.WEEKS_PER_YEAR) - employee.BaseAnnualCostOfBenefits));
		}
    }
}
