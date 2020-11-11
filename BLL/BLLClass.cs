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
        IEnumerable<RoleDTO> GetAllRoles();

        void AddUser(UserDTO userDTO);
        void DeleteBook(int index);
        void DeleteUser(int index);
        void AddBooks(BookDTO bookDTO);
        void UpdateBook(BookDTO booksDTO);
        void ChangeUserRole(int user_id, int new_user_role_id);

        UserDTO GetUserByLoginAndPassword(string login, string password);
        BookDTO GetBookByName(string name);

        bool SellBooks(int index, int amount);
        bool IsExistsUserByLogin(string login);
        bool IsExistsUserByLoginAndPassword(string login, string passHash);
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
                Password = Utils.ComputeSha256Hash(userDTO.Password),
                RoleId = userDTO.RoleId
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
                RoleId = user.RoleId,
                Role = user.Role.Name,
                Password = null
            };
        }
        public BookDTO GetBookByName(string name)
        {
            var book = dal.GetBookByName(name);
            if (book == null)
                return null;
            return new BookDTO()
            {
                Id = book.Id,
                Amount = book.Amount,
                Genre = book.Genre,
                Author = book.Author,
                Name = book.Name,
                Pages = book.Pages,
                Price = book.Price,
                Publisher = book.Publisher,
                Year = book.Year
            };
        }

        public void DeleteBook(int index)
        {
            dal.DeleteBook(index);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return dal.GetAllUsers().Select(user => new UserDTO()
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role.Name,
                RoleId = user.RoleId
            }).ToList();
        }

        public void ChangeUserRole(int user_id, int new_user_role_id)
        {
            dal.ChangeUserRole(user_id, new_user_role_id);
        }

        public void DeleteUser(int index)
        {
            dal.DeleteUser(index);
        }
    }
}
