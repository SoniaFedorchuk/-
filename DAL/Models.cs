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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new BookConfigurate());
            modelBuilder.Configurations.Add(new RoleConfigurate());
            modelBuilder.Configurations.Add(new UserConfigurate());
        }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
    }

}