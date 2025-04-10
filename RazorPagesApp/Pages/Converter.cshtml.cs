using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages
{
    public class ConverterModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public decimal Azn { get; set; } = 30;

        [BindProperty(SupportsGet = true)]
        public string ToCurrency { get; set; } = "USD";

        public decimal Result { get; set; }

        public void OnGet(string toCurrency = "USD")
        {
            switch (ToCurrency?.ToUpperInvariant())
            {
                case "USD":
                    Result = Azn / 1.7m;
                    break;
                case "EUR":
                    Result = Azn / 2m;
                    break;
                case "GBP":
                    Result = Azn / 4m;
                    break;
                default:
                    Result = 0;
                    break;
            }
        }
    }
}
