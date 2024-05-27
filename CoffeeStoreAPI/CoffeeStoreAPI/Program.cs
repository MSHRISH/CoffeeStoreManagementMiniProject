using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using CoffeeStoreAPI.Repositories;
using CoffeeStoreAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAPI
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

            #region context
            builder.Services.AddDbContext<CoffeeStoreContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
            #endregion

            #region Repository
            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int,Authentication>, AuthenticationRepository>();
            builder.Services.AddScoped<IRepository<int,Role>, RoleRepository>();
            builder.Services.AddScoped<IRepository<int, RoleMapping>, RoleMappingsRepository>();
            builder.Services.AddScoped<IRepository<int,Item>, ItemRepository>();
            builder.Services.AddScoped<IRepository<int,ItemType>, ItemTypeRepository>();
            builder.Services.AddScoped<IRepository<int,Order>, OrderRepository>();
            builder.Services.AddScoped<IRepository<OrderItemKeyDTO,OrderItem>,OrderItemsRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IUserService,UserServices>();
            builder.Services.AddScoped<ITokenServices, TokenServices>();
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
