using ConsoleApp.Core.Entity;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.DomainService;
using CustomerApp.Infrastructure.Static.Data.Repositories;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    public class Printer : IPrinter
    {
        #region Service area
        private ICustomerService _customerService;
        #endregion

        public Printer(ICustomerService customerService)
        {
            _customerService = customerService; ;
            InitData();            
        }

        #region UI
        public void StartUI()
        {
            string[] menuItems =
            {
                "List All Customers",
                "Add Customer",
                "Delete Customer",
                "Edit Customer",
                "Exit"
            };

            var selection = ShowMenu(menuItems);

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        var customers = _customerService.GetAllCustomers();
                        ListCustomers(customers);
                        break;
                    case 2:
                        var firstName = AskQuestion("First name: ");
                        var lastName = AskQuestion("Last name: ");
                        var address = AskQuestion("Address: ");
                        var customer = _customerService.NewCustomer(firstName, lastName, address);
                        _customerService.CreateCustomer(customer);
                        break;
                    case 3:
                        var idForDelete = PrintFindCustomerByID();
                        _customerService.DeleteCustomer(idForDelete);
                        break;
                    case 4:
                        var idForEdit = PrintFindCustomerByID();
                        var customerToEdit = _customerService.FindCustomerByID(idForEdit);
                        Console.WriteLine($"Updating {customerToEdit}");
                        var newFirstName = AskQuestion("First name: ");
                        var newLastName = AskQuestion("Last name: ");
                        var newAddress = AskQuestion("Address: ");
                        _customerService.UpdateCustomer(new Customer()
                        {
                            ID = idForEdit,
                            FirstName = newFirstName,
                            LastName = newLastName,
                            Address = newAddress
                        });
                        break;
                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.WriteLine("Bye bye!");
            Console.ReadLine();

            int PrintFindCustomerByID()
            {
                Console.WriteLine("Insert the ID of the customer");
                int id;
                while (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Please insert a valid number");
                }
                return id;
            }

            string AskQuestion(string question)
            {
                Console.WriteLine(question);
                return Console.ReadLine();
            }

            void ListCustomers(List<Customer> customers)
            {
                Console.WriteLine("\nList of customers");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"ID: {customer.ID} Name: {customer.FirstName} {customer.LastName}" +
                        $"Address: {customer.Address}");
                }
                Console.WriteLine("\n");
            }

            int ShowMenu(string[] menuItems)
            {
                Console.WriteLine("Select what you want to do: \n");
                for (int i = 0; i < menuItems.Length; i++)
                {
                    Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
                }

                int selection;
                while (!int.TryParse(Console.ReadLine(), out selection)
                    || selection < 1
                    || selection > 5)
                {
                    Console.WriteLine("You need to insert a number between 1 and 5");
                }
                return selection;
            }

            #endregion
        }
        #region Infrastructure later / Initialization layer
        public void InitData()
        {
            Customer cust1 = new Customer()
            {
                FirstName = "Sponge",
                LastName = "Bob",
                Address = "BikiniBottom"
            };
            _customerService.CreateCustomer(cust1);

            Customer cust2 = new Customer()
            {
                FirstName = "Mickey",
                LastName = "Mouse",
                Address = "Playhouse"
            };
            _customerService.CreateCustomer(cust2);
        }
        #endregion
    }
}

