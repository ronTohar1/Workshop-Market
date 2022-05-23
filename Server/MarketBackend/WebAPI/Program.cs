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
            new SystemController(so);
            so.OpenMarket("admin1", "password1");
            new AdminController(so.GetAdminFacade().Value);
            new BuyersController(so.GetBuyerFacade().Value);
            new StoresController(so.GetStoreManagementFacade().Value);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IBuyerFacade>(_ =>so.GetBuyerFacade().Value);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}