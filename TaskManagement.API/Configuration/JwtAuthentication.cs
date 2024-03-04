using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace TaskManagement.API.Configuration;

public static class JwtAuthentication
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt => {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            // Youtube MongoBD 
            .AddCookie()
            .AddOpenIdConnect("Auth0", options =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}";

                // Configure Auth Creds
                options.ClientId = configuration["Auth0:ClientId"];
                options.ClientSecret = configuration["Auth0:ClientSecret"];

                // set response to code
                options.ResponseType = OpenIdConnectResponseType.Code;

                // set Scope
                options.Scope.Clear();
                options.Scope.Add("openid");

                
            })
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{configuration["Auth0:Domain"]}/";
            //options.TokenValidationParameters =
            //  new TokenValidationParameters
            //  {
            //      ValidateIssuer = true,
            //      ValidateAudience = true,
            //      ValidAudience = configuration["Jwt:Audience"],
            //      ValidIssuer = $"{configuration["Jwt:Domain"]}",l̥
            //      ValidateLifetime = true,
            //  };

            options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = configuration["Jwt:Issuer"],
               ValidAudience = configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
               NameClaimType = ClaimTypes.Name,
               RoleClaimType = ClaimTypes.Role
            };
        });
    }
}
