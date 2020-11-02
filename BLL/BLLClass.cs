using DAL;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Configuration;

namespace BLL
{
    public interface IBLLClass
    {
        void AddUser(UserDTO userDTO);
        IEnumerable<RoleDTO> GetAllRoles();

        UserDTO GetUserByLoginAndPassword(string login, string password);

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
                Password = Utils.ComputeSha256Hash(userDTO.Password)
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
                Password = null
            };
        }
    }
}
