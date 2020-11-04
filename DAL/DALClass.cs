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
        void AddUser(Users newUser);
        IQueryable<Roles> GetAllRoles();
        void AddBooks(Books newBook);
        void UpdateBook(Books book);
        bool SellBook(int index, int amount);
        Users GetUserByLoginAndPassword(string login, string passHash);

        bool IsExistsUserByLoginAndPassword(string login, string passHash);
        bool IsExistsUserByLogin(string login);
        IQueryable<Books> GetAllBooks();
        void DeleteBook(int index);
    }

    public class DALClass : IDALClass
    {
        private Model ctx = new Model();

        public void AddUser(Users newUser)
        {
            ctx.Users.Add(newUser);
            ctx.SaveChanges();
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

        public IQueryable<Roles> GetAllRoles()
        {
            return ctx.Roles;
        }
        public IQueryable<Books> GetAllBooks()
        {
            return ctx.Books;
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

    }
}
