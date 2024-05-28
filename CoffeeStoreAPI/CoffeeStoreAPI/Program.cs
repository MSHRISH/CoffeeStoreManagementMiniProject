using CoffeeStoreAPI.Context;
using CoffeeStoreAPI.Iterfaces;
using CoffeeStoreAPI.Models;
using CoffeeStoreAPI.Models.DTOs;
using CoffeeStoreAPI.Repositories;
using CoffeeStoreAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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

            #region Authorization
            //Auth
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                    };

                });
            #endregion

            #region RoleBasedAuth
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireCustomerRole", policy => policy.RequireRole("Customer"));
                options.AddPolicy("RequireBaristaRole", policy => policy.RequireRole("Barista"));

                options.AddPolicy("RequireAdminOrManagerRole", policy =>
           policy.RequireAssertion(context =>
               context.User.IsInRole("Admin") || context.User.IsInRole("Manager")));

                options.AddPolicy("RequireStoreEmployee", policy =>
           policy.RequireAssertion(context =>
               context.User.IsInRole("Admin") || context.User.IsInRole("Barista")||context.User.IsInRole("Manager")));
            }
         );
            #endregion

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
            builder.Services.AddScoped<IItemServices, ItemServices>();
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
