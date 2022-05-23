using MarketBackend.BusinessLayer.Buyers.Guests;
using System;
using NLog;
using SystemLog;
using WebAPI.Controllers;
using MarketBackend.ServiceLayer;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SystemOperator so = new SystemOperator();
            //so.OpenMarket("admin1", "password1");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<ISystemOperator>(_ => so);
            builder.Services.AddSingleton<IBuyerFacade>(_ => so.GetBuyerFacade().Value);
            builder.Services.AddSingleton<IStoreManagementFacade>(_ => so.GetStoreManagementFacade().Value);
            builder.Services.AddSingleton<IAdminFacade>(_ => so.GetAdminFacade().Value);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            //builder.Services.AddMvc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

 
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}