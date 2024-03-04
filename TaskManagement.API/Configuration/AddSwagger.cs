using Microsoft.OpenApi.Models;

namespace TaskManagement.API.Configuration
{
    public static class AddSwagger
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services,
                 IConfiguration configuration)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });

                opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {

                            AuthorizationUrl = new Uri($"https://{configuration["Auth0:Domain"]}/authorize?audience={configuration["Auth0:Audience"]}"),
                            TokenUrl = new Uri($"https://{configuration["Auth0:Domain"]}/oauth/token"),

                            //AuthorizationUrl = new Uri(configuration["Auth0:Audience"], UriKind.Absolute),
                            //TokenUrl = new Uri("https://localhost:7177/swagger/oauth2-redirect.html"),
                            //Scopes = new Dictionary<string, string>
                            //{
                            //    { "email", "Access to your email" },
                            //    { "profile", "Access to your profile information" }
                            //}
                        }
                    }

                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "email", "writeaccess" }
                    }
                });

                //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                //        AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
                //        {
                //            ValidateIssuer = true,
                //            ValidateAudience = true,
                //            ValidateLifetime = true,
                //            ValidIssuer = configuration["jwt:Issuer"],
                //            ValidAudience = configuration["jwt:Audience"],
                //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"]))
                //        });
            });
        }
    }
}
