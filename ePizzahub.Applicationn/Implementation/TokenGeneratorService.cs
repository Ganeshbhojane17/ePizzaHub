using AutoMapper;
using ePizzahub.Applicationn.Contracts;
using ePizzahub.Applicationn.DTOs.Request;
using ePizzahub.Applicationn.DTOs.Response;
using ePizzahub.Applicationn.Exceptions;
using ePizzahub.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ePizzahub.Applicationn.Implementation
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IUserTokenService _userTokenService;
        private readonly IMapper _mapper;

        public TokenGeneratorService(IConfiguration configuration,
            IUserService userService,
            IUserTokenService userTokenService,
            IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _userTokenService = userTokenService;
            this._mapper = mapper;
        }

        public async Task<TokenResponseDto> GenerateRefreshToken(RefreshTokenRequestDto requestTokenDto)
        {

            // check if access token is valid
            var principal = GetTokenClaimPrincipal(requestTokenDto.AccessToken);
            if (principal == null) throw new Exception("Invalid Access Token");

            await ValidatePreviousTokenDetails(principal, requestTokenDto);

            var emailAddress = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;

            var userDetails = await _userService.GetUserDetailsAsync(emailAddress);
            return GenerateToken(userDetails);


        }

        public async Task<TokenResponseDto> GenerateTokenAsync(string userName, string password)
        {
            var userDetails = await _userService.GetUserDetailsAsync(userName);

            if (userDetails == null)
                throw new InvalidCredentialsExceptions($"The email address {userName} doesn't exists in database.");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, userDetails.Password);

            if (!isPasswordValid)
                throw new InvalidCredentialsExceptions($"The password {password} doesn't match with {userName}");


            var tokenResponseDto = GenerateToken(userDetails);

            if (tokenResponseDto is not null)
            {
                await _userTokenService.PersistToken(new UserTokenRequestDto()
                {
                    AccessToken = tokenResponseDto.AccessToken,
                    RefreshToken = tokenResponseDto.RefreshToken,
                    UserId = userDetails.Id
                });

                return tokenResponseDto;
            }

            return new TokenResponseDto();
        }


        private TokenResponseDto GenerateToken(UserDomain userDomain)
        {
            string secretKey = _configuration["Jwt:Secret"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor =
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([

                         new Claim(ClaimTypes.Name,userDomain.Name),
                         new Claim(ClaimTypes.Email,userDomain.Email),
                         new Claim("IsAdmin","True"),
                         new Claim("UserId",userDomain.Id.ToString()),

                        ]),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:TokenExpiryInMinutes"])),
                    SigningCredentials = credentials,
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"]
                };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponseDto()
            {
                AccessToken = token,
                RefreshToken = GenerateRefreshToken()
            };
        }

        private ClaimsPrincipal? GetTokenClaimPrincipal(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
            var validationParameter = GetTokenValidationParameters(key);

            return tokenHandler.ValidateToken(accessToken, validationParameter, out _);
        }

        private TokenValidationParameters? GetTokenValidationParameters(byte[] key)
        {

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"]!,
                ValidAudience = _configuration["Jwt:Audience"]!,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        }

        private string GenerateRefreshToken()
        {
            //new jwt token

            var randomBytes = new byte[64];

            using var range = RandomNumberGenerator.Create();
            range.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        private async Task ValidatePreviousTokenDetails(
           ClaimsPrincipal principal,
           RefreshTokenRequestDto requestDto)
        {
            var previousTokenDetails =
                await FetchPreviousTokenDetails(principal);

            if (previousTokenDetails == null
                || previousTokenDetails.RefreshToken != requestDto.RefreshToken
                 || previousTokenDetails.AccessToken != requestDto.AccessToken
                || previousTokenDetails.RefreshTokenExpiryTime < DateTime.UtcNow)
                throw new UnAuthorizedException("Invalid Refresh Token Token");
        }

        private async Task<RefreshTokenResponseDto> FetchPreviousTokenDetails(
            ClaimsPrincipal claimsPrincipal)
        {
            var userId
                = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value;

            var userTokenDomain = await _userService.GetUserTokenAsync(Convert.ToInt32(userId));
            return _mapper.Map<RefreshTokenResponseDto>(userTokenDomain);
        }
    }
}

