using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Business.Abstract;
using ECommerce.Business.Concrete;
using ECommerce.DataAccess.Abstract;
using ECommerce.DataAccess.Concrete;
using ECommerce.DataAccess.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ECommerce.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddControllers();

            services.AddMemoryCache();

            services.Configure<appSettings_json>(Configuration.GetSection("Params"));


            ConnectionProvider.ConnectionString = Configuration.GetConnectionString("DBContext");

            services.AddDbContext<ECommerceContext>(
                options => options.UseSqlServer(ConnectionProvider.ConnectionString));



            #region swagger
            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Version = "V1",
                    Title = "ECommerce API Documantation",
                    Description = "Online Shopping",
                    Contact = new OpenApiContact()
                    {
                        Email = "malikulle@hotmail.com",
                        Name = "Mehmet Ali Külle",
                    }
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                gen.IncludeXmlComments(xmlPath);
            });

            #endregion

            #region services

            services.AddScoped<IProductRepository, EfCoreProductRepository>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();
            
            services.AddScoped<IUserRepository, EfCoreUserRepository>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IOrderRepository, EfCoreOrderRepository>();
            services.AddScoped<IOrderService, OrderManager>();

            services.AddScoped<IOrderItemReposiyory, EfCoreOrderItemRepository>();
            services.AddScoped<IOrderItemService, OrderItemManager>();

            services.AddScoped<IEmailService, EmailManager>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion


            #region token
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Params")["JWTKey_SigningKey"])),
                        ValidIssuer = Configuration.GetSection("Params")["JWTKey_Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });

            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/V1/swagger.json", "Ecommerce API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            SeedDatabase.Seed();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
