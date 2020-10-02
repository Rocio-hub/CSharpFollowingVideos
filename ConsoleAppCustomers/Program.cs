using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.ApplicationService.Services;
using CustomerApp.Core.DomainService;
using CustomerApp.Infrastructure.Static.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    #region Comments
    /*
      --UI--
      Console.WriteLine
      Console.ReadLine
      
      --INFRASTRUCTURE--
      EF - Static List - Text File
      
      --Test--
      Unit test for Core
      
      --CORE--
      Customer - Entity - Core.Entity
      Domain service - Respositoy / UOW - Core
      Application service - Service - Core
     */
    #endregion

    public class Program
    {
        #region Main
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IPrinter, Printer>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var printer = serviceProvider.GetRequiredService<IPrinter>();
            printer.StartUI();
        }
        #endregion   
    }
}
