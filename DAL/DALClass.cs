using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public interface IDALClass
    {
        IQueryable<Roles> GetAllRoles();
        IQueryable<Books> GetAllBooks();
        IQueryable<Users> GetAllUsers();

        void AddUser(Users newUser);
        void AddBooks(Books newBook);
        void UpdateBook(Books book);
        void DeleteBook(int index);
        void DeleteUser(int index);
        void ChangeUserRole(int user_id, int new_user_role_id);

        Users GetUserByLoginAndPassword(string login, string passHash);

        bool SellBook(int index, int amount);
        bool IsExistsUserByLoginAndPassword(string login, string passHash);
        bool IsExistsUserByLogin(string login);
    }

    public class DALClass : IDALClass
    {
        private Model ctx = new Model();

        public void AddUser(Users newUser)
        {
            ctx.Users.Add(newUser);
            ctx.SaveChanges();
        }

        public IQueryable<Roles> GetAllRoles()
        {
            return ctx.Roles;
        }
        public IQueryable<Books> GetAllBooks()
        {
            return ctx.Books;
        }
        public void AddBooks(Books newBook)
        {
            ctx.Books.Add(newBook);
            ctx.SaveChanges();
        }
        public bool SellBook(int index, int amount)
        {
            Books book = ctx.Books.FirstOrDefault(b => b.Id == index);
            if (book.Amount < amount)
                return false;
            else
            {
                book.Amount -= amount;
                ctx.SaveChanges();
                return true;
            }
        }

        public Users GetUserByLoginAndPassword(string login, string passHash)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Login == login && u.Password == passHash);
            return user;
        }

        public bool IsExistsUserByLogin(string login)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Login == login);
            return user != null;
        }

        public bool IsExistsUserByLoginAndPassword(string login, string passHash)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Login == login && u.Password == passHash);
            return user != null;
        }
        public void DeleteBook(int index)
        {
            ctx.Books.Remove(ctx.Books.FirstOrDefault(b => b.Id == index));
            ctx.SaveChanges();
        }
        public void UpdateBook(Books book)
        {
            Books updatable_book = ctx.Books.Where(b => b.Id == book.Id).FirstOrDefault();

            updatable_book.Name = book.Name;
            updatable_book.Publisher = book.Publisher;
            updatable_book.Year = book.Year;
            updatable_book.Pages = book.Pages;
            updatable_book.Amount = book.Amount;
            updatable_book.Price = book.Price;
            updatable_book.Genre = book.Genre;
            updatable_book.Author = book.Author;
            ctx.SaveChanges();
        }

        public IQueryable<Users> GetAllUsers()
        {
            return ctx.Users;
        }

        public void ChangeUserRole(int user_id, int new_user_role_id)
        {
            ctx.Users.First(u => u.Id == user_id).RoleId = new_user_role_id;
            ctx.SaveChanges();
        }

        public void DeleteUser(int index)
        {
            ctx.Users.Remove(ctx.Users.FirstOrDefault(u => u.Id == index));
            ctx.SaveChanges();
        }
    }
}
