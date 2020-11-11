using DAL.Configurations;
using DAL.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL
{

    public class Model : DbContext
    {
        public Model()
            : base("name=ModelConnection")
        {
            Database.SetInitializer(new Initializer());
        }
        protected override void OnModelCreating(DbModelBuilder model_builder)
        {
            base.OnModelCreating(model_builder);
            model_builder.Configurations.Add(new BookConfigurate());
            model_builder.Configurations.Add(new RoleConfigurate());
            model_builder.Configurations.Add(new UserConfigurate());
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
    }

}