using ePizzahub.UI.Models;
using ePizzahub.UI.Models.Requests;
using ePizzahub.UI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ePizzahub.UI.Controllers
{
    [Route("Cart")]
    public class CartController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CartController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        Guid CartId
        {
            get
            {
                Guid id;
                string? cartId = Request.Cookies["CartId"];
                if(cartId == null)
                {
                    id = Guid.NewGuid();
                    Response.Cookies.Append("CartId", id.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(1),
                        Secure = true
                    });
                }
                else
                {
                    id = Guid.Parse(cartId);
                }

               return id;
            }
        }

        public async Task<IActionResult> Index()
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizzaApiClient");

            var cartItems =
                await httpClient.GetFromJsonAsync<ApiResponseModelDto<CartItemsResponseModelDto>>(
                $"api/Cart/get-cart-detail?cartId={CartId}");

            return View(cartItems?.Data);
        }

        [HttpGet("AddToCart/{itemId:int}/{unitPrice:decimal}/{quantity:int}")]
        public async Task<JsonResult> AddItemToCart(int itemId,decimal unitPrice, int quantity)
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizzaApiClient");

            var itemResponse
                = await httpClient.PostAsJsonAsync(
                    "api/Cart/add-items", GetCartRequest(itemId, unitPrice, quantity));
            itemResponse.EnsureSuccessStatusCode();

            var itemCount = await GetItemCount(CartId);

            return Json(new { Count = itemCount });
        }


        [NonAction]
        private async Task<int> GetItemCount(Guid cartId)
        {
            using var httpClient = _httpClientFactory.CreateClient("ePizzaApiClient");

            var itemCount =
                await httpClient.GetFromJsonAsync<ApiResponseModelDto<int>>(
                    $"api/Cart/get-item-count?cartId={CartId}");

            if (itemCount != null) return itemCount.Data;

            return await Task.FromResult(0);
        }

        [NonAction]
        private AddToCartRequestDto GetCartRequest(
          int itemId, decimal unitPrice, int quantity)
        {
            return new AddToCartRequestDto
            {
                CartId = CartId,
                Quantity = quantity,
                ItemId = itemId,
                UnitPrice = unitPrice,
            };
        }
    }
}
