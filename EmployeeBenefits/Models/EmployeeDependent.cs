namespace EmployeeBenefits.Models
{
    public class EmployeeDependent
    {
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }

		public int DependentId { get; set; }
		public Dependent Dependent { get; set; }
	}
}
