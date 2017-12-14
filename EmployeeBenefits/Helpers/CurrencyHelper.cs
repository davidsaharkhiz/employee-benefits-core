namespace EmployeeBenefits.Helpers
{
    public class CurrencyHelper
    {

		private const string CURRENCY_FORMAT = "{0:C}";

		public static string FormatCurrency(decimal amount) {
			return string.Format(CURRENCY_FORMAT, amount);
		}

    }
}
