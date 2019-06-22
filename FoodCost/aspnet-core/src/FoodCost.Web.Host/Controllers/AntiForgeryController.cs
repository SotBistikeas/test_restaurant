using Microsoft.AspNetCore.Antiforgery;
using FoodCost.Controllers;

namespace FoodCost.Web.Host.Controllers
{
    public class AntiForgeryController : FoodCostControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
