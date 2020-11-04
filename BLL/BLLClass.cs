using DAL;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Configuration;
using System.Collections;

namespace BLL
{
    public interface IBLLClass
    {
        void AddUser(UserDTO userDTO);
        IEnumerable<RoleDTO> GetAllRoles();
        UserDTO GetUserByLoginAndPassword(string login, string password);
        bool SellBooks(int index, int amount);
        bool IsExistsUserByLogin(string login);
        bool IsExistsUserByLoginAndPassword(string login, string passHash);
        void AddBooks(BookDTO bookDTO);
        IEnumerable <BookDTO> GetAllBooks();
        void DeleteBook(int index);
        void UpdateBook(BookDTO booksDTO);
    }

    public class BLLClass : IBLLClass
    {
        private IDALClass dal = null;

        public BLLClass()
        {
            dal = new DALClass();
        }

        public void AddUser(UserDTO userDTO)
        {
            dal.AddUser(new Users()
            {
                Login = userDTO.Login,
                Password = Utils.ComputeSha256Hash(userDTO.Password)
            });
        }
        public bool SellBooks(int index, int amount)
        {
            return dal.SellBook(index, amount);
        }

        public void AddBooks(BookDTO booksDTO)
        {
            dal.AddBooks(new Books()
            {
                Name = booksDTO.Name,
                Publisher = booksDTO.Publisher,
                Pages = booksDTO.Pages,
                Price = booksDTO.Price,
                Year = booksDTO.Year,
                Author = booksDTO.Author,
                Genre = booksDTO.Genre,
                Amount = booksDTO.Amount

            });
        }
        public void UpdateBook(BookDTO booksDTO)
        {
            dal.UpdateBook(new Books()
            {
                Name = booksDTO.Name,
                Publisher = booksDTO.Publisher,
                Pages = booksDTO.Pages,
                Price = booksDTO.Price,
                Year = booksDTO.Year,
                Author = booksDTO.Author,
                Genre = booksDTO.Genre,
                Amount = booksDTO.Amount
            });
        }
        public IEnumerable<RoleDTO> GetAllRoles()
        {
            return dal.GetAllRoles().Select(role => new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();
        }
        public IEnumerable<BookDTO> GetAllBooks()
        {
            return dal.GetAllBooks().Select(books => new BookDTO()
            {
                Id = books.Id,
                Name = books.Name,
                Author = books.Author,
                Amount = books.Amount,
                Pages = books.Pages,
                Price = books.Price,
                Publisher = books.Publisher,
                Year = books.Year,
                Genre = books.Genre
            }).ToList();
        }
        public bool IsExistsUserByLogin(string login)
        {
            return dal.IsExistsUserByLogin(login);
        }

        public bool IsExistsUserByLoginAndPassword(string login, string pass)
        {
            return dal.IsExistsUserByLoginAndPassword(login, Utils.ComputeSha256Hash(pass));
        }

        public UserDTO GetUserByLoginAndPassword(string login, string password)
        {
            var user = dal.GetUserByLoginAndPassword(login, Utils.ComputeSha256Hash(password));

            if (user == null)
                return null;

            return new UserDTO()
            {
                Id = user.Id,
                Login = user.Login,
                Password = null
            };
        }
        public void DeleteBook(int index)
        {
            dal.DeleteBook(index);
        }
    }
}
