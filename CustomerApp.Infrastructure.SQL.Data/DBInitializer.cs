using ConsoleApp.Core.Entity;
using CustomerApp.Core.Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApp.Infrastructure.SQL.Data
{
    public class DBInitializer
    {
        public static void SeedDB(CustomerAppContext ctx)
        {
            //Every time I restart I reset the db: IMPORTANT: only on development process !!
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            var cust1 = ctx.Customers.Add(new Customer()
            {
                ID = 1,
                FirstName = "Mickey",
                LastName = "Mouse",
                Address = "Playhouse"
            }).Entity;

            ctx.Customers.Add(new Customer()
            {
                //ID = 2,
                FirstName = "Ceni",
                LastName = "Cienta",
                Address = "Castillo"
            });

            ctx.Orders.Add(new Order()
            {
                // ID = 1,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            ctx.Orders.Add(new Order()
            {
                // ID = 1,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                Customer = cust1
            });

            //Create Two users
            string password = "1234";
            byte[] passwordHashCarl, passwordSaltCarl, passwordHashPerry, passwordSaltPerry;
            CreatePasswordHash(password, out passwordHashCarl, out passwordSaltCarl);
            CreatePasswordHash(password, out passwordHashPerry, out passwordSaltPerry);


            List<User> users = new List<User>
            {
                new User
                {
                    Username = "UserCarl",
                    PasswordHash = passwordHashCarl,
                    PasswordSalt = passwordSaltCarl,
                    IsAdmin = false
                },
                new User
                {
                    Username = "AdminPerry",
                    PasswordHash = passwordHashPerry,
                    PasswordSalt = passwordSaltPerry,
                    IsAdmin = true
                }
            };

            //ctx.Customers.AddRange(items);

            ctx.SaveChanges();
        }
            private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
    }
}
