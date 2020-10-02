using ConsoleApp.Core.Entity;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using CustomerApp.Infrastructure.SQL.Data;
using CustomerApp.Infrastructure.SQL.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;

namespace CustomerRestAPI
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
            services.AddDbContext<CustomerAppContext>(
                opt => opt.UseInMemoryDatabase("DB")
                );

            /*services.AddDbContext<CustomerAppContext>(
                opt => opt.UseSqlite("Data source = customerApp.db"));*/

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //    options.SerializerSettings.MaxDepth = 3;
            //});

        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<CustomerAppContext>();
                    var cust1 = ctx.Customers.Add(new Customer()
                    {
                        ID = 1,
                        FirstName = "Mickey",
                        LastName = "Mouse",
                        Address = "Playhouse"
                    }).Entity;

                    ctx.Customers.Add(new Customer()
                    {
                        ID = 2,
                        FirstName = "Ceni",
                        LastName = "Cienta",
                        Address = "Castillo"
                    });

                    ctx.Orders.Add(new Order()
                    {
                        ID = 1,
                        OrderDate = DateTime.Now,
                        DeliveryDate = DateTime.Now,
                        Customer = cust1
                    });

                    ctx.SaveChanges();
                }
            }
            else
            {
                app.UseHsts();
            }
        }
    }
}
