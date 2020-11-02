using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class RoleConfigurate : EntityTypeConfiguration<Models.Roles>
    {
        public RoleConfigurate()
        {
            this.HasKey(r => r.Id);
            this.Property(r => r.Name).IsRequired().HasMaxLength(30);
        }
    }
}
