using MarketBackend.BusinessLayer.Buyers.Guests;
using System;
using NLog;
using SystemLog;
using WebAPI.Controllers;
using MarketBackend;
using MarketBackend.ServiceLayer;
using MarketBackend.ServiceLayer.ServiceDTO;
using WebAPI;
using WebSocketSharp.Server;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var appConfig = new AppConfig();
            WebSocketServer notificationServer = new WebSocketServer(System.Net.IPAddress.Parse("127.0.0.1"), appConfig.websocketServerPort);
            notificationServer.Start();
            Console.WriteLine($"WS server started on ws://127.0.0.1:{appConfig.websocketServerPort}");

            SystemOperator so = new();
            if (!so.MarketOpen)
            {
                Console.WriteLine("Unable to open market successfully. Goodbye...");
                return;
            }

            //while (!so.MarketOpen)
            //{
            //    Console.WriteLine("Please enter Admin Username:");
            //    string username = Console.ReadLine();
            //    Console.WriteLine("Please enter Admin Password:");
            //    string password = Console.ReadLine();
            //    Response<int> openResponse = so.OpenMarket(username, password);
            //    if (openResponse.ErrorOccured)
            //        Console.WriteLine(openResponse.ErrorMessage);
            //}
            //Console.WriteLine("Market opened successfully!");


            //SetUpExample setup = new SetUpExample(so);
            //setup.SetUp();

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
            builder.Services.AddSingleton(_ => notificationServer);

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