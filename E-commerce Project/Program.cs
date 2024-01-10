
using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic.Table_Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace E_commerce_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.GetConfig(builder.Configuration).AddServices();


            builder.Services.AddDbContext<DBcontext>(con => con.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))) ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();

            });

            app.MapControllers();

            app.Run();
        }
    }
}
