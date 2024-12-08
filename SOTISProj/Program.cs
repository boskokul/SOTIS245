using SOTISProj.Services;
using Microsoft.EntityFrameworkCore;

using SOTISProj.Repo;
using Microsoft.Extensions.Configuration;
using SOTISProj.RepositoryInterfaces;
using SOTISProj.SeriveInterfaces;

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        
    }
}

