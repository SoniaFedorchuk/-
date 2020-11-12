using DAL.Configurations;
using DAL.Models;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
    //public class MyDbConfig : DbConfiguration
    //{
    //    public MyDbConfig()
    //    {
    //        SqlConnectionFactory defaultFactory =
    //            new SqlConnectionFactory("Server=den1.mssql8.gear.host;Username=courselib;Password=Ni1a~P!sO8iS;");

    //        this.SetDefaultConnectionFactory(defaultFactory);
    //    }
    //}

    //[DbConfigurationType(typeof(MyDbConfig))]

    public class Model : DbContext
    {
        public Model()
            : base("LibShopContext")
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