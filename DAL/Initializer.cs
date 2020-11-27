using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Initializer : DropCreateDatabaseIfModelChanges<Model>
    {
        protected override void Seed(Model context)
        {
            base.Seed(context);

            var role = new List<Roles>()
            {
                new Roles()
                {
                    Name = "Admin"
                },
                new Roles()
                {
                    Name = "Librarian"
                },
                new Roles()
                {
                    Name = "User"
                }
            };
            context.Roles.AddRange(role);
            context.SaveChanges();


            var books = new List<Books>()
            {
                new Books()
                {
                    Name = "Eugene Onegin",
                    Publisher = "mem",
                    Pages = 100,
                    Price = 60,
                    Year = DateTime.Parse("09/10/1873"),
                    Amount = 100,
                    Author = "Pushkin",
                    Genre = "Drama"
                },

                new Books()
                {
                    Name = "The Captain’s Daughter",
                    Publisher = "mem",
                    Pages = 300,
                    Price = 80,
                    Year = DateTime.Parse("09/07/1860"),
                    Amount = 100,
                    Author = "Julien",
                    Genre = "Drama"
                },

                new Books()
                {
                    Name = "Anna Karenina",
                    Publisher = "nemem",
                    Pages = 170,
                    Price = 60,
                    Year = DateTime.Parse("06/15/1855"),
                    Amount = 100,
                    Author = "Pushkin",
                    Genre = "Drama"
                },

                new Books()
                {
                    Name = "What I Believe",
                    Publisher = "nemem",
                    Pages = 100,
                    Price = 70,
                    Year = DateTime.Parse("06/15/1855"),
                    Amount = 100,
                    Author = "Rokfeler",
                    Genre = "Sientific"
                },

                new Books()
                {
                    Name = "Harry Potter and the Philosopher's Stone",
                    Publisher = "nemem",
                    Pages = 230,
                    Price = 50,
                    Year = DateTime.Parse("05/21/1999"),
                    Amount = 100,
                    Author = "Rowling",
                    Genre = "Fantasy"
                }
            };

            var users = new List<Users>()
            {
                new Users()
                {
                    Login = "admin",
                    Password = Utils.ComputeSha256Hash("admin"),
                    RoleId = (int)UserRole.Admin
                }
            };
            context.Users.AddRange(users);
            context.SaveChanges();

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
