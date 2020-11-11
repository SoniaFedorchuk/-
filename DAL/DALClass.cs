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
        IQueryable<Books> GetAllBooks();

        Users GetUserByLoginAndPassword(string login, string passHash);

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
    }
}
