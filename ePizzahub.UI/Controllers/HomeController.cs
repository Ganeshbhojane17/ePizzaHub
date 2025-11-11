using ePizzahub.UI.Models;
using ePizzahub.UI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ePizzahub.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ePizzaApiClient");

            var items = await client.GetFromJsonAsync<ApiResponseModelDto<IEnumerable<ItemResponseDto>>>("api/Item/GetAllItems");

            if (items is not null && items.IsSuccess)
            {
                return View(items.Data);
            }

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
