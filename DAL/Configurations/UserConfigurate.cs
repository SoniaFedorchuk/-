using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class UserConfigurate : EntityTypeConfiguration<Models.Users>
    {
        public UserConfigurate()
        {
            this.HasKey(u => u.Id);
            this.Property(u => u.RoleId);
            this.Property(u => u.Login).IsRequired().HasMaxLength(30);
            this.Property(u => u.Password).IsRequired().HasMaxLength(512);
        }
    }
}
