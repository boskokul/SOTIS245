using SOTISProj.Services;
using Microsoft.EntityFrameworkCore;

using SOTISProj.Repo;
using Microsoft.Extensions.Configuration;
using SOTISProj.RepositoryInterfaces;
using SOTISProj.SeriveInterfaces;
using SOTISProj.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SOTISProj
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<MyDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDataService, InMemoryDataService>();
            builder.Services.AddScoped<IFieldRepository, FieldRepository>();
            builder.Services.AddScoped<ITestRepository, TestRepository>();
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
            builder.Services.AddScoped<IAuthenticationServicee, AuthenticationService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddCors(options => 
            { 
                options.AddPolicy("AllowAll", builder => 
                { 
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader(); 
                }); 
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                         ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "explorer",
                        ValidAudience = "explorer-front.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecureKeyWithAtLeast32Characters"))
                    };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("AuthenticationTokens-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("admin"));
                options.AddPolicy("StudentPolicy", policy => policy.RequireRole("student"));
            });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
    }
}

