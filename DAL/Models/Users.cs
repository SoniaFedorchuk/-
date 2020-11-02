using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Users
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public virtual Roles Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
