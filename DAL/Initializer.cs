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
                    Name = "User"
                }
            };
        }
    }
}
