using ePizahub.UI.Models.Responses;
using ePizahub.UI.Models.ViewModels;
using ePizzahub.UI.Models;
using ePizzahub.UI.TokenHelpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ePizza.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public LoginController(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("ePizzaApiClient");

            var tokenResponse = await client.GetFromJsonAsync<ApiResponseModelDto<TokenResponseModelDto>>($"api/Token/getToken/{model.UserName}/{model.Password}");

            if (tokenResponse is not null && tokenResponse.IsSuccess)
            {
                _tokenService.SetToken(tokenResponse.Data.AccessToken);

                var claims = await ProcessToken(tokenResponse.Data.AccessToken);

                bool isAdmin = claims != null && Convert.ToBoolean(claims.FirstOrDefault(x => x.Type == "IsAdmin")?.Value);

                if (isAdmin)
                {
                     // 
                }
                else
                {
                    //
                }
            }


            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }


        private async Task<List<Claim>> ProcessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            var claims = new List<Claim>();
            foreach (var claim in jwtToken.Claims)
            {
                claims.Add(claim);
            }

            await GenerateTicket(claims);

            return claims;
        }

        private async Task GenerateTicket(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties()
                {
                    IsPersistent = false,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)

                });
        }
    }
}
