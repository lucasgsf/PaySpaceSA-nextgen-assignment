using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Borders.Models;

namespace PaySpace.Calculator.Web.Controllers
{
    [Route("/[Controller]")]
    public class CalculatorController(ICalculatorService calculatorService, IPostalCodeService postalCodeService) : Controller
    {
        [HttpGet, Route("history")]
        public async Task<IActionResult> History()
        {
            return this.View(new CalculatorHistoryViewModel
            {
                CalculatorHistory = await calculatorService.GetHistoryAsync()
            });
        }

        [HttpGet, Route("calculate")]
        public async Task<IActionResult> Calculate()
        {
            var vm = await this.GetCalculatorViewModelAsync();
            return this.View(vm);
        }

        [HttpPost, Route("calculate")]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Calculate(CalculateRequestViewModel request)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    await calculatorService.CalculateTaxAsync(new CalculateRequest
                    {
                        PostalCode = request.PostalCode,
                        Income = request.Income
                    });

                    return this.RedirectToAction(nameof(this.History));
                }
                catch (Exception e)
                {
                    this.ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            
            var vm = await this.GetCalculatorViewModelAsync();
            return this.View(vm);
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        {
            var lstPostalCodes = await postalCodeService.GetPostalCodesAsync();

            return new CalculatorViewModel
            {
                PostalCodes = new SelectList(lstPostalCodes, nameof(PostalCode.Code), nameof(PostalCode.Code)),
                Income = request?.Income ?? 0,
                PostalCode = request?.PostalCode ?? string.Empty
            };
        }
    }
}