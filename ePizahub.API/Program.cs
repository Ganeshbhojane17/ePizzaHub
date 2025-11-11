
using ePizahub.API.Middleware;
using ePizza.API.Middleware;
using ePizzahub.Applicationn;
using ePizzahub.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"]!,
            ValidAudience = builder.Configuration["Jwt:Audience"]!,
            ClockSkew = TimeSpan.Zero
        };
    });
// added reference of Application Layer
builder.Services.AddAutoMapper(typeof(ApplicationAssemblyMarker).Assembly);
builder.Services.AddApplication();


// adding reference of Infra layer
builder.Services.AddAutoMapper(typeof(InfraAssemblyMarker).Assembly);
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DatabaseConnection")!);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CommonResponseMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
